namespace TrackTV.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTV.Data.Repositories.Contracts;

    using TrackTv.Models;

    public class EpisodeRepository : IEpisodeRepository
    {
        public EpisodeRepository(TrackTvDbContext context)
        {
            this.Context = context;
        }

        private TrackTvDbContext Context { get; }

        public async Task<Episode> GetEpisodeById(int id)
        {
            return await this.Context.Episodes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Episode>> GetEpisodesByShowIdAsync(int id)
        {
            return await this.Context.Episodes.Where(x => x.ShowId == id).ToListAsync();
        }

        public async Task<List<Episode>> GetEpisodesByTheTvDbIdsAsync(int[] ids)
        {
            return await this.Context.Episodes.Where(x => ids.Contains(x.TheTvDbId)).ToListAsync();
        }
    }
}