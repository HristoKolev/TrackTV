namespace TrackTv
{
    using System.Globalization;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Configuration;
    using TrackTv.Data;
    using TrackTv.Services.Calendar;
    using TrackTv.Services.Data;
    using TrackTv.Services.Subscription;

    public class ServicesProgram
    {
        public async Task DoAsync()
        {
            using (var context = await CreateContext().ConfigureAwait(false))
            {
                var episodeRepo = new EpisodeRepository(context);
                var showsRepo = new ShowsRepository(context);
                var subscriptionRepo = new SubscriptionRepository(context);

                var calendar = new GregorianCalendar();
                var episodeCalendar = new EpisodeCalendar(episodeRepo, calendar);

                var service = new SubscriptionService(subscriptionRepo);

                await service.Subscribe(5, 8).ConfigureAwait(false);

                // Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
            }
        }

        private static async Task<TrackTvDbContext> CreateContext()
        {
            var configurator = new DbContextConfigurator();

            var context = new TrackTvDbContext(configurator.GetOptions());

            await context.Database.MigrateAsync().ConfigureAwait(false);

            configurator.AttachLogger<SqlLoggerProvider>(context);

            return context;
        }
    }
}