namespace TrackTV.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query;

    using TrackTV.Data.Repositories.Contracts;

    using TrackTv.Models;

    public class ShowsRepository : IShowsRepository
    {
        public ShowsRepository(ICoreDataContext context)
        {
            this.Context = context;
        }

        private ICoreDataContext Context { get; }

        public Task<Show> GetFullShowById(int id)
        {
            return this.FullShows().FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Show>> GetFullShowsByTheTvDbIdsAsync(int[] theTvDbIds)
        {
            return this.FullShows().Where(x => theTvDbIds.Contains(x.TheTvDbId)).ToListAsync();
        }

        private IIncludableQueryable<Show, ICollection<Episode>> FullShows()
        {
            return
                this.Context.Shows.Include(x => x.ShowsGenres).Include(x => x.ShowsActors).Include(x => x.Network).Include(x => x.Episodes);
        }

        private Task SaveChangesAsync()
        {
            return this.Context.SaveChangesAsync();
        }
    }
}