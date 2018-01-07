namespace TrackTv.WebServices.BackgroundTasks
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using TrackTv.WebServices.Infrastructure;

    [RetryTaskError(3 * 1000)]
    public class UpdateShowsTask : BackgroundTask
    {
        protected override Task ExecuteTaskAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();

            return Task.CompletedTask;
        }
    }

    // public class UpdateShowsTask1 : BackgroundTask
    // {
    // protected override Task ExecuteTaskAsync(CancellationToken stoppingToken)
    // {
    // return Task.CompletedTask;
    // }
    // }
}