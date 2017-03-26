namespace TrackTv.DataRetrieval.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query;

    using TrackTv.Data;
    using TrackTv.Models;

    public class ShowsRepository : IShowsRepository
    {
        public ShowsRepository(TrackTvDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        private TrackTvDbContext DbContext { get; }

        public async Task AddShowAsync(Show show)
        {
            this.DbContext.Shows.Add(show);

            await this.DbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public Task<Show> GetFullShowByIdAsync(int id)
        {
            return this.FullShows().FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Show[]> GetFullShowsByTheTvDbIdsAsync(int[] theTvDbIds)
        {
            return this.FullShows().Where(x => theTvDbIds.Contains(x.TheTvDbId)).ToArrayAsync();
        }

        public async Task UpdateShowAsync(Show show)
        {
            this.DbContext.ChangeTracker.TrackGraph(show, node =>
            {
                var entity = node.Entry.Entity;

                int id = (int)entity.GetType().GetProperty("Id").GetValue(entity);

                node.Entry.State = id == default(int) ? EntityState.Added : EntityState.Modified;
            });

            await this.DbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        private IIncludableQueryable<Show, ICollection<Episode>> FullShows()
        {
            return
                this.DbContext.Shows.Include(x => x.ShowsGenres)
                    .Include(x => x.ShowsActors)
                    .Include(x => x.Network)
                    .Include(x => x.Episodes);
        }
    }
}