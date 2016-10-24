namespace TrackTV.Data.Repositories.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TrackTv.Models;

    public interface IEpisodeRepository
    {
        Task<Episode> GetEpisodeById(int id);

        Task<List<Episode>> GetEpisodesByShowIdAsync(int id);

        Task<List<Episode>> GetEpisodesByTvDbIdsAsync(int[] ids);
    }
}