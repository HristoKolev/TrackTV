﻿namespace TrackTV.Data.Repositories.Contracts
{
    using System.Threading.Tasks;

    using TrackTv.Models;

    public interface IGenresRepository
    {
        Task<Genre[]> GetGenres(string[] names);
    }
}