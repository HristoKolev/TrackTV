namespace TrackTv.WebServices.Infrastructure
{
    using System.Globalization;

    using Microsoft.EntityFrameworkCore;

    using StructureMap;

    using TrackTv.Data;
    using TrackTv.Services.Calendar;
    using TrackTv.Services.Data;
    using TrackTv.Services.MyShows;
    using TrackTv.Services.Profile;
    using TrackTv.Services.Show;
    using TrackTv.Services.Shows;
    using TrackTv.Services.Subscription;

    public class ContainerRegistry : Registry
    {
        public ContainerRegistry()
        {
            this.For<ISubscriptionService>().Use<SubscriptionService>();
            this.For<IProfilesRepository>().Use<ProfilesRepository>();

            this.For<IShowsRepository>().Use<ShowsRepository>();
            this.For<IShowsService>().Use<ShowsService>();

            this.For<IShowService>().Use<ShowService>();

            this.For<IEpisodeRepository>().Use<EpisodeRepository>();
            this.For<IMyShowsService>().Use<MyShowsService>();

            this.For<Calendar>().Use<GregorianCalendar>();
            this.For<IEpisodeCalendar>().Use<EpisodeCalendar>();
            this.For<ICalendarService>().Use<CalendarService>();

            this.For<IProfileService>().Use<ProfileService>();

            this.ForConcreteType<ApplicationDbContext>().Configure.ContainerScoped();

            this.Forward<ApplicationDbContext, TrackTvDbContext>();
            this.Forward<ApplicationDbContext, DbContext>();

            this.For<ITransactionScopeFactory>().Use<TransactionScopeFactory>().ContainerScoped();
        }
    }
}