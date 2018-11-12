namespace TrackTv.Updater
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using log4net;

    using LinqToDB;

    using TrackTv.Data;
    using TrackTv.Services;
    using TrackTv.Updater.Infrastructure;

    using TvDbSharper;
    using TvDbSharper.Dto;

    public class ChangeListCompiler
    {
        public ChangeListCompiler(ITvDbClient client, IDbService dbService, ILog log, SettingsService settingsService)
        {
            this.Client = client;
            this.DbService = dbService;
            this.Log = log;
            this.SettingsService = settingsService;
        }

        private ITvDbClient Client { get; }

        private IDbService DbService { get; }

        private ILog Log { get; }

        private SettingsService SettingsService { get; }

        public async Task MergeChangeList(Update[] updates)
        {
            var updateIDs = updates.Select(u => u.Id).ToArray();

            var changeList = await this.GetChangeList();

            var registeredEpisodeIDs =
                this.DbService.Poco.Episodes.Where(p => updateIDs.Contains(p.Thetvdbid)).Select(p => p.Thetvdbid).ToArray();

            var registeredEpisodeIDsHashSet = new HashSet<int>(registeredEpisodeIDs);

            int changeChunkSize = int.Parse(await this.SettingsService.GetSettingAsync(Setting.UpdateChangeChunkSize));

            int chunkCount = 1;

            foreach (var chunk in updates.Split(changeChunkSize))
            {
                var tasks = chunk.Select(update => this.MergeUpdates(update, registeredEpisodeIDsHashSet, changeList)).ToArray();

                if (updates.Length > changeChunkSize)
                {
                    this.Log.Debug($"Chunk {chunkCount++} of {Math.Ceiling(updates.Length / (decimal)changeChunkSize)}");
                }

                await Task.WhenAll(tasks);
            }

            this.Log.Debug($"{changeList.Count} changes compiled.");

            var list = changeList.Values.Where(poco => poco != null).ToList();

            await this.DbService.BulkInsert(list);

            this.Log.Debug($"{changeList.Count} changes inserted.");
        }

        private async Task<ConcurrentDictionary<string, ApiChangePoco>> GetChangeList()
        {
            var list = await this.DbService.Poco.ApiChanges.Select(poco => new { ID = poco.ApiChangeThetvdbid, Type= poco.ApiChangeType }).ToListAsync();

            var dict = new ConcurrentDictionary<string, ApiChangePoco>();

            foreach (var item in list)
            {
                dict.TryAdd(item.ID + "_" + item.Type, null);
            }

            return dict;
        }

        private async Task MergeUpdates(Update update, ICollection<int> registeredEpisodeIDs, ConcurrentDictionary<string, ApiChangePoco> changeList)
        {
            var episode = await this.Client.Episodes.GetExternalEpisodeAsync(update.Id);

            if (episode != null)
            {
                if (registeredEpisodeIDs.Contains(update.Id))
                {
                     AddEpisode(episode, changeList) ;
                }
                else
                {
                    int seriesID = int.Parse(episode.SeriesId);

                    if (seriesID != 0)
                    {
                        var attachedSeries = await this.Client.Series.GetExternalShowAsync(seriesID);

                        if (attachedSeries != null)
                        {
                            AddShow(attachedSeries, changeList);
                        }
                    }
                }
            }

            var series = await this.Client.Series.GetExternalShowAsync(update.Id);

            if (series != null)
            {
                 AddShow(series, changeList) ;
            }
        }

        private static void AddShow(Series series, ConcurrentDictionary<string, ApiChangePoco> changeList)
        {
            string key = series.Id + "_" + (int)ApiChangeType.Show;

            if (IsValidSeries(series) && !changeList.TryGetValue(key, out _))
            {
                changeList.TryAdd(key, new ApiChangePoco
                {
                    ApiChangeCreatedDate = DateTime.UtcNow,
                    ApiChangeType = (int)ApiChangeType.Show,
                    ApiChangeThetvdbid = series.Id,
                    ApiChangeThetvdbLastUpdated = series.LastUpdated.ToDateTime(),
                });
            }
        }

        private static void AddEpisode(EpisodeRecord episode, ConcurrentDictionary<string, ApiChangePoco> changeList)
        {
            string key = episode.Id + "_" + (int)ApiChangeType.Episode;

            if (!changeList.TryGetValue(key, out _))
            {
                changeList.TryAdd(key, new ApiChangePoco
                {
                    ApiChangeCreatedDate = DateTime.UtcNow,
                    ApiChangeType = (int)ApiChangeType.Episode,
                    ApiChangeThetvdbid = episode.Id,
                    ApiChangeAttachedSeriesID = int.Parse(episode.SeriesId),
                    ApiChangeThetvdbLastUpdated = episode.LastUpdated.ToDateTime(),
                });
            }
        }

        private static bool IsValidSeries(Series series)
        {
            return !string.IsNullOrWhiteSpace(series.SeriesName) && !series.SeriesName.StartsWith("***Duplicate");
        }
    }
}