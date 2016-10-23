namespace TrackTv.DataRetrieval
{
    using System.Linq;
    using System.Threading.Tasks;

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

        public async Task AddAllEpisodesAsync(Show show, int seriesId)
        {
            var ids = await this.GetAllEpisodeIdsAsync(seriesId);

            await this.AddEpisodesAsync(show, ids);
        }

        public async Task AddNewEpisodesAsync(Show show, int seriesId)
        {
            var allIds = await this.GetAllEpisodeIdsAsync(seriesId);
            var existingIds = show.Episodes.Select(x => x.TvDbId);

            var newIds = allIds.Except(existingIds).ToArray();

            await this.AddEpisodesAsync(show, newIds);
        }

        public async Task UpdateEpisodeAsync(Episode episode)
        {
            var response = await this.Client.Episodes.GetAsync(episode.TvDbId);

            long? lastUpdated = response.Data.LastUpdated;

            if (lastUpdated.ToDateTime() > episode.LastUpdated)
            {
                this.MapToEpisode(episode, response.Data);
            }
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

            long? lastUpdated = data.LastUpdated;
            episode.LastUpdated = lastUpdated.ToDateTime();
        }
    }
}