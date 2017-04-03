namespace TrackTv.WebServices.Infrastructure
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    using TrackTv.Data;
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
            services.TryAddTransient<IMyShowsService, MyShowsService>();

            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<TrackTvDbContext, ApplicationDbContext>();
        }
    }
}