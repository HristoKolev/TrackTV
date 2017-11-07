namespace TrackTv.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Data;
    using TrackTv.Data.Models;
    using TrackTv.Services.Data.Models;

    public class ShowsRepository : IShowsRepository
    {
        public ShowsRepository(TrackTvDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        private TrackTvDbContext DbContext { get; }

        public Task<int> CountAllAsync(string showName, int? genreId)
        {
            var query = (IQueryable<Show>)this.DbContext.Shows;

            if (!string.IsNullOrWhiteSpace(showName))
            {
                query = query.Where(show => show.ShowName.Contains(showName));
            }

            if (genreId.HasValue)
            {
                query = query.Where(show => show.ShowsGenres.Any(g => g.GenreId == genreId));
            }

            return query.CountAsync();
        }

        public async Task<SubscriberSummary[]> CountSubscribersAsync(int[] showIds)
        {
            var summaries = await this.DbContext.Shows.Where(x => showIds.Contains(x.ShowId))
                                      .Select(x => new
                                      {
                                          x.ShowId,
                                          SubscriberCount = x.Subscriptions.Count()
                                      })
                                      .ToArrayAsync()
                                      .ConfigureAwait(false);

            return summaries.Select(s => new SubscriberSummary
                            {
                                ShowId = s.ShowId,
                                SubscriberCount = s.SubscriberCount
                            })
                            .ToArray();
        }

        public Task<Show[]> GetShowsAsync(string showName, int? genreId, int page, int pageSize)
        {
            var query = this.DbContext.Shows.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(showName))
            {
                query = query.Where(show => show.ShowName.Contains(showName));
            }

            if (genreId.HasValue)
            {
                query = query.Where(show => show.ShowsGenres.Any(g => g.GenreId == genreId));
            }

            return query.OrderByDescending(show => show.Subscriptions.Count()).Page(page, pageSize).ToArrayAsync();
        }

        public Task<Show> GetShowWithNetworkByIdAsync(int showId)
        {
            return this.DbContext.Shows.AsNoTracking().Include(x => x.Network).FirstOrDefaultAsync(x => x.ShowId == showId);
        }
    }
}