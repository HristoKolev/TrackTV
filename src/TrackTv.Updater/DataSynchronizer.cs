namespace TrackTv.Updater
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using log4net;

    using StructureMap;

    using TrackTv.Data;
    using TrackTv.Services;
    using TrackTv.Updater.Infrastructure;

    using TvDbSharper;
    using TvDbSharper.Dto;

    public class DataSynchronizer
    {
        public DataSynchronizer(
            ILog log,
            ErrorHandler errorHandler,
            ITvDbClient client)
        {
            this.Log = log;
            this.ErrorHandler = errorHandler;
            this.Client = client;
        }

        private ITvDbClient Client { get; }
 
        private ErrorHandler ErrorHandler { get; }

        private ILog Log { get; }

        public async Task PerformUpdate(IContainer container)
        {
            var settingsService = container.GetInstance<SettingsService>();

            if (!bool.Parse(await settingsService.GetSettingAsync(Setting.DisableDatabaseUpdate).ConfigureAwait(false)))
            {
                var lastUpdated = DateTime.Parse(await settingsService.GetSettingAsync(Setting.LastDatabaseUpdate).ConfigureAwait(false))
                                          .ToUniversalTime();

                var changeListCompiler = container.GetInstance<ChangeListCompiler>();
                var apiChangeRepository = container.GetInstance<ApiChangeRepository>();
      
                var dbService = container.GetInstance<IDbService>();

                var updates = await this.GetUpdates(lastUpdated, DateTime.UtcNow).ConfigureAwait(false);

                if (updates.Any())
                {
                    await dbService.ExecuteInTransaction(async () =>
                    {
                        await this.MergeChangeList(updates, changeListCompiler).ConfigureAwait(false);
                    })
                    .ConfigureAwait(false);

                    var newLastUpdated = new[]
                    {
                        lastUpdated,
                        updates.Select(u => u.LastUpdated).Max().ToDateTime()
                    }.Max();

                    await settingsService.SetSettingAsync(Setting.LastDatabaseUpdate, newLastUpdated.ToString("O"))
                                         .ConfigureAwait(false);
                }

                var fullChangeList = await apiChangeRepository.GetCurrentChangeList().ConfigureAwait(false);

                int episodeCount = fullChangeList.Count(item => item.ApiChangeType == (int)ApiChangeType.Episode);
                int showCount = fullChangeList.Count(item => item.ApiChangeType == (int)ApiChangeType.Show);
                this.Log.Debug($"{episodeCount} episodes. {showCount} shows.");

                int index = 1;

                foreach (var change in fullChangeList)
                {
                    await this.ApplyChange(change, index, fullChangeList.Length, container)
                              .ContinueWith(task => index++).ConfigureAwait(false);
                }

                Global.Log.Debug("Updater finished successfully.");
            }
            else
            {
                Global.Log.Debug("Updates disabled. Exiting...");
            }
        }
 
        private async Task ApplyChange(ApiChangePoco change, int index, int maxCount, IContainer masterContainer)
        {
            using (var container = masterContainer.CreateChildContainer())
            {
                try
                {
                    var dbService = container.GetInstance<IDbService>();
                    var applier = container.GetInstance<ChangeListApplier>();
                    var failedChangeRepository = container.GetInstance<ApiChangeRepository>();

                    var changeWatch = Stopwatch.StartNew();

                    await dbService.ExecuteInTransaction(async tr =>
                    {
                        string typeName = ((ApiChangeType)change.ApiChangeType).ToString();

                        try
                        {
                            this.Log.Debug($"[{index}/{maxCount}] Starting to apply change (ID={change.ApiChangeThetvdbid}, Type={typeName})");

                            await applier.ApplyChange(change).ConfigureAwait(false);

                            await failedChangeRepository.RemoveApiChange(change.ApiChangeThetvdbid).ConfigureAwait(false);

                            changeWatch.Stop();

                            this.Log.Debug($"[{index}/{maxCount}] Successfuly applied change (ID={change.ApiChangeThetvdbid}, Type={typeName} {changeWatch.Elapsed:mm\\:ss})");
                        }
                        catch (Exception e)
                        {
                            tr.Rollback();

                            await failedChangeRepository.IncrementFailedCount(change.ApiChangeThetvdbid).ConfigureAwait(false);

                            changeWatch.Stop();

                            throw new DataSyncException(
                                $"[{index}/{maxCount}] Failed to apply a change. (ID={change.ApiChangeThetvdbid}, Type={typeName}, {changeWatch.Elapsed:mm\\:ss})", e);
                        }
                    }, TimeSpan.FromMinutes(20)).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    await this.ErrorHandler.HandleErrorAsync(e).ConfigureAwait(false);
                }
            }
        }

        private async Task MergeChangeList(Update[] updates, ChangeListCompiler changeListCompiler)
        {
            var changeListWatch = Stopwatch.StartNew();

            try
            {
                await changeListCompiler.MergeChangeList(updates).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new DataSyncException("An error occured while merging the change list.", e);
            }

            changeListWatch.Stop();
            this.Log.Debug($"Change list was merged successfuly in {changeListWatch.Elapsed:hh\\:mm\\:ss}.");
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