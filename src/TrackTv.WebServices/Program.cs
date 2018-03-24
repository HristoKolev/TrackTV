namespace TrackTv.WebServices
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;

    using TrackTv.WebServices.Infrastructure;

    public class Program
    {
        private const string ConfigFile = "appsettings.json";

        public static void Main()
        {
            var config = BuildConfig(new ConfigurationBuilder());

            Global.AppConfig = config;

            var builder = new WebHostBuilder();

            builder.UseKestrel(opt => opt.AddServerHeader = false)
                   .UseContentRoot(Global.RootDirectory)
                   .UseUrls(config["Server:Urls"])
                   .UseIISIntegration()
                   .UseConfiguration(config)
                   .UseStartup<Startup>()
                   .Build()
                   .Run();
        }

        private static IConfigurationRoot BuildConfig(IConfigurationBuilder builder)
        {
            return builder.SetBasePath(Global.ConfigDirectory)
                          .AddJsonFile(ConfigFile, optional: true, reloadOnChange: true)
                          .AddEnvironmentVariables()
                          .Build();
        }
    }
}