namespace TrackTv.WebServices.Infrastructure
{
    using System.IO;

    public static class Global
    {
        public static AppConfig AppConfig { get; set; }

        public static string ConfigDirectory => Debug ? RootDirectory : Path.Combine(RootDirectory, "../");

        public static string RootDirectory => Directory.GetCurrentDirectory();

        #if DEBUG
        public const bool Debug = true;
        #else
        public const bool Debug = false;
        #endif
    }
}