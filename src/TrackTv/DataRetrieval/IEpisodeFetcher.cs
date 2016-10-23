namespace TrackTv.DataRetrieval
{
    using System.Threading.Tasks;

    using TrackTv.Models;

    public interface IEpisodeFetcher
    {
        Task AddAllEpisodesAsync(Show show, int seriesId);

        Task AddNewEpisodesAsync(Show show, int seriesId);

        Task UpdateEpisodeAsync(Episode episode);
    }
}