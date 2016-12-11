namespace TrackTv.DataRetrieval.Data
{
    using System.Threading.Tasks;

    using TrackTv.Models;

    public interface IEpisodeRepository
    {
        Task<Episode> GetEpisodeById(int id);

        Task<Episode[]> GetEpisodesByShowIdAsync(int id);

        Task<Episode[]> GetEpisodesByTheTvDbIdsAsync(int[] ids);
    }
}