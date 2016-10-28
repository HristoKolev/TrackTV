﻿namespace TrackTV.DataRetrieval.Fetchers.Contracts
{
    using System.Threading.Tasks;

    using TrackTv.Models;

    public interface IEpisodeFetcher
    {
        Task AddAllEpisodesAsync(Show show);

        Task AddNewEpisodesAsync(Show show);

        Task PopulateEpisodeAsync(Episode episode);
    }
}