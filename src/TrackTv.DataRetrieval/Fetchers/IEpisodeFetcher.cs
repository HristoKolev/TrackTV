namespace TrackTv.DataRetrieval.Fetchers
{
    using System.Threading.Tasks;

    using TrackTv.Data.Models;

    public interface IEpisodeFetcher
    {
        Task AddAllEpisodesAsync(Show show);

        Task AddNewEpisodesAsync(Show show);

        Task PopulateEpisodeAsync(Episode episode);
    }
}