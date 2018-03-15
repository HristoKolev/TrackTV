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

        public async Task MergeChangeList(Update[] updates)
        {
            var updateIDs = updates.Select(u => u.Id).ToArray();

            var registeredEpisodeIDs =
                this.DbService.Episodes.Where(p => updateIDs.Contains(p.Thetvdbid)).Select(p => p.Thetvdbid).ToArray();

            foreach (var update in updates)
            {
                await this.MergeEpisode(update, registeredEpisodeIDs).ConfigureAwait(false);

                await this.MergeShow(update).ConfigureAwait(false);
            }
        }

        private async Task MergeShow(Update update)
        {
            var series = await this.GetExternalShowAsync(update.Id).ConfigureAwait(false);

            if (series != null)
            {
                await this.UpdateShow(series).ConfigureAwait(false);
            }
        }

        private async Task UpdateShow(Series series)
        {
            if (IsValidSeries(series))
            {
                var apiChange =
                    await this.DbService.ApiChanges
                              .FirstOrDefaultAsync(p => p.ApiChangeThetvdbid == series.Id && p.ApiChangeType == (int)ApiChangeType.Show)
                              .ConfigureAwait(false) ?? new ApiChangePoco();

                apiChange.ApiChangeType = (int)ApiChangeType.Show;
                apiChange.ApiChangeThetvdbLastUpdated = series.LastUpdated.ToDateTime();
                apiChange.ApiChangeThetvdbid = series.Id;

                if (((IPoco)apiChange).IsNew())
                {
                    apiChange.ApiChangeCreatedDate = DateTime.UtcNow;
                }

                await this.DbService.Save(apiChange).ConfigureAwait(false);
            }
        }

        private async Task MergeEpisode(Update update, int[] registeredEpisodeIDs)
        {
            var episode = await this.GetExternalEpisodeAsync(update.Id).ConfigureAwait(false);

            if (episode != null)
            {
                if (registeredEpisodeIDs.Contains(update.Id))
                {
                    await this.UpdateEpisode(episode).ConfigureAwait(false);
                }
                else
                {
                    int seriesID = int.Parse(episode.SeriesId);

                    var series = await this.GetExternalShowAsync(seriesID).ConfigureAwait(false);

                    if (series != null)
                    {
                        await this.UpdateShow(series).ConfigureAwait(false);
                    }
                }
            }
        }

        private async Task UpdateEpisode(EpisodeRecord episode)
        {
            var apiChange = await this.DbService.ApiChanges
                          .FirstOrDefaultAsync(p => p.ApiChangeThetvdbid == episode.Id && p.ApiChangeType == (int)ApiChangeType.Episode)
                          .ConfigureAwait(false) ?? new ApiChangePoco();

            apiChange.ApiChangeType = (int)ApiChangeType.Episode;
            apiChange.ApiChangeThetvdbLastUpdated = episode.LastUpdated.ToDateTime();
            apiChange.ApiChangeThetvdbid = episode.Id;
            apiChange.ApiChangeAttachedSeriesID = int.Parse(episode.SeriesId);

            if (((IPoco)apiChange).IsNew())
            {
                apiChange.ApiChangeCreatedDate = DateTime.UtcNow;
            }

            await this.DbService.Save(apiChange).ConfigureAwait(false);
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
    }

    public class ChangeListItem
    {
        public int[] EpisodeIDs { get; set; }

        public DateTime LastUpdated { get; set; }

        public int TheTvDbID { get; set; }

        public ApiChangeType Type { get; set; }
    }
}