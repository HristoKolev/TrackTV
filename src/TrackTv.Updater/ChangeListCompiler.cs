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

    using TvDbSharper;
    using TvDbSharper.Dto;

    public class ChangeListCompiler
    {
        private const int ChangeChunkSize = 1000;

        public ChangeListCompiler(ITvDbClient client, IDbService dbService, ILog log)
        {
            this.Client = client;
            this.DbService = dbService;
            this.Log = log;
        }

        private ITvDbClient Client { get; }

        private IDbService DbService { get; }

        private ILog Log { get; }

        public async Task MergeChangeList(Update[] updates)
        {
            var updateIDs = updates.Select(u => u.Id).ToArray();

            var changeList = await this.GetChangeList().ConfigureAwait(false);

            var registeredEpisodeIDs =
                this.DbService.Episodes.Where(p => updateIDs.Contains(p.Thetvdbid)).Select(p => p.Thetvdbid).ToArray();

            var registeredEpisodeIDsHashSet = new HashSet<int>(registeredEpisodeIDs);
    
            int chunkCount = 1;

            foreach (var chunk in updates.Split(ChangeChunkSize))
            {
                var tasks = chunk.Select(update => this.MergeUpdates(update, registeredEpisodeIDsHashSet, changeList)).ToArray();

                await Task.WhenAll(tasks).ConfigureAwait(false);

                if (updates.Length > ChangeChunkSize)
                {
                    this.Log.Debug($"Chunk {chunkCount++} of ~{updates.Length / ChangeChunkSize}");
                }
            }

            this.Log.Debug($"{changeList.Count} changes compiled.");

            var list = changeList.Values.Where(poco => poco != null).ToList();

            this.DbService.BulkInsert(list);

            this.Log.Debug($"{changeList.Count} changes inserted.");
        }

        private async Task<ConcurrentDictionary<string, ApiChangePoco>> GetChangeList()
        {
            var list = await this.DbService.ApiChanges.Select(poco => new { ID = poco.ApiChangeThetvdbid, Type= poco.ApiChangeType }).ToListAsync().ConfigureAwait(false);

            var dict = new ConcurrentDictionary<string, ApiChangePoco>();

            foreach (var item in list)
            {
                dict.TryAdd(item.ID + "_" + item.Type, null);
            }

            return dict;
        }

        private async Task MergeUpdates(Update update, ICollection<int> registeredEpisodeIDs, ConcurrentDictionary<string, ApiChangePoco> changeList)
        {
            var episode = await this.GetExternalEpisodeAsync(update.Id).ConfigureAwait(false);

            if (episode != null)
            {
                if (registeredEpisodeIDs.Contains(update.Id))
                {
                     AddEpisode(episode, changeList) ;
                }
                else
                {
                    int seriesID = int.Parse(episode.SeriesId);

                    var attachedSeries = await this.GetExternalShowAsync(seriesID).ConfigureAwait(false);

                    if (attachedSeries != null)
                    {
                         AddShow(attachedSeries, changeList) ;
                    }
                }
            }

            var series = await this.GetExternalShowAsync(update.Id).ConfigureAwait(false);

            if (series != null)
            {
                 AddShow(series, changeList) ;
            }
        }
 
        private static void AddShow(Series series, ConcurrentDictionary<string, ApiChangePoco> changeList)
        {
            string key = series.Id + "_" + (int)ApiChangeType.Show;

            if (IsValidSeries(series) && !changeList.TryGetValue(key, out var _))
            {
                changeList.TryAdd(key, new ApiChangePoco
                {
                    ApiChangeCreatedDate = DateTime.UtcNow,
                    ApiChangeType = (int)ApiChangeType.Show,
                    ApiChangeThetvdbid = series.Id
                });
            }
        }
 
        private static void AddEpisode(EpisodeRecord episode, ConcurrentDictionary<string, ApiChangePoco> changeList)
        {
            string key = episode.Id + "_" + (int)ApiChangeType.Episode;

            if (!changeList.TryGetValue(key, out var _))
            {
                changeList.TryAdd(key, new ApiChangePoco
                {
                    ApiChangeCreatedDate = DateTime.UtcNow,
                    ApiChangeType = (int)ApiChangeType.Episode,
                    ApiChangeThetvdbid = episode.Id,
                    ApiChangeAttachedSeriesID = int.Parse(episode.SeriesId)
                });
            }
        }

        private static bool IsValidSeries(Series series)
        {
            return !string.IsNullOrWhiteSpace(series.SeriesName) && !series.SeriesName.StartsWith("***Duplicate");
        }

        private async Task<EpisodeRecord> GetExternalEpisodeAsync(int updateId)
        {
            try
            {
                var response = await this.Client.Episodes.GetAsync(updateId).ConfigureAwait(false);

                return response?.Data;
            }
            catch (TvDbServerException ex)
            {
                if (ex.StatusCode == 404)
                {
                    return null;
                }

                throw;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        private async Task<Series> GetExternalShowAsync(int updateId)
        {
            try
            {
                var response = await this.Client.Series.GetAsync(updateId).ConfigureAwait(false);

                return response?.Data;
            }
            catch (TvDbServerException ex)
            {
                if (ex.StatusCode == 404)
                {
                    return null;
                }

                throw;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }
    }
}