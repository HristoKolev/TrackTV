namespace TrackTv.WebServices.BackgroundTasks
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;

    using TrackTv.Data.Enums;
    using TrackTv.DataRetrieval;
    using TrackTv.WebServices.Infrastructure;

    [RetryTaskError(3 * 1000)]
    public class UpdateShowsTask : BackgroundTask
    {
        private IHostingEnvironment HostingEnvironment { get; }

        public UpdateShowsTask(IHostingEnvironment hostingEnvironment)
        {
            this.HostingEnvironment = hostingEnvironment;
        }

        protected override async Task ExecuteTaskAsync(CancellationToken stoppingToken)
        {
            if (this.HostingEnvironment.IsDevelopment())
            {
                return;
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                int databaseUpdateInterval;

                using (var container = Global.Container.CreateChildContainer())
                {
                    var errorHandler = container.GetInstance<ErrorHandler>();

                    var settingsService = container.GetInstance<SettingsService>();

                    if (!bool.Parse(await settingsService.GetSettingAsync(Setting.DisableDatabaseUpdate).ConfigureAwait(false)))
                    {
                        var lastUpdated = DateTime
                                          .Parse(await settingsService.GetSettingAsync(Setting.LastDatabaseUpdate).ConfigureAwait(false))
                                          .ToUniversalTime();

                        var synchronizer = container.GetInstance<DataSynchronizer>();

                        await synchronizer.UpdateAllAsync(lastUpdated,
                                              async ex => await errorHandler.HandleErrorAsync(ex).ConfigureAwait(false),
                                              async time => await settingsService
                                                                  .SetSettingAsync(Setting.LastDatabaseUpdate, time.ToString("O"))
                                                                  .ConfigureAwait(false))
                                          .ConfigureAwait(false);
                    }

                    databaseUpdateInterval =
                        int.Parse(await settingsService.GetSettingAsync(Setting.DatabaseUpdateInterval).ConfigureAwait(false));
                }

                await Task.Delay(TimeSpan.FromMinutes(databaseUpdateInterval), stoppingToken).ConfigureAwait(false);
            }
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