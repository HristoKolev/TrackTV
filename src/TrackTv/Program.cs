namespace TrackTv
{
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Newtonsoft.Json;

    using TvDbSharper;
    using TvDbSharper.Clients.Authentication.Json;

    public class Program
    {
        public static void Main(string[] args) => new Program().MainAsync(args).GetAwaiter().GetResult();

        public async Task MainAsync(string[] args)
        {
            using (var context = await CreateContext())
            {
                // ReSharper disable once StyleCop.SA1305
                var tvDbClient = new TvDbClient();

                var authData = ReadConfig<AuthenticationData>("thetvdb.json");

                await tvDbClient.Authentication.AuthenticateAsync(authData);

                var fetcher = new Fetcher(context, tvDbClient);

                await fetcher.AddShowAsync("tt4574334");
            }
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