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
            this.For<ISubscriptionRepository>().Use<SubscriptionRepository>().ContainerScoped();
            this.For<ISubscriptionService>().Use<SubscriptionService>().ContainerScoped();

            this.For<IProfilesRepository>()
                .Use<CacheProfilesRepository>()
                .ContainerScoped()
                .Ctor<IProfilesRepository>()
                .Is<ProfilesRepository>()
                .ContainerScoped();

            this.For<IProfileService>().Use<ProfileService>().ContainerScoped();

            this.For<IShowsRepository>().Use<ShowsRepository>().ContainerScoped();
            this.For<IShowsService>().Use<ShowsService>().ContainerScoped();

            this.For<IShowService>().Use<ShowService>().ContainerScoped();

            this.For<IEpisodeRepository>().Use<EpisodeRepository>().ContainerScoped();
            this.For<IMyShowsService>().Use<MyShowsService>().ContainerScoped();

            this.For<IGenresRepository>().Use<GenresRepository>().ContainerScoped();
            this.For<IGenresService>().Use<GenresService>().ContainerScoped();

            this.For<Calendar>().Use<GregorianCalendar>().AlwaysUnique();
            this.For<IEpisodeCalendar>().Use<EpisodeCalendar>().ContainerScoped();
            this.For<ICalendarService>().Use<CalendarService>().ContainerScoped();
        }
    }
}