namespace TrackTv.WebServices.Infrastructure
{
    using System.IO;
    using System.Runtime.InteropServices;

    using Microsoft.Extensions.Configuration;

    public static class Global
    {
        public static IConfigurationRoot AppConfig { get; set; }

        public static string ConfigDirectory =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? RootDirectory : Path.Combine(RootDirectory, "../");

        public static ErrorHandler ErrorHandler { get; set; }

        public static string RootDirectory => Directory.GetCurrentDirectory();
    }
}