namespace TrackTv.Updater
{
    using System;
    using System.Collections.Generic;

    using CommandLine;

    public class CliOptions
    {
        [Option('c', "compile-only", HelpText = "Only compile changes.", Required = false)]
        public bool CompileOnly { get; set; }

        [Option('r', "restart-threshold", HelpText = "The number of changes to apply before restarting.", Required = false)]
        public int RestartThreshold { get; set; }

        [Option('s', "skip-failed", HelpText = "Skip failed changes.", Required = false)]
        public bool SkipFailed { get; set; }

        public static CliOptions Read(string[] args)
        {
            CliOptions options = null;

            IEnumerable<Error> errors = null;

            var result = Parser.Default.ParseArguments<CliOptions>(args);

            result.WithParsed(opt => options = opt).WithNotParsed(e => errors = e);

            if (errors != null)
            {
                Environment.Exit(0);
            }

            return options;
        }
    }
}