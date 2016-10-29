namespace TrackTV.Data.Repositories.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TrackTv.Models;

    public interface IShowsRepository
    {
        Task<Show> GetFullShowById(int id);

        Task<List<Show>> GetFullShowsByTheTvDbIdsAsync(int[] theTvDbIds);

        Task SaveChangesAsync();
    }
}