namespace TrackTv.WebServices.Infrastructure
{
    using Microsoft.Extensions.DependencyInjection;

    using TrackTv.Data;
    using TrackTv.Services.Data;
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

            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<TrackTvDbContext, ApplicationDbContext>();
        }
    }
}