﻿namespace TrackTv.Updater
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
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
        public DataSynchronizer(ILog log, ErrorHandler errorHandler, ITvDbClient client)
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

            if (!bool.Parse(await settingsService.GetSettingAsync(Setting.DisableDatabaseUpdate)))
            {
                if (!Global.CliOptions.ApplyOnly && !Global.CliOptions.ListChanges)
                {
                    await this.UpdateChangeLists(container);
                }

                var apiChangeRepository = container.GetInstance<ApiChangeRepository>();
                var fullChangeList = await apiChangeRepository.GetCurrentChangeList();

                if (Global.CliOptions.SkipFailed)
                {
                    fullChangeList = fullChangeList.Where(poco => poco.ApiChangeFailCount == 0).ToArray();
                }

                if (Global.CliOptions.RetryFailedOnly)
                {
                    fullChangeList = fullChangeList.Where(poco => poco.ApiChangeFailCount > 0).ToArray();
                }

                int episodeCount = fullChangeList.Count(item => item.ApiChangeType == (int)ApiChangeType.Episode);
                int showCount = fullChangeList.Count(item => item.ApiChangeType    == (int)ApiChangeType.Show);
                this.Log.Debug($"{episodeCount} episodes. {showCount} shows.");

                if (!Global.CliOptions.CompileOnly && !Global.CliOptions.ListChanges)
                {
                    int index = 1;

                    foreach (var change in fullChangeList)
                    {
                        await this.ApplyChange(change, index, fullChangeList.Length, container)
                                  .ContinueWith(task => index++);
                    }
                }

                Global.Log.Debug("Updater finished successfully.");
            }
            else
            {
                Global.Log.Debug("Updates disabled. Exiting...");
            }
        }

        private async Task UpdateChangeLists(IContainer container)
        {
            var settingsService = container.GetInstance<SettingsService>();

            var changeListCompiler = container.GetInstance<ChangeListCompiler>();
            var dbService = container.GetInstance<IDbService>();

            var lastUpdated = DateTime.Parse(await settingsService.GetSettingAsync(Setting.LastDatabaseUpdate))
                                      .ToUniversalTime();

            var updates = await this.GetUpdates(lastUpdated, DateTime.UtcNow);

            if (updates.Any())
            {
                await dbService.ExecuteInTransactionAndCommit(async () =>
                {
                    var changeListWatch = Stopwatch.StartNew();

                    try
                    {
                        await changeListCompiler.MergeChangeList(updates);
                    }
                    catch (Exception e)
                    {
                        throw new DataSyncException("An error occured while merging the change list.", e);
                    }

                    changeListWatch.Stop();
                    this.Log.Debug($"Change list was merged successfuly in {changeListWatch.Elapsed:hh\\:mm\\:ss}.");

                });

                var newLastUpdated = new[]
                {
                    lastUpdated,
                    updates.Select(u => u.LastUpdated).Max().ToDateTime()
                }.Max();

                await settingsService.SetSettingAsync(Setting.LastDatabaseUpdate, newLastUpdated.ToString("O"));
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

                    var timeoutToken = new CancellationTokenSource(TimeSpan.FromMinutes(20)).Token;

                    await dbService.ExecuteInTransactionAndCommit(async tr =>
                    {
                        string typeName = ((ApiChangeType)change.ApiChangeType).ToString();

                        try
                        {
                           this.Log.Debug(
                               $"[{index}/{maxCount}] Starting to apply change (ID={change.ApiChangeThetvdbid}, Type={typeName})");

                           await applier.ApplyChange(change);

                           await failedChangeRepository.RemoveApiChange(change.ApiChangeThetvdbid);

                           changeWatch.Stop();

                           this.Log.Debug(
                               $"[{index}/{maxCount}] Successfuly applied change (ID={change.ApiChangeThetvdbid}, Type={typeName} {changeWatch.Elapsed:mm\\:ss})");
                        }
                        catch (Exception e)
                        {
                           await tr.RollbackAsync();

                           await failedChangeRepository.IncrementFailedCount(change.ApiChangeThetvdbid);

                           changeWatch.Stop();

                           throw new DataSyncException(
                               $"[{index}/{maxCount}] Failed to apply a change. (ID={change.ApiChangeThetvdbid}, Type={typeName}, {changeWatch.Elapsed:mm\\:ss})",
                               e);
                        }
                    }, timeoutToken)
                   ;
                }
                catch (Exception e)
                {
                    await this.ErrorHandler.HandleErrorAsync(e);
                }
            }
        }

        private async Task<Update[]> GetUpdates(DateTime fromTime, DateTime toTime)
        {
            Update[] updates;
            var updatesWatch = Stopwatch.StartNew();

            try
            {
                var response = await this.Client.Updates.GetAccumulatedAsync(fromTime, toTime);

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