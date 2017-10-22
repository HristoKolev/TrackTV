namespace TrackTv.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using TrackTv.Data.Models;
    using TrackTv.Services.MyShows.Models;

    public interface IEpisodeRepository
    {
        Task<Episode[]> GetMonthlyEpisodesAsync(int profileId, DateTime startDay, DateTime endDay);

        Task<MyShow[]> GetEpisodesSummariesAsync(int[] showIds, DateTime time);
    }
}