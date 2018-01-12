namespace TrackTv.WebServices.Infrastructure.IocConfig
{
    using System;

    using StructureMap;
    using StructureMap.Pipeline;

    /// <summary>
    /// Holds instances for a specified period of time. It is thread safe.
    /// </summary>
    public class TimedLifecycle : ILifecycle
    {
        private readonly IObjectCache cache;

        private readonly TimeSpan expirationTime;

        private readonly object syncLock;

        private DateTime lastExpired;

        public TimedLifecycle(TimeSpan expirationTime)
        {
            this.expirationTime = expirationTime;
            this.cache = new LifecycleObjectCache();
            this.syncLock = new object();
            this.lastExpired = DateTime.UtcNow;
        }

        public string Description => "Holds instances for a specified period of time.";

        public void EjectAll(ILifecycleContext context)
        {
            // ReSharper disable once InconsistentlySynchronizedField
            this.cache.DisposeAndClear();
        }

        public IObjectCache FindCache(ILifecycleContext context)
        {
            var now = DateTime.UtcNow;

            if (now.AddMilliseconds(-this.expirationTime.TotalMilliseconds) >= this.lastExpired)
            {
                lock (this.syncLock)
                {
                    if (now.AddMilliseconds(-this.expirationTime.TotalMilliseconds) >= this.lastExpired)
                    {
                        this.lastExpired = now;
                        this.cache.DisposeAndClear();
                    }
                }
            }

            return this.cache;
        }
    }

    public static class ExpressedInstanceExtensions
    {
        public static ExpressedInstance<T> TimeScoped<T>(this ExpressedInstance<T> instance)
        {
            return instance.TimeScoped(new TimeSpan(0, 1, 0, 0));
        }

        public static ExpressedInstance<T> TimeScoped<T>(this ExpressedInstance<T> instance, TimeSpan expirationTime)
        {
            instance.SetLifecycleTo(new TimedLifecycle(expirationTime));

            return instance;
        }
    }
}