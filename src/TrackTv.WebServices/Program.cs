namespace TrackTv.WebServices
{
    using System.IO;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;

    using TrackTv.WebServices.Infrastructure;

    public class Program
    {
        public static void Main()
        {
            var config = StartupConfig.BuildConfig(new ConfigurationBuilder(), Directory.GetCurrentDirectory());

            Global.AppConfig = config;

            var host = StartupConfig.BuildHost(new WebHostBuilder(), config);

            host.Run();
        }
    }
}