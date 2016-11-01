namespace TrackTV.DataRetrieval.ClientExtensions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TvDbSharper.Clients.Episodes;
    using TvDbSharper.Clients.Episodes.Json;

    public class AdvancedEpisodeClient : IAdvancedEpisodeClient
    {
        public AdvancedEpisodeClient(IEpisodesClient episodesClient)
        {
            this.EpisodesClient = episodesClient;
        }

        private IEpisodesClient EpisodesClient { get; }

        public async Task<IEnumerable<EpisodeRecord>> GetFullEpisodesAsync(IEnumerable<int> ids)
        {
            var episodes = await Task.WhenAll(ids.Select(this.EpisodesClient.GetAsync));

            return episodes.Select(x => x.Data);
        }
    }
}