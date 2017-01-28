using System.Reflection;

namespace TrackTv.DataRetrieval.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.Query;

    using TrackTv.Data;
    using TrackTv.Models;

    public class ShowsRepository : IShowsRepository
    {
        public ShowsRepository(ICoreDataStore dataStore)
        {
            this.DataStore = dataStore;
        }

        private ICoreDataStore DataStore { get; }

        public async Task AddShowAsync(Show show)
        {
            this.DataStore.Shows.Add(show);

            await this.DataStore.SaveChangesAsync();
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
            this.DataStore.ChangeTracker.TrackGraph(show, node =>
            {
                var entity = node.Entry.Entity;

                int id = (int)entity.GetType().GetProperty("Id").GetValue(entity);

                node.Entry.State = id == default(int) ? EntityState.Added : EntityState.Modified;
            });

            await this.DataStore.SaveChangesAsync();
        }

        private IIncludableQueryable<Show, ICollection<Episode>> FullShows()
        {
            return
                this.DataStore.Shows.Include(x => x.ShowsGenres)
                    .Include(x => x.ShowsActors)
                    .Include(x => x.Network)
                    .Include(x => x.Episodes);
        }
    }
}