namespace TrackTV.DataRetrieval.ClientExtensions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TvDbSharper.Clients.Episodes;
    using TvDbSharper.Clients.Episodes.Json;

    public static class EpisodeClientExtensions
    {
        public static async Task<IEnumerable<EpisodeRecord>> GetFullEpisodesAsync(this IEpisodesClient client, IEnumerable<int> ids)
        {
            var episodes = await Task.WhenAll(ids.Select(client.GetAsync));

            return episodes.Select(x => x.Data);
        }
    }
}