namespace TrackTv.WebServices
{
    using System.IO;
    using System.Net;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Server.Kestrel.Core;

    using Newtonsoft.Json;

    using Npgsql;

    using TrackTv.WebServices.Infrastructure;

    public class Program
    {
        private const string ConfigFile = "appsettings.json";

        public static void Main()
        {
            Global.AppConfig = JsonConvert.DeserializeObject<AppConfig>(File.ReadAllText(Path.Join(Global.DataDirectory, ConfigFile)));
            Global.AppConfig.ConnectionString = CreateConnectionString(Global.AppConfig.ConnectionString);

            var builder = new WebHostBuilder();

            builder.UseKestrel(ConfigureKestrel)
                   .UseContentRoot(Global.RootDirectory)
                   .UseStartup<Startup>()
                   .Build()
                   .Run();
        }

        private static void ConfigureKestrel(KestrelServerOptions opt)
        {
            opt.AddServerHeader = false;
            opt.Listen(IPAddress.Any, Global.AppConfig.Port);
        }

        private static string CreateConnectionString(string connectionString)
        {
            var builder = new NpgsqlConnectionStringBuilder(connectionString)
            {
                Enlist = false,
                NoResetOnClose = true,
            };

            return builder.ToString();
        }
    }

    public class AppConfig
    {
        public string ConnectionString { get; set; }

        public string[] CorsUrls { get; set; }

        public int Port { get; set; }

        public string AspNetLoggingLevel { get; set; }
    }
}