namespace TrackTv.DataRetrieval.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query;

    using TrackTv.Data;
    using TrackTv.Data.Models;

    public class ShowsRepository  
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
            return this.FullShows().FirstOrDefaultAsync(x => x.ShowId == id);
        }

        public Task<Show[]> GetFullShowsByTheTvDbIdsAsync(int[] theTvDbIds)
        {
            return this.FullShows().Where(x => theTvDbIds.Contains(x.TheTvDbId)).ToArrayAsync();
        }

        public Task UpdateShowAsync(Show show)
        {
            this.DbContext.ChangeTracker.TrackGraph(show, node =>
            {
                var entity = node.Entry.Entity;

                int showId = (int)entity.GetType().GetProperty(nameof(show.ShowId)).GetValue(entity);

                node.Entry.State = showId == default(int) ? EntityState.Added : EntityState.Modified;
            });

            return this.DbContext.SaveChangesAsync();
        }

        private IIncludableQueryable<Show, ICollection<Episode>> FullShows()
        {
            return
                this.DbContext.Shows.Include(x => x.ShowsGenres)
                    .Include(x => x.Roles)
                    .Include(x => x.Network)
                    .Include(x => x.Episodes);
        }
    }
}