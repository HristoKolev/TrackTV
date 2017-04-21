namespace TrackTv.Configuration
{
    using System;
    using System.IO;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using Newtonsoft.Json;

    using TrackTv.Data;

    public class DbContextConfigurator
    {
        public void AttachLogger<T>(TrackTvDbContext context)
            where T : ILoggerProvider
        {
            var serviceProvider = context.GetInfrastructure();

            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

            loggerFactory.AddProvider(Activator.CreateInstance<T>());
        }

        public DbContextOptions<TrackTvDbContext> GetOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TrackTvDbContext>();

            var appSettings = ReadConfig<AppSettings>("appsettings.json");

            optionsBuilder.SelectProvider(appSettings.ConnectionString);

            return optionsBuilder.Options;
        }

        private static T ReadConfig<T>(string path) => JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
    }
}