namespace TrackTv.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LinqToDB;

    using TrackTv.Data;
    using TrackTv.Services.Data;

    public class ShowsService
    {
        private const int DefaultPageSize = 50;

        public ShowsService(IDbService dbService)
        {
            this.DbService = dbService;
        }

        private IDbService DbService { get; }

        public async Task<PagedResponse<ShowSummary[]>> GetShowsAsync(
            string showName,
            int? genreId,
            int page = 1,
            int pageSize = DefaultPageSize)
        {
            var showQuery = this.DbService.Poco.Shows;

            if (!string.IsNullOrWhiteSpace(showName))
            {
                showQuery = showQuery.Where(show => show.ShowName.Contains(showName));
            }

            var showGenres = this.DbService.Poco.ShowsGenres;

            if (genreId.HasValue)
            {
                showGenres = showGenres.Where(g => g.GenreID == genreId);
            }

            var shows = await (from show in showQuery
                               join showGenre in showGenres on show.ShowID equals showGenre.ShowID
                               select show).Distinct()
                                           .OrderByDescending(poco => this.DbService.Poco.Subscriptions.Count(s => s.ShowID == poco.ShowID))
                                           .Page(page, pageSize)
                                           .ToArrayAsync()
                                           ;

            var subscriberCounts = await this.CountSubscribersAsync(shows.Select(x => x.ShowID).ToArray());

            int totalCount = await this.CountAllAsync(showName, genreId);

            return ConstructResponse(shows, subscriberCounts, totalCount);
        }

        private static PagedResponse<ShowSummary[]> ConstructResponse(
            IEnumerable<ShowPoco> shows,
            IEnumerable<SubscriberSummary> subscriberCounts,
            int totalCount)
        {
            var summaries = shows.Select(show => MapToSummary(show, subscriberCounts)).ToArray();

            return new PagedResponse<ShowSummary[]>
            {
                Data = summaries,
                TotalCount = totalCount
            };
        }

        private static ShowSummary MapToSummary(ShowPoco show, IEnumerable<SubscriberSummary> subscriberCounts)
        {
            return new ShowSummary
            {
                ShowName = show.ShowName,
                ShowBanner = show.ShowBanner,
                ImdbId = show.Imdbid,
                ShowStatus = (ShowStatus)show.ShowStatus,
                SubscriberCount = subscriberCounts.First(x => x.ShowId == show.ShowID).SubscriberCount,
                ShowId = show.ShowID
            };
        }

        private Task<int> CountAllAsync(string showName, int? genreId)
        {
            var showsQuery = this.DbService.Poco.Shows;

            if (!string.IsNullOrWhiteSpace(showName))
            {
                showsQuery = showsQuery.Where(show => show.ShowName.Contains(showName));
            }

            if (!genreId.HasValue)
            {
                return showsQuery.CountAsync();
            }

            return (from show in showsQuery
                    join showGenre in this.DbService.Poco.ShowsGenres on show.ShowID equals showGenre.ShowID
                    where showGenre.GenreID == genreId
                    select show).CountAsync();
        }

        private Task<SubscriberSummary[]> CountSubscribersAsync(int[] showIds)
        {
            return (from show in this.DbService.Poco.Shows
                    where showIds.Contains(show.ShowID)
                    select new SubscriberSummary
                    {
                        ShowId = show.ShowID,
                        SubscriberCount = this.DbService.Poco.Subscriptions.Count(poco => poco.ShowID == show.ShowID)
                    }).ToArrayAsync();
        }
    }

    public class ShowSummary
    {
        public string ImdbId { get; set; }

        public string ShowBanner { get; set; }

        public int ShowId { get; internal set; }

        public string ShowName { get; set; }

        public ShowStatus ShowStatus { get; set; }

        public int SubscriberCount { get; set; }
    }

    public class SubscriberSummary
    {
        public int ShowId { get; set; }

        public int SubscriberCount { get; set; }
    }

    public class PagedResponse<T>
        where T : class
    {
        public T Data { get; set; }

        public int TotalCount { get; set; }
    }
}