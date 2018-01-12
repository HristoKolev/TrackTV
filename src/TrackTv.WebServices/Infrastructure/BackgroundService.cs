// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace TrackTv.WebServices.Infrastructure
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Base class for implementing a long running <see cref="IHostedService"/>.
    /// </summary>
    public abstract class BackgroundService : IHostedService, IDisposable
    {
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();

        private Task _executingTask;

        public virtual void Dispose()
        {
            this._stoppingCts.Cancel();
        }

        /// <summary>
        /// Triggered when the application host is ready to start the service.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            // Store the task we're executing
            this._executingTask = this.ExecuteAsync(this._stoppingCts.Token);

            // If the task is completed then return it, this will bubble cancellation and failure to the caller
            if (this._executingTask.IsCompleted)
            {
                return this._executingTask;
            }

            // Otherwise it's running
            return Task.CompletedTask;
        }

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            // Stop called without start
            if (this._executingTask == null)
            {
                return;
            }

            try
            {
                // Signal cancellation to the executing method
                this._stoppingCts.Cancel();
            }
            finally
            {
                // Wait until the task completes or the stop token triggers
                await Task.WhenAny(this._executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
            }
        }

        /// <summary>
        /// This method is called when the <see cref="IHostedService"/> starts. The implementation should return a task that represents
        /// the lifetime of the long running operation(s) being performed.
        /// </summary>
        /// <param name="stoppingToken">Triggered when <see cref="IHostedService.StopAsync(CancellationToken)"/> is called.</param>
        /// <returns>A <see cref="Task"/> that represents the long running operations.</returns>
        protected abstract Task ExecuteAsync(CancellationToken stoppingToken);
    }
}