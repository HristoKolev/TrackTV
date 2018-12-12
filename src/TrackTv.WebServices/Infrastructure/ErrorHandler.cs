namespace TrackTv.WebServices.Infrastructure
{
    using System;
    using System.Threading.Tasks;

    using log4net;

    public class ErrorHandler
    {
        public ErrorHandler(ILog log)
        {
            this.Log = log;
        }

        private ILog Log { get; }

        #pragma warning disable 1998
        public async Task HandleErrorAsync(Exception ex)
            #pragma warning restore 1998
        {
            try
            {
                this.Log.Error($"Exception was handled. (ExceptionMessage: {ex.Message}, ExceptionName: {ex.GetType().Name})");
            }
            catch (Exception exception)
            {
                this.Log.Error("\r\n\r\n" + "Exception occured while handling an exception.\r\n\r\n" + $"Original exception: {ex}\r\n\r\n"
                               + $"Error handler exception: {exception}");
            }
        }
    }
}