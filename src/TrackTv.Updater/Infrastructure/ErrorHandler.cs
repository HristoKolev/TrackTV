﻿namespace TrackTv.Updater.Infrastructure
{
    using System;
    using System.Threading.Tasks;

    using log4net;

    using SharpRaven;
    using SharpRaven.Data;

    public class ErrorHandler
    {
        public ErrorHandler(ILog log, IRavenClient ravenClient)
        {
            this.Log = log;

            this.RavenClient = ravenClient;
        }

        private ILog Log { get; }

        private IRavenClient RavenClient { get; }

        public async Task HandleErrorAsync(Exception ex)
        {
            try
            {
                this.Log.Error($"Exception was handled. (ExceptionMessage: {ex.Message}, ExceptionName: {ex.GetType().Name})");

                await this.RavenClient.CaptureAsync(new SentryEvent(ex));
            }
            catch (Exception exception)
            {
                this.Log.Error("\r\n\r\n" + "Exception occured while handling an exception.\r\n\r\n" + $"Original exception: {ex}\r\n\r\n"
                               + $"Error handler exception: {exception}");
            }
        }
    }
}