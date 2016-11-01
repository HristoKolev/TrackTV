namespace TrackTV.DataRetrieval.ClientExtensions
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TvDbSharper.Clients.Series.Json;

    public interface IAdvancedSeriesClient
    {
        Task<IEnumerable<BasicEpisode>> GetBasicEpisodesAsync(int seriesId);
    }
}