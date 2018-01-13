namespace TrackTv.Services.Shows
{
    using System.Linq;
    using System.Threading.Tasks;

    using LinqToDB;

    using TrackTv.Data;

    public class ShowsRepository
    {
        public ShowsRepository(IDbService dbService)
        {
            this.DbService = dbService;
        }

        private IDbService DbService { get; }

        public Task<int> CountAllAsync(string showName, int? genreId)
        {
            var showsQuery = this.DbService.Shows;

            if (!string.IsNullOrWhiteSpace(showName))
            {
                showsQuery = showsQuery.Where(show => show.ShowName.Contains(showName));
            }

            if (!genreId.HasValue)
            {
                return showsQuery.CountAsync();
            }

            return (from show in showsQuery
                    join showGenre in this.DbService.ShowsGenres on show.ShowId equals showGenre.ShowId
                    where showGenre.GenreId == genreId
                    select show).CountAsync();
        }

        public async Task<SubscriberSummary[]> CountSubscribersAsync(int[] showIds)
        {
            return await (from show in this.DbService.Shows
                          where showIds.Contains(show.ShowId)
                          select new SubscriberSummary
                          {
                              ShowId = show.ShowId,
                              SubscriberCount = this.DbService.Subscriptions.Count(poco => poco.ShowId == show.ShowId)
                          }).ToArrayAsync()
                            .ConfigureAwait(false);
        }

        public async Task<ShowPoco[]> GetShowsAsync(string showName, int? genreId, int page, int pageSize)
        {
            var showQuery = this.DbService.Shows;

            if (!string.IsNullOrWhiteSpace(showName))
            {
                showQuery = showQuery.Where(show => show.ShowName.Contains(showName));
            }

            var showGenres = this.DbService.ShowsGenres;

            if (genreId.HasValue)
            {
                showGenres = showGenres.Where(g => g.GenreId == genreId);
            }

            var shows = await (from show in showQuery
                               join showGenre in showGenres on show.ShowId equals showGenre.ShowId
                               select show).Distinct()
                                           .OrderByDescending(poco => this.DbService.Subscriptions.Count(s => s.ShowId == poco.ShowId))
                                           .Page(page, pageSize)
                                           .ToArrayAsync()
                                           .ConfigureAwait(false);

            return shows;
        }
    }
}