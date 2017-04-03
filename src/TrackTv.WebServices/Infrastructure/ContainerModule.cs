namespace TrackTv.WebServices.Infrastructure
{
    using System.Globalization;

    using Microsoft.Extensions.DependencyInjection;

    using TrackTv.Data;
    using TrackTv.Services.Calendar;
    using TrackTv.Services.Data;
    using TrackTv.Services.MyShows;
    using TrackTv.Services.Show;
    using TrackTv.Services.Shows;
    using TrackTv.Services.Subscription;

    public class ContainerModule
    {
        public void Register(IServiceCollection services)
        {
            services.AddTransient<ISubscriptionService, SubscriptionService>();
            services.AddTransient<IProfilesRepository, ProfilesRepository>();

            services.AddTransient<IShowsRepository, ShowsRepository>();
            services.AddTransient<IShowsService, ShowsService>();

            services.AddTransient<IShowService, ShowService>();

            services.AddTransient<IEpisodeRepository, EpisodeRepository>();
            services.AddTransient<IMyShowsService, MyShowsService>();

            services.AddTransient<Calendar, GregorianCalendar>();
            services.AddTransient<IEpisodeCalendar, EpisodeCalendar>();
            services.AddTransient<ICalendarService, CalendarService>();

            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<TrackTvDbContext, ApplicationDbContext>();
        }
    }
}