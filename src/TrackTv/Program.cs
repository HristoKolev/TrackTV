namespace TrackTv
{
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Newtonsoft.Json;

    using TrackTv.DataRetrieval;

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
                var fetcher = new Fetcher(context, client);

                await fetcher.AddShowAsync(267440);
            }
        }

        private static async Task<TvDbClient> CreateClient()
        {
            // ReSharper disable once StyleCop.SA1305
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

        private static T ReadConfig<T>(string path) => JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
    }
}