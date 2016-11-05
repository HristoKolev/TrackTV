﻿namespace TrackTV.DataRetrieval.Fetchers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TrackTV.DataRetrieval.ClientExtensions;
    using TrackTV.DataRetrieval.Fetchers.Contracts;

    using TrackTv.Models;

    using TvDbSharper.Clients.Episodes;
    using TvDbSharper.Clients.Episodes.Json;
    using TvDbSharper.Clients.Updates;

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
            var ids = await this.GetAllEpisodeIdsAsync(show.TheTvDbId);

            await this.AddEpisodesAsync(show, ids);
        }

        public async Task AddNewEpisodesAsync(Show show)
        {
            var ids = await this.GetAllEpisodeIdsAsync(show.TheTvDbId);

            var existingIds = show.Episodes.Select(x => x.TheTvDbId);

            var newIds = ids.Except(existingIds).ToArray();

            await this.AddEpisodesAsync(show, newIds);
        }

        public async Task PopulateEpisodeAsync(Episode episode)
        {
            var response = await this.EpisodesClient.GetAsync(episode.TheTvDbId);

            this.MapToEpisode(episode, response.Data);
        }

        private async Task AddEpisodesAsync(Show show, IEnumerable<int> ids)
        {
            var records = await this.AdvancedEpisodeClient.GetFullEpisodesAsync(ids);

            foreach (var record in records)
            {
                var episode = new Episode();

                this.MapToEpisode(episode, record);

                show.Episodes.Add(episode);
            }
        }

        private async Task<int[]> GetAllEpisodeIdsAsync(int seriesId)
        {
            var basicEpisodes = await this.AdvancedSeriesClient.GetBasicEpisodesAsync(seriesId);

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