﻿namespace TrackTV.DataRetrieval
{
    using System;
    using System.Threading.Tasks;

    public interface IFetcher
    {
        Task AddShowAsync(int theTvDbId);

        Task UpdateAllRecordsAsync(DateTime from);

        Task UpdateEpisodeAsync(int id);

        Task UpdateEpisodesAsync(int showId);

        Task UpdateShowAsync(int id);
    }
}