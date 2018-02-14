namespace TrackTv.Updater
{
    using System;
    using System.Threading.Tasks;

    using log4net;

    using TrackTv.Services;

    public class ErrorHandler
    {
        public ErrorHandler(ILog log, MishapService mishapService)
        {
            this.Log = log;
            this.MishapService = mishapService;
        }

        private ILog Log { get; }

        private MishapService MishapService { get; }

        public async Task HandleErrorAsync(Exception ex)
        {
            try
            {
                this.Log.Error($"Exception was handled. (ExceptionMessage: {ex.Message}, ExceptionName: {ex.GetType().Name})");

                await this.MishapService.HandleErrorAsync(ex).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                this.Log.Error("\r\n\r\n" + "Exception occured while handling an exception.\r\n\r\n" + $"Original exception: {ex}\r\n\r\n"
                               + $"Error handler exception: {exception}");
            }
        }
    }
}