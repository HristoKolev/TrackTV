namespace TrackTv.Services.Shows
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TrackTv.Data.Models;
    using TrackTv.Services.Data;
    using TrackTv.Services.Data.Models;
    using TrackTv.Services.Shows.Models;

    public class ShowsService 
    {
        private const int DefaultPageSize = 50;

        public ShowsService(ShowsRepository showsRepository)
        {
            this.ShowsRepository = showsRepository;
        }

        private ShowsRepository ShowsRepository { get; }

        public async Task<PagedResponse<ShowSummary[]>> GetShowsAsync(string showName, int? genreId, int page = 1, int pageSize = DefaultPageSize)
        {
            var shows = await this.ShowsRepository.GetShowsAsync(showName, genreId, page, pageSize).ConfigureAwait(false);

            var subscriberCounts =
                await this.ShowsRepository.CountSubscribersAsync(shows.Select(x => x.ShowId).ToArray()).ConfigureAwait(false);

            int totalCount = await this.ShowsRepository.CountAllAsync(showName, genreId).ConfigureAwait(false);

            return ConstructResponse(shows, subscriberCounts, totalCount);
        }

        private static PagedResponse<ShowSummary[]> ConstructResponse(
            IEnumerable<Show> shows,
            IEnumerable<SubscriberSummary> subscriberCounts,
            int totalCount)
        {
            var summaries = shows.Select(show => MapToSummary(show, subscriberCounts)).ToArray();

            return new PagedResponse<ShowSummary[]>(summaries, totalCount);
        }

        private static ShowSummary MapToSummary(Show show, IEnumerable<SubscriberSummary> subscriberCounts)
        {
            return new ShowSummary
            {
                ShowName = show.ShowName,
                ShowBanner = show.ShowBanner,
                ImdbId = show.ImdbId,
                ShowStatus = show.ShowStatus,
                SubscriberCount = subscriberCounts.First(x => x.ShowId == show.ShowId).SubscriberCount,
                ShowId = show.ShowId
            };
        }
    }
}