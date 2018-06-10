﻿namespace TrackTv.Updater
{
    using System;
    using System.Collections.Generic;

    using CommandLine;

    public class CliOptions
    {
        [Option('c', "compile-only", HelpText = "Only updates change lists. Does not apply any changes.", Required = false)]
        public bool CompileOnly { get; set; }
 
        [Option('s', "skip-failed", HelpText = "Skip failed changes.", Required = false)]
        public bool SkipFailed { get; set; }

        [Option('a', "apply-only", HelpText = "Only applies pending changes. It does not update the change lists.", Required = false)]
        public bool ApplyOnly { get; set; }

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