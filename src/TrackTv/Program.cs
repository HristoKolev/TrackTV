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
                var data = ReadConfig<AuthenticationData>("thetvdb.json");

                // ReSharper disable once StyleCop.SA1305
                var tvDbClient = new TvDbClient();

                await tvDbClient.Authentication.AuthenticateAsync(data);

                var fetcher = new Fetcher(context, tvDbClient);

                await fetcher.AddShow("tt4574334");
            }
        }

        private static async Task<TrackTvDbContext> CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder();

            optionsBuilder.UseSqlServer(@"Server=.;Database=TrackTvDb;Trusted_Connection=True;MultipleActiveResultSets=True;");

            var context = new TrackTvDbContext(optionsBuilder.Options);

            await context.Database.MigrateAsync();

            return context;
        }

        private static T ReadConfig<T>(string path) => JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
    }
}