namespace TrackTv.DataRetrieval.Fetchers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TrackTv.Data.Models;
    using TrackTv.DataRetrieval.ClientExtensions;

    using TvDbSharper;
    using TvDbSharper.Dto;

    public class EpisodeFetcher : IEpisodeFetcher
    {
        public EpisodeFetcher(
            IEpisodesClient episodesClient,
            IAdvancedEpisodeClient advancedEpisodeClient,
            IAdvancedSeriesClient advancedSeriesClient)
        {
            this.EpisodesClient = episodesClient;

            this.AdvancedEpisodeClient = advancedEpisodeClient;
            this.AdvancedSeriesClient = advancedSeriesClient;

            this.DateParser = new DateParser();
        }

        private IAdvancedEpisodeClient AdvancedEpisodeClient { get; }

        private IAdvancedSeriesClient AdvancedSeriesClient { get; }

        private DateParser DateParser { get; }

        private IEpisodesClient EpisodesClient { get; }

        public async Task AddAllEpisodesAsync(Show show)
        {
            var ids = await this.GetAllEpisodeIdsAsync(show.TheTvDbId).ConfigureAwait(false);

            await this.AddEpisodesAsync(show, ids).ConfigureAwait(false);
        }

        public async Task AddNewEpisodesAsync(Show show)
        {
            var ids = await this.GetAllEpisodeIdsAsync(show.TheTvDbId).ConfigureAwait(false);

            var existingIds = show.Episodes.Select(x => x.TheTvDbId);

            var newIds = ids.Except(existingIds).ToArray();

            await this.AddEpisodesAsync(show, newIds).ConfigureAwait(false);
        }

        public async Task PopulateEpisodeAsync(Episode episode)
        {
            var response = await this.EpisodesClient.GetAsync(episode.TheTvDbId).ConfigureAwait(false);

            this.MapToEpisode(episode, response.Data);
        }

        private async Task AddEpisodesAsync(Show show, IEnumerable<int> ids)
        {
            var records = await this.AdvancedEpisodeClient.GetFullEpisodesAsync(ids).ConfigureAwait(false);

            foreach (var record in records)
            {
                var episode = new Episode();

                if (!record.AiredSeason.HasValue)
                {
                    continue;
                }

                this.MapToEpisode(episode, record);

                show.Episodes.Add(episode);
            }
        }

        private async Task<int[]> GetAllEpisodeIdsAsync(int seriesId)
        {
            var basicEpisodes = await this.AdvancedSeriesClient.GetBasicEpisodesAsync(seriesId).ConfigureAwait(false);

            return basicEpisodes.Select(x => x.Id).ToArray();
        }

        private void MapToEpisode(Episode episode, EpisodeRecord data)
        {
            episode.Title = data.EpisodeName;
            episode.Description = data.Overview;
            episode.ImdbId = data.ImdbId;
            episode.Number = data.AiredEpisodeNumber.Value;
            episode.SeasonNumber = data.AiredSeason.Value;
            episode.TheTvDbId = data.Id;

            if (!string.IsNullOrWhiteSpace(data.FirstAired))
            {
                episode.FirstAired = this.DateParser.ParseFirstAired(data.FirstAired);
            }

            episode.LastUpdated = data.LastUpdated.ToDateTime();
        }
    }
}