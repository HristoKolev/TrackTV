namespace TrackTv.Configuration
{
    using System;

    using Microsoft.Extensions.Logging;

    public class SqlLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new SqlLogger();
        }

        public void Dispose()
        {
            // N/A
        }

        private class SqlLogger : ILogger
        {
            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public void Log<TState>(
                LogLevel logLevel,
                EventId eventId,
                TState state,
                Exception exception,
                Func<TState, Exception, string> formatter)
            {
                if (eventId.Id == 1)
                {
                    string message = formatter(state, exception);

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine();
                    Console.WriteLine(message);
                    Console.WriteLine();
                    Console.ResetColor();
                }

                // File.AppendAllText(@".\sql.log", formatter(state, exception));
            }
        }
    }
}