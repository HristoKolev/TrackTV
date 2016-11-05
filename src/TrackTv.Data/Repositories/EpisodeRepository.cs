﻿namespace TrackTV.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTV.Data.Repositories.Contracts;

    using TrackTv.Models;

    public class EpisodeRepository : IEpisodeRepository
    {
        public EpisodeRepository(ICoreDataContext context)
        {
            this.Context = context;
        }

        private ICoreDataContext Context { get; }

        public Task<Episode> GetEpisodeById(int id)
        {
            return this.Context.Episodes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Episode>> GetEpisodesByShowIdAsync(int id)
        {
            return this.Context.Episodes.Where(x => x.ShowId == id).ToListAsync();
        }

        public Task<List<Episode>> GetEpisodesByTheTvDbIdsAsync(int[] ids)
        {
            return this.Context.Episodes.Where(x => ids.Contains(x.TheTvDbId)).ToListAsync();
        }
    }
}