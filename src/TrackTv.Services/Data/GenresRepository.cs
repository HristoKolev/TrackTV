﻿namespace TrackTv.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Data;
    using TrackTv.Data.Models;

    public class GenresRepository : IGenresRepository
    {
        public GenresRepository(TrackTvDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        private TrackTvDbContext DbContext { get; }

        public Task<bool> GenreExistsAsync(int genreId)
        {
            return this.DbContext.Genres.AnyAsync(g => g.Id == genreId);
        }

        public Task<Genre[]> GetGenresAsync()
        {
            return this.DbContext.Genres.AsNoTracking().ToArrayAsync();
        }
    }
}