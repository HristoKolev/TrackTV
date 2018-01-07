namespace TrackTv.WebServices.Infrastructure
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Hosting;

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
                    await Task.Run(async () => await this.ExecuteTaskAsync(stoppingToken), stoppingToken);

                    return;
                }
                catch (Exception ex)
                {
#pragma warning disable 4014
                    Global.ErrorHandler.HandleErrorAsync(ex);
#pragma warning restore 4014

                    await Task.Delay(this.errorPauseTime, stoppingToken);
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

    public class CronJobSchedulerTask : BackgroundTask
    {
        private readonly Dictionary<string, List<ICronTask>> listeners;

        private readonly TimeSpan oneMinute = new TimeSpan(0, 1, 0);

        private readonly ConcurrentQueue<Func<Task>> runningTasks = new ConcurrentQueue<Func<Task>>();

        private DateTime lastUpdatedTime;

        public CronJobSchedulerTask(IServiceProvider serviceProvider, ErrorHandler errorHandler)
        {
            this.ServiceProvider = serviceProvider;
            this.ErrorHandler = errorHandler;

            this.lastUpdatedTime = DateTime.Now;
            this.listeners = this.GetListeners();
        }

        private ErrorHandler ErrorHandler { get; }

        private IServiceProvider ServiceProvider { get; }

        protected override async Task ExecuteTaskAsync(CancellationToken stoppingToken)
        {
#pragma warning disable 4014
            Task.Run(async () =>
#pragma warning restore 4014
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    while (this.runningTasks.TryDequeue(out var task))
                    {
                        await task().ConfigureAwait(false);
                    }

                    await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken).ConfigureAwait(false);
                }
            }, stoppingToken);

            await Task.Run(async () =>
                      {
                          while (!stoppingToken.IsCancellationRequested)
                          {
                              var now = DateTime.Now;

                              if (now.Subtract(this.lastUpdatedTime) > this.oneMinute)
                              {
                                  this.lastUpdatedTime = now;

                                  this.EnqueueTasks(now);
                              }

                              await Task.Delay(55 * 1000, stoppingToken).ConfigureAwait(false);
                          }
                      }, stoppingToken)
                      .ConfigureAwait(false);
        }

        private void EnqueueTasks(DateTime now)
        {
            var tasks = this.listeners[$"{now.Hour}x{now.Minute}"];

            foreach (var task in tasks)
            {
                this.runningTasks.Enqueue(() => this.ExecuteCronTaskAsync(task, now));
            }
        }

        private async Task ExecuteCronTaskAsync(ICronTask task, DateTime now)
        {
            await Task.Run(async () =>
                      {
                          try
                          {
                              await task.ExecuteTaskAsync(now).ConfigureAwait(false);
                          }
                          catch (Exception ex)
                          {
                              await this.ErrorHandler.HandleErrorAsync(ex).ConfigureAwait(false);
                          }
                      })
                      .ConfigureAwait(false);
        }

        private Dictionary<string, List<ICronTask>> GetListeners()
        {
            var result = new Dictionary<string, List<ICronTask>>();

            var cronTasks = Assembly.GetEntryAssembly()
                                    .DefinedTypes.Select(info => info.AsType())
                                    .Where(type => type.IsClass && typeof(ICronTask).IsAssignableFrom(type))
                                    .ToList();

            foreach (var task in cronTasks)
            {
                var taskAttribute = task.GetCustomAttribute<CronTaskAttribute>();

                if (taskAttribute == null)
                {
                    throw new ApplicationException($"The cron task {task.Name} does not have a {nameof(CronTaskAttribute)}.");
                }

                string key = $"{taskAttribute.Hour}x{taskAttribute.Minute}";

                if (!result.ContainsKey(key))
                {
                    result[key] = new List<ICronTask>();
                }

                result[key].Add(this.ServiceProvider.GetService(task) as ICronTask);
            }

            return result;
        }
    }

    public interface ICronTask
    {
        Task ExecuteTaskAsync(DateTime now);
    }

    public class CronTaskAttribute : Attribute
    {
        public CronTaskAttribute(int hour, int minute)
        {
            this.Hour = hour;
            this.Minute = minute;
        }

        public int Hour { get; }

        public int Minute { get; }
    }
}