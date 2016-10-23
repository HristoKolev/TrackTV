namespace TrackTv.DataRetrieval
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TrackTv.Models;
    using TrackTv.Models.Contracts;
    using TrackTv.Repositories;

    using TvDbSharper;
    using TvDbSharper.Clients.Updates;
    using TvDbSharper.Clients.Updates.Json;

    public class Fetcher : IFetcher
    {
        public Fetcher(TrackTvDbContext context, ITvDbClient client)
        {
            this.Context = context;
            this.Client = client;

            this.EpisodeFetcher = new EpisodeFetcher(client);
            this.ActorFetcher = new ActorFetcher(new ActorsRepository(context), client);
            this.GenreFetcher = new GenreFetcher(new GenresRepository(context));
            this.ShowFetcher = new ShowFetcher(new NetworkRepository(context));
            this.ShowsRepository = new ShowsRepository(context);
            this.EpisodeRepository = new EpisodeRepository(context);
        }

        private ActorFetcher ActorFetcher { get; }

        private ITvDbClient Client { get; }

        private TrackTvDbContext Context { get; }

        private EpisodeFetcher EpisodeFetcher { get; }

        private EpisodeRepository EpisodeRepository { get; }

        private GenreFetcher GenreFetcher { get; }

        private ShowFetcher ShowFetcher { get; }

        private ShowsRepository ShowsRepository { get; }

        public async Task AddShowAsync(int theTvDbId)
        {
            var show = new Show
            {
                TvDbId = theTvDbId
            };

            await this.PopulateShowAsync(show);

            await this.EpisodeFetcher.AddAllEpisodesAsync(show);

            this.Context.Shows.Add(show);

            await this.Context.SaveChangesAsync();
        }

        public async Task UpdateAllRecordsAsync(DateTime from)
        {
            var updates = await this.Client.Updates.GetAsync(from);

            var ids = updates.Data.Select(x => x.Id).ToArray();

            var shows = await this.ShowsRepository.GetFullShowsByTheTvDbIdsAsync(ids);

            foreach (var show in shows.Where(x => IsOutdated(x, updates.Data)))
            {
                await this.PopulateShowAsync(show);

                await this.EpisodeFetcher.AddNewEpisodesAsync(show);
            }

            var episodes = await this.EpisodeRepository.GetEpisodesByTvDbIdsAsync(ids);

            foreach (var episode in episodes.Where(x => IsOutdated(x, updates.Data)))
            {
                await this.EpisodeFetcher.PopulateEpisodeAsync(episode);
            }

            await this.Context.SaveChangesAsync();
        }

        public async Task UpdateEpisodeAsync(int id)
        {
            var episode = await this.EpisodeRepository.GetEpisodeById(id);

            await this.EpisodeFetcher.PopulateEpisodeAsync(episode);

            await this.Context.SaveChangesAsync();
        }

        public async Task UpdateEpisodesAsync(int showId)
        {
            var episodes = await this.EpisodeRepository.GetEpisodesByShowIdAsync(showId);

            var tasks = new List<Task>();

            foreach (var episode in episodes)
            {
                tasks.Add(this.EpisodeFetcher.PopulateEpisodeAsync(episode));
            }

            Task.WaitAll(tasks.ToArray());

            await this.Context.SaveChangesAsync();
        }

        public async Task UpdateShowAsync(int id)
        {
            var show = await this.ShowsRepository.GetFullShowById(id);

            await this.PopulateShowAsync(show);

            await this.EpisodeFetcher.AddNewEpisodesAsync(show);

            await this.Context.SaveChangesAsync();
        }

        private static bool IsOutdated(ITvDbRecord record, IEnumerable<Update> updates)
        {
            long? time = updates.First(x => x.Id == record.TvDbId).LastUpdated;

            return time.ToDateTime() > record.LastUpdated;
        }

        private async Task PopulateShowAsync(Show show)
        {
            var response = await this.Client.Series.GetAsync(show.TvDbId);

            await this.ShowFetcher.PopulateShowAsync(show, response);

            await this.GenreFetcher.PopulateGenresAsync(show, response.Data.Genre);

            await this.ActorFetcher.PopulateActorsAsync(show);
        }
    }
}