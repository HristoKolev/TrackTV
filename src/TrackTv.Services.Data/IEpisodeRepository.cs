﻿namespace TrackTv.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using TrackTv.Models;
    using TrackTv.Services.Data.Models;

    public interface IEpisodeRepository
    {
        Task<EpisodesSummary[]> GetEpisodesSummariesAsync(int[] ids, DateTime time);

        Task<Episode[]> GetMonthlyEpisodesAsync(int userId, DateTime startDay, DateTime endDay);
    }
}