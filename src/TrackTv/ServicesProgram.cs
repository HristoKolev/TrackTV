namespace TrackTv
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Newtonsoft.Json;

    using TrackTv.Configuration;
    using TrackTv.Data;
    using TrackTv.Services.Calendar;
    using TrackTv.Services.Data;

    public class ServicesProgram
    {
        public async Task DoAsync()
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