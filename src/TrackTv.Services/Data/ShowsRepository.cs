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

        public Task<int> CountAllAsync()
        {
            return this.DbContext.Shows.CountAsync();
        }

        public Task<int> CountAllResultsAsync(string query)
        {
            return this.DbContext.Shows.CountAsync(x => x.ShowName.ToLower().Contains(query.ToLower()));
        }

        public Task<int> CountByGenreAsync(int genreId)
        {
            return this.DbContext.ShowsGenres.Where(x => x.Genre.GenreId == genreId).CountAsync();
        }

        public async Task<SubscriberSummary[]> CountSubscribersAsync(int[] showIds)
        {
            var summaries = await this.DbContext.Shows.Where(x => showIds.Contains(x.ShowId))
                                      .Select(x => new
                                      {
                                          ShowId = x.ShowId,
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

        public Task<Show> GetShowWithNetworkByIdAsync(int showId)
        {
            return this.DbContext.Shows.AsNoTracking().Include(x => x.Network).FirstOrDefaultAsync(x => x.ShowId == showId);
        }

        public Task<Show[]> GetTopAsync(int page, int pageSize)
        {
            return this.DbContext.Shows.AsNoTracking()
                       .OrderByDescending(show => show.Subscriptions.Count())
                       .Page(page, pageSize)
                       .ToArrayAsync();
        }

        public Task<Show[]> GetTopByGenreAsync(int genreId, int page, int pageSize)
        {
            return this.DbContext.Shows.AsNoTracking()
                       .Where(x => x.ShowsGenres.Any(g => g.Genre.GenreId == genreId))
                       .OrderByDescending(show => show.Subscriptions.Count())
                       .Page(page, pageSize)
                       .ToArrayAsync();
        }

        public Task<Show[]> SearchTopAsync(string query, int page, int pageSize)
        {
            return this.DbContext.Shows.AsNoTracking()
                       .Where(x => x.ShowName.ToLower().Contains(query.ToLower()))
                       .OrderByDescending(show => show.Subscriptions.Count())
                       .Page(page, pageSize)
                       .ToArrayAsync();
        }
    }
}