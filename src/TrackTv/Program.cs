namespace TrackTv
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Newtonsoft.Json;

    using TrackTv.Configuration;
    using TrackTv.Services.Show;
    using TrackTv.Services.Shows;

    using TrackTV.Data;
    using TrackTV.Data.Repositories;
    using TrackTV.DataRetrieval;
    using TrackTV.DataRetrieval.ClientExtensions;
    using TrackTV.DataRetrieval.Fetchers;

    using TvDbSharper;
    using TvDbSharper.Clients.Authentication.Json;

    public class Program
    {
        public static void Main(string[] args) => new Program().MainAsync(args).GetAwaiter().GetResult();

        public async Task MainAsync(string[] args)
        {
            var client = await CreateClient();

            using (var context = await CreateContext())
            {
                //var fetcher = CreateFetcher(context, client);

                //await fetcher.AddShowAsync(70851);
                //await fetcher.AddShowAsync(78804);
                //await fetcher.AddShowAsync(83237);
                //await fetcher.AddShowAsync(70851);
                //await fetcher.AddShowAsync(72449);
                //await fetcher.AddShowAsync(82066);
                //await fetcher.AddShowAsync(292124);
                //await fetcher.AddShowAsync(296762);

                var showsRepo = new ShowsRepository(context);
                var usersRepo = new UsersRepository(context);

                var showService = new ShowsService(showsRepo);

                var shows = await showService.GetTopShowsAsync();

                Console.WriteLine(JsonConvert.SerializeObject(shows, Formatting.Indented));

                // await fetcher.UpdateShowAsync(2);
                // await fetcher.UpdateAllRecordsAsync(new DateTime(2016, 10, 19));
            }
        }

        private static async Task<TvDbClient> CreateClient()
        {
            var client = new TvDbClient();

            var authData = ReadConfig<AuthenticationData>("thetvdb.json");

            await client.Authentication.AuthenticateAsync(authData);

            return client;
        }

        private static async Task<TrackTvDbContext> CreateContext()
        {
            var configurator = new DbContextConfigurator();

            var context = new TrackTvDbContext(configurator.GetOptions());

            await context.Database.MigrateAsync();

            configurator.AttachLogger<SqlLoggerProvider>(context);

            return context;
        }

        private static IFetcher CreateFetcher(TrackTvDbContext context, ITvDbClient client)
        {
            var actorsRepository = new ActorsRepository(context);
            var networkRepository = new NetworkRepository(context);
            var episodeRepository = new EpisodeRepository(context);
            var genresRepository = new GenresRepository(context);
            var showsRepository = new ShowsRepository(context);

            var advancedEpisodesClient = new AdvancedEpisodeClient(client.Episodes);
            var advancedSeriesClient = new AdvancedSeriesClient(client.Series);

            var episodeFetcher = new EpisodeFetcher(client.Episodes, advancedEpisodesClient, advancedSeriesClient);
            var actorFetcher = new ActorFetcher(actorsRepository, client.Series);
            var showFetcher = new ShowFetcher(networkRepository);
            var genreFetcher = new GenreFetcher(genresRepository);

            var fetcher = new Fetcher(context, client, episodeFetcher, actorFetcher, genreFetcher, showFetcher, showsRepository,
                episodeRepository);

            return fetcher;
        }

        private static T ReadConfig<T>(string path) => JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
    }
}