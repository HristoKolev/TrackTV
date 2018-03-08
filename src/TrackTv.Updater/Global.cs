﻿namespace TrackTv.Updater
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;

    using log4net;

    public static class Global
    {
        public static string ConfigDirectory =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? Environment.CurrentDirectory : Path.Combine(RootDirectory, "../");

        public static ErrorHandler ErrorHandler { get; set; }

        public static string RootDirectory => Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        public static AppConfigModel AppConfig { get; set; }

        public static ILog Log { get; set; }
    }
}