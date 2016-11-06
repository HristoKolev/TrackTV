namespace TrackTv.DataRetrieval
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TrackTv.Data;
    using TrackTv.DataRetrieval.Data;
    using TrackTv.DataRetrieval.Fetchers;
    using TrackTv.Models;
    using TrackTv.Models.Contracts;

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
            IEpisodeRepository episodeRepository)
        {
            this.Context = context;
            this.Client = client;
            this.EpisodeFetcher = episodeFetcher;
            this.ActorFetcher = actorFetcher;
            this.GenreFetcher = genreFetcher;
            this.ShowFetcher = showFetcher;
            this.ShowsRepository = showsRepository;
            this.EpisodeRepository = episodeRepository;
        }

        private IActorFetcher ActorFetcher { get; }

        private ITvDbClient Client { get; }

        private TrackTvDbContext Context { get; }

        private IEpisodeFetcher EpisodeFetcher { get; }

        private IEpisodeRepository EpisodeRepository { get; }

        private IGenreFetcher GenreFetcher { get; }

        private IShowFetcher ShowFetcher { get; }

        private IShowsRepository ShowsRepository { get; }

        public async Task AddShowAsync(int theTvDbId)
        {
            var show = new Show
            {
                TheTvDbId = theTvDbId
            };

            await this.PopulateShowAsync(show);

            await this.EpisodeFetcher.AddAllEpisodesAsync(show);

            this.Context.Shows.Add(show);

            await this.Context.SaveChangesAsync();
        }

        public async Task UpdateAllRecordsAsync(DateTime from)
        {
            var response = await this.Client.Updates.GetAsync(from);

            var ids = response.Data.Select(x => x.Id).ToArray();

            var shows = await this.ShowsRepository.GetFullShowsByTheTvDbIdsAsync(ids);

            foreach (var show in shows.Where(x => IsOutdated(x, response.Data)))
            {
                await this.PopulateShowAsync(show);

                await this.EpisodeFetcher.AddNewEpisodesAsync(show);
            }

            var episodes = await this.EpisodeRepository.GetEpisodesByTheTvDbIdsAsync(ids);

            foreach (var episode in episodes.Where(x => IsOutdated(x, response.Data)))
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

            await Task.WhenAll(tasks);

            await this.Context.SaveChangesAsync();
        }

        public async Task UpdateShowAsync(int id)
        {
            var show = await this.ShowsRepository.GetFullShowByIdAsync(id);

            await this.PopulateShowAsync(show);

            await this.EpisodeFetcher.AddNewEpisodesAsync(show);

            await this.Context.SaveChangesAsync();
        }

        private static bool IsOutdated(ITvDbRecord record, IEnumerable<Update> updates)
        {
            return updates.First(x => x.Id == record.TheTvDbId).LastUpdated.ToDateTime() > record.LastUpdated;
        }

        private async Task PopulateShowAsync(Show show)
        {
            var response = await this.Client.Series.GetAsync(show.TheTvDbId);

            await this.ShowFetcher.PopulateShowAsync(show, response.Data);

            await this.GenreFetcher.PopulateGenresAsync(show, response.Data.Genre);

            await this.ActorFetcher.PopulateActorsAsync(show);
        }
    }
}