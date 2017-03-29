namespace TrackTv.DataRetrieval.Data
{
    using System.Threading.Tasks;

    using TrackTv.Data.Models;

    public interface IShowsRepository
    {
        Task AddShowAsync(Show show);

        Task<Show> GetFullShowByIdAsync(int id);

        Task<Show[]> GetFullShowsByTheTvDbIdsAsync(int[] theTvDbIds);
    }
}