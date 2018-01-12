namespace TrackTv.WebServices.Infrastructure
{
    using System;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class BackgroundTask : BackgroundService
    {
        private readonly TimeSpan errorPauseTime;

        protected BackgroundTask()
        {
            var attribute = this.GetType().GetCustomAttribute<RetryTaskErrorAttribute>();

            if (attribute != null)
            {
                this.errorPauseTime = TimeSpan.FromMilliseconds(attribute.Interval);
            }
            else
            {
                this.errorPauseTime = TimeSpan.FromSeconds(10);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Run(async () => await this.ExecuteTaskAsync(stoppingToken).ConfigureAwait(false), stoppingToken)
                              .ConfigureAwait(false);

                    return;
                }
                catch (Exception ex)
                {
                    #pragma warning disable 4014
                    Global.ErrorHandler.HandleErrorAsync(ex);
                    #pragma warning restore 4014

                    await Task.Delay(this.errorPauseTime, stoppingToken).ConfigureAwait(false);
                }
            }
        }

        protected abstract Task ExecuteTaskAsync(CancellationToken stoppingToken);
    }

    public class RetryTaskErrorAttribute : Attribute
    {
        public RetryTaskErrorAttribute(int interval)
        {
            this.Interval = interval;
        }

        public int Interval { get; }
    }
}