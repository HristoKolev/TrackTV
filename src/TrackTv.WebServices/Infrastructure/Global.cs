namespace TrackTv.WebServices.Infrastructure
{
    using System.IO;
    using System.Runtime.InteropServices;

    public static class Global
    {
        public static AppConfig AppConfig { get; set; }

        public static string DataDirectory
        {
            get
            {
                #pragma warning disable 162
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                if (Debug || RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    return RootDirectory;
                }

                return Path.Combine(RootDirectory, "/data");
                #pragma warning restore 162
            }
        }

        public static string RootDirectory => Directory.GetCurrentDirectory();

        #if DEBUG
        public const bool Debug = true;
        #else
        public const bool Debug = false;
        #endif
    }
}