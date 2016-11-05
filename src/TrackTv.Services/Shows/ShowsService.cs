﻿namespace TrackTv.Services.Shows
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TrackTV.Data.Repositories.Contracts;
    using TrackTV.Data.Repositories.Models;

    using TrackTv.Models;
    using TrackTv.Services.Shows.Models;

    public class ShowsService
    {
        private const int DefaultPageSize = 50;

        public ShowsService(IShowsRepository showsRepository)
        {
            this.ShowsRepository = showsRepository;
        }

        private IShowsRepository ShowsRepository { get; }

        public async Task<PagedResponse<ShowSummary[]>> GetByGenreAsync(string genreName, int page = 1, int pageSize = DefaultPageSize)
        {
            var shows = await this.ShowsRepository.GetTopByGenreAsync(genreName, page, pageSize);

            var subscriberCounts = await this.ShowsRepository.CountSubscribersAsync(shows.Select(x => x.Id).ToArray());

            int totalCount = await this.ShowsRepository.CountByGenreAsync(genreName);

            return ConstructResponse(shows, subscriberCounts, totalCount);
        }

        public async Task<PagedResponse<ShowSummary[]>> GetTopShowsAsync(int page = 1, int pageSize = DefaultPageSize)
        {
            var shows = await this.ShowsRepository.GetTopAsync(page, pageSize);

            var subscriberCounts = await this.ShowsRepository.CountSubscribersAsync(shows.Select(x => x.Id).ToArray());

            int totalCount = await this.ShowsRepository.CountAllAsync();

            return ConstructResponse(shows, subscriberCounts, totalCount);
        }

        public async Task<PagedResponse<ShowSummary[]>> SearchTopShowsAsync(string query, int page = 1, int pageSize = DefaultPageSize)
        {
            var shows = await this.ShowsRepository.SearchTopAsync(query, page, pageSize);

            var subscriberCounts = await this.ShowsRepository.CountSubscribersAsync(shows.Select(x => x.Id).ToArray());

            int totalCount = await this.ShowsRepository.CountAllResultsAsync(query);

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
                Name = show.Name,
                Banner = show.Banner,
                ImdbId = show.ImdbId,
                Status = show.Status,
                SubscriberCount = subscriberCounts.First(x => x.ShowId == show.Id).SubscriberCount
            };
        }
    }
}