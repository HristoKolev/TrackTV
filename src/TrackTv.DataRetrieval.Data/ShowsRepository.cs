namespace TrackTv.DataRetrieval.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query;

    using TrackTv.Data;
    using TrackTv.DataRetrieval.Data.Contracts;
    using TrackTv.Models;

    public class ShowsRepository : IShowsRepository
    {
        public ShowsRepository(ICoreDataStore dataStore)
        {
            this.DataStore = dataStore;
        }

        private ICoreDataStore DataStore { get; }

        public Task<Show> GetFullShowByIdAsync(int id)
        {
            return this.FullShows().FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Show[]> GetFullShowsByTheTvDbIdsAsync(int[] theTvDbIds)
        {
            return this.FullShows().Where(x => theTvDbIds.Contains(x.TheTvDbId)).ToArrayAsync();
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