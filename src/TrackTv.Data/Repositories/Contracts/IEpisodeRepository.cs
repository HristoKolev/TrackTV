namespace TrackTV.Data.Repositories.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TrackTv.Data.Repositories.Models;
    using TrackTv.Models;

    public interface IEpisodeRepository
    {
        Task<Episode> GetEpisodeById(int id);

        Task<List<Episode>> GetEpisodesByShowIdAsync(int id);

        Task<List<Episode>> GetEpisodesByTheTvDbIdsAsync(int[] ids);

        Task<EpisodesSummary[]> GetEpisodesSummariesAsync(int[] ids, DateTime time);
    }
}