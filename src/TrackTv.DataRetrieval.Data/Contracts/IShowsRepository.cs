namespace TrackTv.DataRetrieval.Data.Contracts
{
    using System.Threading.Tasks;

    using TrackTv.Models;

    public interface IShowsRepository
    {
        Task<Show> GetFullShowByIdAsync(int id);

        Task<Show[]> GetFullShowsByTheTvDbIdsAsync(int[] theTvDbIds);
    }
}