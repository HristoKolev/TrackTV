namespace TrackTv.Data.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Data.Repositories.Exceptions;
    using TrackTv.Data.Repositories.Models;
    using TrackTv.Models;

    public class ShowsRepository : IShowsRepository
    {
        public ShowsRepository(ICoreDataStore dataStore)
        {
            this.DataStore = dataStore;
        }

        private ICoreDataStore DataStore { get; }

        public Task<int> CountAllAsync()
        {
            return this.DataStore.Shows.CountAsync();
        }

        public Task<int> CountAllResultsAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new InvalidQueryException("The query is null or an empty string.");
            }

            return this.DataStore.Shows.CountAsync(x => x.Name.ToLower().Contains(query.ToLower()));
        }

        public Task<int> CountByGenreAsync(string genreName)
        {
            return this.DataStore.ShowsGenres.Where(x => x.Genre.Name.ToLower() == genreName.ToLower()).CountAsync();
        }

        public Task<SubscriberSummary[]> CountSubscribersAsync(int[] ids)
        {
            return this.DataStore.Shows.Where(x => ids.Contains(x.Id)).Select(x => new SubscriberSummary
                       {
                           ShowId = x.Id,
                           SubscriberCount = x.ShowsUsers.Count()
                       }).ToArrayAsync();
        }

        public Task<Show[]> GetShowsByUserIdAsync(int userId)
        {
            return this.DataStore.ShowsUsers.Where(x => x.UserId == userId).Select(x => x.Show).ToArrayAsync();
        }

        public Task<Show> GetShowWithNetworkByIdAsync(int id)
        {
            return this.DataStore.Shows.AsNoTracking().Include(x => x.Network).FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Show[]> GetTopAsync(int page, int pageSize)
        {
            return
                this.DataStore.Shows.AsNoTracking().OrderByDescending(show => show.ShowsUsers.Count()).Page(page, pageSize).ToArrayAsync();
        }

        public Task<Show[]> GetTopByGenreAsync(string genreName, int page, int pageSize)
        {
            return
                this.DataStore.Shows.AsNoTracking()
                    .Where(x => x.ShowsGenres.Any(g => g.Genre.Name.ToLower() == genreName.ToLower()))
                    .OrderByDescending(show => show.ShowsUsers.Count())
                    .Page(page, pageSize)
                    .ToArrayAsync();
        }

        public Task<Show[]> SearchTopAsync(string query, int page, int pageSize)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new InvalidQueryException("The query is null or an empty string.");
            }

            return
                this.DataStore.Shows.AsNoTracking()
                    .Where(x => x.Name.ToLower().Contains(query.ToLower()))
                    .OrderByDescending(show => show.ShowsUsers.Count())
                    .Page(page, pageSize)
                    .ToArrayAsync();
        }
    }
}