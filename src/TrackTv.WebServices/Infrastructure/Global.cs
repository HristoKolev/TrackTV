namespace TrackTv.WebServices.Infrastructure
{
    using System.IO;
    using System.Runtime.InteropServices;

    public static class Global
    {
        public static AppConfig AppConfig { get; set; }

        public static string ConfigDirectory =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? RootDirectory : Path.Combine(RootDirectory, "../");

        public static string RootDirectory => Directory.GetCurrentDirectory();
    }
}