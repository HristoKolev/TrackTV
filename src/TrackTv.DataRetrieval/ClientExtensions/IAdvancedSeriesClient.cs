namespace TrackTv.DataRetrieval.ClientExtensions
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TvDbSharper.Dto;

    public interface IAdvancedSeriesClient
    {
        Task<IEnumerable<BasicEpisode>> GetBasicEpisodesAsync(int seriesId);
    }
}