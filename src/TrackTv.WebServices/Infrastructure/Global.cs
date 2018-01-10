namespace TrackTv.WebServices.Infrastructure
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;

    using Microsoft.Extensions.Configuration;

    using StructureMap;

    public static class Global
    {
        public static readonly ConcurrentStack<KeyValuePair<string, object>> SqlLog = new ConcurrentStack<KeyValuePair<string, object>>();

        public static IConfigurationRoot AppConfig { get; set; }

        public static string ConfigDirectory =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? RootDirectory : Path.Combine(RootDirectory, "../");

        public static Container Container { get; set; }

        public static ErrorHandler ErrorHandler { get; set; }

        public static string RootDirectory => Directory.GetCurrentDirectory();
    }
}