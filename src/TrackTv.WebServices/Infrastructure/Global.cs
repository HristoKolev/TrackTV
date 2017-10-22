namespace TrackTv.WebServices.Infrastructure
{
    using System.IO;
    using System.Runtime.InteropServices;

    using Microsoft.Extensions.Configuration;

    public static class Global
    {
        public static IConfigurationRoot AppConfig { get; set; }

        public static string GetConfigDirectory()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                       ? Directory.GetCurrentDirectory()
                       : Path.Combine(Directory.GetCurrentDirectory(), "../");
        }
    }
}