namespace TrackTv.Updater
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using log4net;

    using StructureMap;

    using TrackTv.Data;
    using TrackTv.Services;

    using TvDbSharper;
    using TvDbSharper.Dto;

    public class DataSynchronizer
    {
        public DataSynchronizer(
            ILog log,
            SettingsService settingsService,
            ErrorHandler errorHandler,
            ITvDbClient client)
        {
            this.Log = log;
            this.SettingsService = settingsService;
            this.ErrorHandler = errorHandler;
            this.Client = client;
        }

        private ITvDbClient Client { get; }
 
        private ErrorHandler ErrorHandler { get; }

        private ILog Log { get; }

        private SettingsService SettingsService { get; }

        public async Task PerformUpdate(IContainer container)
        {
            if (!bool.Parse(await this.SettingsService.GetSettingAsync(Setting.DisableDatabaseUpdate).ConfigureAwait(false)))
            {
                var lastUpdated = DateTime.Parse(await this.SettingsService.GetSettingAsync(Setting.LastDatabaseUpdate).ConfigureAwait(false))
                                          .ToUniversalTime();

                var changeListCompiler = container.GetInstance<ChangeListCompiler>();
                var failedChangeRepository = container.GetInstance<ApiChangeRepository>();

                var updates = await this.GetUpdates(lastUpdated, DateTime.UtcNow).ConfigureAwait(false);

                var changeList = await this.GetChangeList(updates, changeListCompiler).ConfigureAwait(false);

                var failedChangeList = await failedChangeRepository.GetFailedUpdates().ConfigureAwait(false);

                int episodeCount = failedChangeList.Count(item => item.Type == UpdateRecordType.Episode);
                int showCount = failedChangeList.Count(item => item.Type    == UpdateRecordType.Show);
                this.Log.Debug($"Failed changes from previous runs: {episodeCount} episodes. {showCount} shows.");

                var fullChangeList = changeList.Concat(failedChangeList);

                int index = 1;

                foreach (var change in fullChangeList)
                {
                    await this.ApplyChange(change, lastUpdated, index, changeList.Count, container)
                              .ContinueWith(task => index++).ConfigureAwait(false);
                }

                Global.Log.Debug("Updater finished successfully.");
            }
            else
            {
                Global.Log.Debug("Updates disabled. Exiting...");
            }
        }
 
        private async Task ApplyChange(ChangeListItem change, DateTime lastUpdated, int index, int maxCount, IContainer masterContainer)
        {
            using (var container = masterContainer.CreateChildContainer())
            {
                try
                {
                    var dbService = container.GetInstance<IDbService>();
                    var settingsService = container.GetInstance<SettingsService>();
                    var applier = container.GetInstance<ChangeListApplier>();
                    var failedChangeRepository = container.GetInstance<ApiChangeRepository>();

                    var changeWatch = Stopwatch.StartNew();

                    await dbService.ExecuteInTransaction(async transaction =>
                    {
                        try
                        {
                            this.Log.Debug($"[{index}/{maxCount}] Starting to apply change (ID={change.TheTvDbID},"
                                           + $" Type={change.Type.ToString()}, EpisodeCount={change.EpisodeIDs.Length})");

                            await applier.ApplyChange(change).ConfigureAwait(false);

                            await failedChangeRepository.RemoveFailedUpdate(change.TheTvDbID).ConfigureAwait(false);
                        }
                        catch (Exception e)
                        {
                            transaction.Rollback();

                            await failedChangeRepository.AddFailedApiChange(change).ConfigureAwait(false);

                            changeWatch.Stop();

                            throw new DataSyncException(
                                $"[{index}/{maxCount}] Failed to apply a change. (ID={change.TheTvDbID},"
                                + $" Type={change.Type.ToString()}, EpisodeCount={change.EpisodeIDs.Length}) {changeWatch.Elapsed:mm\\:ss}", e);
                        }

                        DateTime newLastUpdated = new[]
                        {
                            lastUpdated,
                            change.LastUpdated
                        }.Max();

                        await settingsService.SetSettingAsync(Setting.LastDatabaseUpdate, newLastUpdated.ToString("O"))
                                             .ConfigureAwait(false);
                    }).ConfigureAwait(false);

                    changeWatch.Stop();

                    this.Log.Debug($"[{index}/{maxCount}] Successfuly applied change (ID={change.TheTvDbID},"
                                   + $" Type={change.Type.ToString()}, EpisodeCount={change.EpisodeIDs.Length}) {changeWatch.Elapsed:mm\\:ss}");
                }
                catch (Exception e)
                {
                    await this.ErrorHandler.HandleErrorAsync(e).ConfigureAwait(false);
                }
            }
        }

        private async Task<List<ChangeListItem>> GetChangeList(Update[] updates, ChangeListCompiler changeListCompiler)
        {
            List<ChangeListItem> changeList;
            var changeListWatch = Stopwatch.StartNew();

            try
            {
                changeList = await changeListCompiler.GetChangeList(updates).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new DataSyncException("An error occured while compiling the change list.", e);
            }

            this.Log.Debug($"Change list was compiled successfuly in {changeListWatch.Elapsed:hh\\:mm\\:ss}.");

            int episodeCount = changeList.Count(item => item.Type == UpdateRecordType.Episode);
            int showCount = changeList.Count(item => item.Type    == UpdateRecordType.Show);
            this.Log.Debug($"{episodeCount} episodes. {showCount} shows.");

            return changeList;
        }

        private async Task<Update[]> GetUpdates(DateTime fromTime, DateTime toTime)
        {
            Update[] updates;
            var updatesWatch = Stopwatch.StartNew();

            try
            {
                var response = await this.Client.Updates.GetAccumulatedAsync(fromTime, toTime).ConfigureAwait(false);

                if (response.Data == null)
                {
                    updates = Array.Empty<Update>();
                }
                else
                {
                    updates = response.Data.OrderBy(update => update.LastUpdated).ToArray();
                }
            }
            catch (Exception e)
            {
                throw new DataSyncException("An error occured while getting the updates from the server.", e);
            }

            this.Log.Debug($"{updates.Length} updates were detected in {updatesWatch.Elapsed:hh\\:mm\\:ss}.");
            updatesWatch.Stop();
            return updates;
        }
    }

    public class DataSyncException : Exception
    {
        public DataSyncException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public DataSyncException(string message)
            : base(message)
        {
        }
    }
}