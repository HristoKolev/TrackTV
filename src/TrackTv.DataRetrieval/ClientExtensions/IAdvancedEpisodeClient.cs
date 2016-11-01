namespace TrackTV.DataRetrieval.ClientExtensions
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TvDbSharper.Clients.Episodes.Json;

    public interface IAdvancedEpisodeClient
    {
        Task<IEnumerable<EpisodeRecord>> GetFullEpisodesAsync(IEnumerable<int> ids);
    }
}