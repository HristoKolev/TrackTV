namespace TrackTv.Repositories
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Models;

    public class ShowsRepository
    {
        public ShowsRepository(TrackTvDbContext context)
        {
            this.Context = context;
        }

        private TrackTvDbContext Context { get; }

        public async Task<Show> GetFullShowByTvDbId(int tvdbId)
        {
            return
                await
                    this.Context.Shows.Include(x => x.ShowsGenres)
                        .Include(x => x.ShowsActors)
                        .Include(x => x.Network)
                        .Include(x => x.Episodes)
                        .FirstOrDefaultAsync(x => x.TvDbId == tvdbId);
        }
    }
}