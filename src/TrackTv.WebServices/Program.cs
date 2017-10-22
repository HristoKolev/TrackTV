namespace TrackTv.WebServices
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;

    using TrackTv.WebServices.Infrastructure;

    public class Program
    {
        public static void Main()
        {
            Global.AppConfig = StartupConfig.BuildConfig(new ConfigurationBuilder());

            var host = StartupConfig.BuildHost(new WebHostBuilder(), Global.AppConfig);

            host.Run();
        }
    }
}