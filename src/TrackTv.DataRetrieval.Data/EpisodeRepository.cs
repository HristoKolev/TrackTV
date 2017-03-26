namespace TrackTv.DataRetrieval.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Data;
    using TrackTv.Models;

    public class EpisodeRepository : IEpisodeRepository
    {
        public EpisodeRepository(TrackTvDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        private TrackTvDbContext DbContext { get; }

        public Task<Episode> GetEpisodeById(int id)
        {
            return this.DbContext.Episodes.FirstOrDefaultAsync(x => x.Id == id);
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