namespace TrackTv.Updater.Infrastructure
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
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

        public static CliOptions CliOptions { get; set; }

        public static void Restart()
        {
            Log.Debug("Restarting application...");

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = Environment.GetCommandLineArgs().First(),
                    Arguments = string.Join(" ", Environment.GetCommandLineArgs().Skip(1).ToArray()),
                }
            };

            process.Start();

            Environment.Exit(0);
        }
    }
}