namespace TrackTv
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Newtonsoft.Json;

    using TrackTv.Configuration;
    using TrackTv.Data;
    using TrackTv.Data.Repositories;
    using TrackTv.Services.Calendar;

    public class Program
    {
        public static void Main(string[] args) => new Program().MainAsync(args).GetAwaiter().GetResult();

        public async Task MainAsync(string[] args)
        {
            using (var context = await CreateContext())
            {
                var episodeRepo = new EpisodeRepository(context);

                var calendar = new GregorianCalendar();
                var episodeCalendar = new EpisodeCalendar(episodeRepo, calendar);

                var service = new CalendarService(episodeCalendar);

                var data = await service.GetCalendarAsync(1);

                Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
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
    }
}