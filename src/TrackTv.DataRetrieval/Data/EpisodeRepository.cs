﻿namespace TrackTv.DataRetrieval.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Data;
    using TrackTv.Data.Models;

    public class EpisodeRepository  
    {
        public EpisodeRepository(TrackTvDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        private TrackTvDbContext DbContext { get; }

        public Task<Episode> GetEpisodeById(int id)
        {
            return this.DbContext.Episodes.FirstOrDefaultAsync(x => x.EpisodeId == id);
        }

        public Task<Episode[]> GetEpisodesByShowIdAsync(int id)
        {
            return this.DbContext.Episodes.Where(x => x.ShowId == id).ToArrayAsync();
        }

        public Task<Episode[]> GetEpisodesByTheTvDbIdsAsync(int[] ids)
        {
            return this.DbContext.Episodes.Where(x => ids.Contains(x.TheTvDbId)).ToArrayAsync();
        }
    }
}