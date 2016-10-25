﻿namespace TrackTv
{
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Newtonsoft.Json;

    using TrackTv.Configuration;

    using TrackTV.Data;
    using TrackTV.Data.Repositories;
    using TrackTV.DataRetrieval;
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
                var fetcher = CreateFetcher(context, client);

                await fetcher.AddShowAsync(70851);

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

            var episodeFetcher = new EpisodeFetcher(client);
            var actorFetcher = new ActorFetcher(actorsRepository, client);
            var showFetcher = new ShowFetcher(networkRepository);
            var genreFetcher = new GenreFetcher(genresRepository);

            var fetcher = new Fetcher(context, client, episodeFetcher, actorFetcher, genreFetcher, showFetcher, showsRepository,
                episodeRepository);

            return fetcher;
        }

        private static T ReadConfig<T>(string path) => JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
    }
}