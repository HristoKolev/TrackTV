namespace TrackTV.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query;

    using TrackTv.Data;

    using TrackTV.Data.Repositories.Contracts;
    using TrackTV.Data.Repositories.Exceptions;
    using TrackTV.Data.Repositories.Models;

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

        public Task<Show> GetFullShowByIdAsync(int id)
        {
            return this.FullShows().FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Show[]> GetFullShowsByTheTvDbIdsAsync(int[] theTvDbIds)
        {
            return this.FullShows().Where(x => theTvDbIds.Contains(x.TheTvDbId)).ToArrayAsync();
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