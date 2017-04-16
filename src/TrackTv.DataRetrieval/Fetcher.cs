namespace TrackTv.DataRetrieval
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Data;
    using TrackTv.Data.Models;
    using TrackTv.Data.Models.Contracts;
    using TrackTv.DataRetrieval.Data;
    using TrackTv.DataRetrieval.Fetchers;

    using TvDbSharper;
    using TvDbSharper.Clients.Updates;
    using TvDbSharper.Clients.Updates.Json;

    public class Fetcher : IFetcher
    {
        public Fetcher(
            TrackTvDbContext context,
            ITvDbClient client,
            IEpisodeFetcher episodeFetcher,
            IActorFetcher actorFetcher,
            IGenreFetcher genreFetcher,
            IShowFetcher showFetcher,
            IShowsRepository showsRepository,
            IEpisodeRepository episodeRepository,
            ITransactionScopeFactory transactionScopeFactory)
        {
            this.Context = context;
            this.Client = client;
            this.EpisodeFetcher = episodeFetcher;
            this.ActorFetcher = actorFetcher;
            this.GenreFetcher = genreFetcher;
            this.ShowFetcher = showFetcher;
            this.ShowsRepository = showsRepository;
            this.EpisodeRepository = episodeRepository;
            this.TransactionScopeFactory = transactionScopeFactory;
        }

        private IActorFetcher ActorFetcher { get; }

        private ITvDbClient Client { get; }

        private DbContext Context { get; }

        private IEpisodeFetcher EpisodeFetcher { get; }

        private IEpisodeRepository EpisodeRepository { get; }

        private IGenreFetcher GenreFetcher { get; }

        private IShowFetcher ShowFetcher { get; }

        private IShowsRepository ShowsRepository { get; }

        private ITransactionScopeFactory TransactionScopeFactory { get; }

        public async Task AddShowAsync(int theTvDbId)
        {
            using (var scope = this.TransactionScopeFactory.CreateScope())
            {
                var show = new Show
                {
                    TheTvDbId = theTvDbId
                };

                await this.PopulateShowAsync(show).ConfigureAwait(false);

                await this.EpisodeFetcher.AddAllEpisodesAsync(show).ConfigureAwait(false);

                await this.ShowsRepository.AddShowAsync(show).ConfigureAwait(false);

                scope.Complete();
            }
        }

        public async Task UpdateAllRecordsAsync(DateTime from)
        {
            using (var scope = this.TransactionScopeFactory.CreateScope())
            {
                var response = await this.Client.Updates.GetAsync(from).ConfigureAwait(false);

                var ids = response.Data.Select(x => x.Id).ToArray();

                var shows = await this.ShowsRepository.GetFullShowsByTheTvDbIdsAsync(ids).ConfigureAwait(false);

                foreach (var show in shows.Where(x => IsOutdated(x, response.Data)))
                {
                    await this.PopulateShowAsync(show).ConfigureAwait(false);

                    await this.EpisodeFetcher.AddNewEpisodesAsync(show).ConfigureAwait(false);
                }

                var episodes = await this.EpisodeRepository.GetEpisodesByTheTvDbIdsAsync(ids).ConfigureAwait(false);

                foreach (var episode in episodes.Where(x => IsOutdated(x, response.Data)))
                {
                    await this.EpisodeFetcher.PopulateEpisodeAsync(episode).ConfigureAwait(false);
                }

                await this.Context.SaveChangesAsync().ConfigureAwait(false);

                scope.Complete();
            }
        }

        public async Task UpdateEpisodeAsync(int episodeId)
        {
            using (var scope = this.TransactionScopeFactory.CreateScope())
            {
                var episode = await this.EpisodeRepository.GetEpisodeById(episodeId).ConfigureAwait(false);

                await this.EpisodeFetcher.PopulateEpisodeAsync(episode).ConfigureAwait(false);

                await this.Context.SaveChangesAsync().ConfigureAwait(false);

                scope.Complete();
            }
        }

        public async Task UpdateEpisodesAsync(int showId)
        {
            using (var scope = this.TransactionScopeFactory.CreateScope())
            {
                var episodes = await this.EpisodeRepository.GetEpisodesByShowIdAsync(showId).ConfigureAwait(false);

                var tasks = episodes.Select(episode => this.EpisodeFetcher.PopulateEpisodeAsync(episode));

                await Task.WhenAll(tasks).ConfigureAwait(false);

                await this.Context.SaveChangesAsync().ConfigureAwait(false);

                scope.Complete();
            }
        }

        public async Task UpdateShowAsync(int showId)
        {
            using (var scope = this.TransactionScopeFactory.CreateScope())
            {
                var show = await this.ShowsRepository.GetFullShowByIdAsync(showId).ConfigureAwait(false);

                await this.PopulateShowAsync(show).ConfigureAwait(false);

                await this.EpisodeFetcher.AddNewEpisodesAsync(show).ConfigureAwait(false);

                await this.Context.SaveChangesAsync().ConfigureAwait(false);

                scope.Complete();
            }
        }

        private static bool IsOutdated(ITvDbRecord record, IEnumerable<Update> updates)
        {
            return updates.First(x => x.Id == record.TheTvDbId).LastUpdated.ToDateTime() > record.LastUpdated;
        }

        private async Task PopulateShowAsync(Show show)
        {
            var response = await this.Client.Series.GetAsync(show.TheTvDbId).ConfigureAwait(false);

            await this.ShowFetcher.PopulateShowAsync(show, response.Data).ConfigureAwait(false);

            await this.GenreFetcher.PopulateGenresAsync(show, response.Data.Genre).ConfigureAwait(false);

            await this.ActorFetcher.PopulateActorsAsync(show).ConfigureAwait(false);
        }
    }
}