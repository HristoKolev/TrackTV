namespace TrackTv.Services.Shows
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TrackTv.Data;
    using TrackTv.Services.Data;

    public class ShowsService
    {
        private const int DefaultPageSize = 50;

        public ShowsService(ShowsRepository showsRepository)
        {
            this.ShowsRepository = showsRepository;
        }

        private ShowsRepository ShowsRepository { get; }

        public async Task<PagedResponse<ShowSummary[]>> GetShowsAsync(
            string showName,
            int? genreId,
            int page = 1,
            int pageSize = DefaultPageSize)
        {
            var shows = await this.ShowsRepository.GetShowsAsync(showName, genreId, page, pageSize).ConfigureAwait(false);

            var subscriberCounts =
                await this.ShowsRepository.CountSubscribersAsync(shows.Select(x => x.ShowId).ToArray()).ConfigureAwait(false);

            int totalCount = await this.ShowsRepository.CountAllAsync(showName, genreId).ConfigureAwait(false);

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
                ImdbId = show.ImdbId,
                ShowStatus = (ShowStatus)show.ShowStatus,
                SubscriberCount = subscriberCounts.First(x => x.ShowId == show.ShowId).SubscriberCount,
                ShowId = show.ShowId
            };
        }
    }
}