﻿namespace TrackTv.WebServices.Infrastructure
{
    using System.IO;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Server.Kestrel.Core;
    using Microsoft.Extensions.Configuration;

    public static class StartupConfig
    {
        private const string ConfigFile = "appsettings.json";

        public static IConfigurationRoot BuildConfig(IConfigurationBuilder builder)
        {
            return builder.SetBasePath(Global.GetConfigDirectory())
                          .AddJsonFile(ConfigFile, optional: true, reloadOnChange: true)
                          .AddEnvironmentVariables()
                          .Build();
        }

        public static IWebHost BuildHost(IWebHostBuilder builder, IConfiguration config)
        {
            return builder.UseKestrel(options => ConfigureKestrel(options, config))
                          .UseContentRoot(Directory.GetCurrentDirectory())
                          .UseUrls(config["Server:Urls"])
                          .UseIISIntegration()
                          .UseConfiguration(config)
                          .UseStartup<Startup>()
                          .Build();
        }

        private static void ConfigureKestrel(KestrelServerOptions options, IConfiguration config)
        {
            bool.TryParse(config["Kestrel:AddServerHeader"], out bool addServerHeader);

            options.AddServerHeader = addServerHeader;
        }
    }
}