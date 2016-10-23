namespace TrackTv.DataRetrieval
{
    using System.Threading.Tasks;

    using TrackTv.Models;
    using TrackTv.Repositories;

    using TvDbSharper;

    public class Fetcher
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
        }

        private ActorFetcher ActorFetcher { get; }

        private ITvDbClient Client { get; }

        private TrackTvDbContext Context { get; }

        private IEpisodeFetcher EpisodeFetcher { get; }

        private GenreFetcher GenreFetcher { get; }

        private ShowFetcher ShowFetcher { get; }

        private ShowsRepository ShowsRepository { get; }

        public async Task AddShowAsync(int seriesId)
        {
            var show = new Show();

            await this.ProcessShowAsync(show, seriesId);

            await this.EpisodeFetcher.AddAllEpisodesAsync(show, seriesId);

            this.Context.Shows.Add(show);

            await this.Context.SaveChangesAsync();
        }

        public async Task UpdateShowAsync(int seriesId)
        {
            var show = await this.ShowsRepository.GetFullShowByTvDbId(seriesId);

            await this.ProcessShowAsync(show, seriesId);

            await this.EpisodeFetcher.AddNewEpisodesAsync(show, seriesId);

            await this.Context.SaveChangesAsync();
        }

        private async Task ProcessShowAsync(Show show, int seriesId)
        {
            var response = await this.Client.Series.GetAsync(seriesId);

            await this.ShowFetcher.UpdateShowAsync(show, response);

            await this.GenreFetcher.AddGenresAsync(show, response.Data.Genre);

            await this.ActorFetcher.PopulateActorsAsync(show, seriesId);
        }
    }
}