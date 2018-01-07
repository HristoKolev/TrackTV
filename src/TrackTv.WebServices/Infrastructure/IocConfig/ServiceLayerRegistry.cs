namespace TrackTv.WebServices.Infrastructure.IocConfig
{
    using System.Globalization;

    using StructureMap;

    using TrackTv.Services.Calendar;
    using TrackTv.Services.Data;
    using TrackTv.Services.Genres;
    using TrackTv.Services.MyShows;
    using TrackTv.Services.Profile;
    using TrackTv.Services.Show;
    using TrackTv.Services.Shows;
    using TrackTv.Services.Subscription;

    public class ServiceLayerRegistry : Registry
    {
        public ServiceLayerRegistry()
        {
            this.For<SubscriptionRepository>().ContainerScoped();
            this.For<SubscriptionService>().ContainerScoped();

            this.For<ProfilesRepository>().ContainerScoped();
            this.For<ProfileService>().ContainerScoped();

            this.For<ShowsRepository>().ContainerScoped();
            this.For<ShowsService>().ContainerScoped();

            this.For<ShowService>().ContainerScoped();

            this.For<EpisodeRepository>().ContainerScoped();
            this.For<MyShowsService>().ContainerScoped();

            this.For<GenresRepository>().ContainerScoped();
            this.For<GenresService>().ContainerScoped();

            this.For<Calendar>().Use<GregorianCalendar>().AlwaysUnique();
            this.For<EpisodeCalendar>().ContainerScoped();
            this.For<CalendarService>().ContainerScoped();
        }
    }
}