namespace TrackTV.DataRetrieval.Fetchers
{
    using System.Linq;
    using System.Threading.Tasks;

    using TrackTV.DataRetrieval.ClientExtensions;
    using TrackTV.DataRetrieval.Fetchers.Contracts;

    using TrackTv.Models;

    using TvDbSharper;
    using TvDbSharper.Clients.Episodes.Json;
    using TvDbSharper.Clients.Updates;

    public class EpisodeFetcher : IEpisodeFetcher
    {
        public EpisodeFetcher(ITvDbClient client)
        {
            this.Client = client;
            this.DateParser = new DateParser();
        }

        private ITvDbClient Client { get; }

        private DateParser DateParser { get; }

        public async Task AddAllEpisodesAsync(Show show)
        {
            var ids = await this.GetAllEpisodeIdsAsync(show.TvDbId);

            await this.AddEpisodesAsync(show, ids);
        }

        public async Task AddNewEpisodesAsync(Show show)
        {
            var allIds = await this.GetAllEpisodeIdsAsync(show.TvDbId);

            var existingIds = show.Episodes.Select(x => x.TvDbId);

            var newIds = allIds.Except(existingIds).ToArray();

            await this.AddEpisodesAsync(show, newIds);
        }

        public async Task PopulateEpisodeAsync(Episode episode)
        {
            var response = await this.Client.Episodes.GetAsync(episode.TvDbId);

            this.MapToEpisode(episode, response.Data);
        }

        private async Task AddEpisodesAsync(Show show, int[] ids)
        {
            var records = await this.Client.Episodes.GetFullEpisodesAsync(ids);

            foreach (var record in records)
            {
                var episode = new Episode();

                this.MapToEpisode(episode, record);

                show.Episodes.Add(episode);
            }
        }

        private async Task<int[]> GetAllEpisodeIdsAsync(int seriesId)
        {
            var basicEpisodes = await this.Client.Series.GetBasicEpisodesAsync(seriesId);

            return basicEpisodes.Select(x => x.Id).ToArray();
        }

        private void MapToEpisode(Episode episode, EpisodeRecord data)
        {
            episode.Title = data.EpisodeName;
            episode.Description = data.Overview;
            episode.ImdbId = data.ImdbId;
            episode.Number = data.AiredEpisodeNumber.Value;
            episode.SeasonNumber = data.AiredSeason.Value;
            episode.TvDbId = data.Id;

            if (!string.IsNullOrWhiteSpace(data.FirstAired))
            {
                episode.FirstAired = this.DateParser.ParseFirstAired(data.FirstAired);
            }

            episode.LastUpdated = LongExtensions.ToDateTime(data.LastUpdated);
        }
    }
}