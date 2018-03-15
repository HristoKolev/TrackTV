namespace TrackTv.Updater
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LinqToDB;

    using TrackTv.Data;

    using TvDbSharper;
    using TvDbSharper.Dto;

    public class ChangeListCompiler
    {
        public ChangeListCompiler(ITvDbClient client, IDbService dbService)
        {
            this.Client = client;
            this.DbService = dbService;
        }

        private ITvDbClient Client { get; }

        private IDbService DbService { get; }

        public async Task<List<ChangeListItem>> GetChangeList(Update[] updates)
        {
            var updateIDs = updates.Select(update => update.Id).ToArray();

            var registeredEpisodes = await this.DbService.Episodes.Where(poco => updateIDs.Contains(poco.Thetvdbid))
                                               .Select(poco => poco.Thetvdbid)
                                               .ToArrayAsync()
                                               .ConfigureAwait(false);

            var registeredShows = await this.DbService.Shows.Where(poco => updateIDs.Contains(poco.Thetvdbid))
                                            .Select(poco => poco.Thetvdbid)
                                            .ToArrayAsync()
                                            .ConfigureAwait(false);

            var updateRecordList = new List<ChangeListItem>();

            updateRecordList.AddRange(registeredEpisodes.Select(id => new ChangeListItem
            {
                Type = UpdateRecordType.Episode,
                TheTvDbID = id,
                EpisodeIDs = Array.Empty<int>(),
                LastUpdated = updates.First(u => u.Id == id).LastUpdated.ToDateTime(),
            }));

            var unknownUpdates = updates.Where(update => !registeredEpisodes.Contains(update.Id) && !registeredShows.Contains(update.Id))
                                        .ToArray();

            var unknownRecords = (await Task.WhenAll(unknownUpdates.Select(this.ResolveUnknownSeries).ToArray()).ConfigureAwait(false))
                                 .Where(record => record != null)
                                 .ToList();

            updateRecordList.AddRange(unknownRecords);

            return updateRecordList;
        }

        private static bool IsValidSeries(Series series)
        {
            return !string.IsNullOrWhiteSpace(series?.SeriesName) && !series.SeriesName.StartsWith("***Duplicate");
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
        }

        private async Task<ChangeListItem> GetSeriesRecord(int seriesId, DateTime lastUpdated)
        {
            var series = await this.GetExternalShowAsync(seriesId).ConfigureAwait(false);

            if (IsValidSeries(series))
            {
                var episodes = await this.Client.Series.GetBasicEpisodesAsync(seriesId).ConfigureAwait(false);

                return new ChangeListItem
                {
                    Type = UpdateRecordType.Show,
                    TheTvDbID = seriesId,
                    EpisodeIDs = episodes.Select(e => e.Id).ToArray(),
                    LastUpdated = lastUpdated
                };
            }

            return null;
        }

        private async Task<ChangeListItem> ResolveUnknownSeries(Update update)
        {
            int seriesID = update.Id;

            var episode = await this.GetExternalEpisodeAsync(update.Id).ConfigureAwait(false);
            if (episode != null && int.TryParse(episode.SeriesId, out var episodeSeriesID))
            {
                seriesID = episodeSeriesID;
            }

            var record = await this.GetSeriesRecord(seriesID, update.LastUpdated.ToDateTime()).ConfigureAwait(false);

            return record;
        }
    }

    public class ChangeListItem
    {
        public int[] EpisodeIDs { get; set; }

        public DateTime LastUpdated { get; set; }

        public int TheTvDbID { get; set; }

        public UpdateRecordType Type { get; set; }
    }

    public enum UpdateRecordType
    {
        Episode,

        Show
    }
}