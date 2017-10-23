namespace TrackTv.WebServices
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Server.Kestrel.Core;
    using Microsoft.Extensions.Configuration;

    using TrackTv.WebServices.Infrastructure;

    public class Program
    {
        private const string ConfigFile = "appsettings.json";

        private static IConfigurationRoot BuildConfig(IConfigurationBuilder builder)
        {
            return builder.SetBasePath(Global.ConfigDirectory)
                .AddJsonFile(ConfigFile, optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public static void Main()
        {
            var config = BuildConfig(new ConfigurationBuilder());

            Global.AppConfig = config;

            var builder = new WebHostBuilder();

            builder.UseKestrel(options => ConfigureKestrel(options, config))
                .UseContentRoot(Global.RootDirectory)
                .UseUrls(config["Server:Urls"])
                .UseIISIntegration()
                .UseConfiguration(config)
                .UseStartup<Startup>()
                .Build()
                .Run();
        }

        private static void ConfigureKestrel(KestrelServerOptions options, IConfiguration config)
        {
            bool.TryParse(config["Kestrel:AddServerHeader"], out bool addServerHeader);

            options.AddServerHeader = addServerHeader;
        }
    }
}