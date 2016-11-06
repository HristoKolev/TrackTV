namespace TrackTv.Data.Repositories
{
    using System;
    using System.Threading.Tasks;

    using TrackTv.Data.Repositories.Models;
    using TrackTv.Models;

    public interface IEpisodeRepository
    {
        Task<EpisodesSummary[]> GetEpisodesSummariesAsync(int[] ids, DateTime time);

        Task<Episode[]> GetMonthlyEpisodesAsync(int userId, DateTime startDay, DateTime endDay);
    }
}