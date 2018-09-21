namespace TrackTv.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using LinqToDB;

    using TrackTv.Data;
    using TrackTv.Services.Data;
    using TrackTv.Services.Exceptions;

    public class ShowService
    {
        public ShowService(IDbService dbService, SubscriptionRepository subscriptionRepository, ProfilesRepository profilesRepository)
        {
            this.DbService = dbService;
            this.SubscriptionRepository = subscriptionRepository;
            this.ProfilesRepository = profilesRepository;
        }

        private IDbService DbService { get; }

        private ProfilesRepository ProfilesRepository { get; }

        private SubscriptionRepository SubscriptionRepository { get; }

        public async Task<FullShow> GetFullShowAsync(int showId, int profileId)
        {
            var show = await this.GetFullShowAsync(showId);

            if (!await this.ProfilesRepository.ProfileExistsAsync(profileId))
            {
                throw new ProfileNotFoundException(profileId);
            }

            show.IsUserSubscribed = await this.SubscriptionRepository.IsProfileSubscribedAsync(profileId, showId);

            return show;
        }

        public async Task<FullShow> GetFullShowAsync(int showId)
        {
            var show = await this.GetShowWithNetworkByIdAsync(showId);

            if (show == null)
            {
                throw new ShowNotFoundException(showId);
            }

            if (show.AirTimeDate.HasValue)
            {
                show.AirTime = new AirTime
                {
                    Hour = show.AirTimeDate.Value.Hour,
                    Minute = show.AirTimeDate.Value.Minute
                };
            }

            return show;
        }

        private Task<FullShow> GetShowWithNetworkByIdAsync(int showId)
        {
            var result = (from show in this.DbService.Poco.Shows
                          join network in this.DbService.Poco.Networks on show.NetworkID equals network.NetworkID
                          where show.ShowID == showId
                          select new FullShow
                          {
                              ShowId = show.ShowID,
                              TheTvDbId = show.Thetvdbid,
                              ShowName = show.ShowName,
                              FirstAired = show.FirstAired,
                              AirDay = show.AirDay,
                              AirTimeDate = show.AirTime,
                              ImdbId = show.Imdbid,
                              ShowBanner = show.ShowBanner,
                              ShowStatus = show.ShowStatus,
                              ShowDescription = show.ShowDescription,
                              NetworkName = network.NetworkName
                          }).FirstOrDefaultAsync();
            return result;
        }
    }

    public class ShowNotFoundException : Exception
    {
        public ShowNotFoundException(int showId)
            : this($"There is no show with id {showId}")
        {
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public ShowNotFoundException(string message)
            : base(message)
        {
        }

        public ShowNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class FullShow
    {
        public int? AirDay { get; set; }

        public AirTime AirTime { get; set; }

        public DateTime? AirTimeDate { get; set; }

        public DateTime? FirstAired { get; set; }

        public string ImdbId { get; set; }

        public bool IsUserSubscribed { get; set; }

        public string NetworkName { get; set; }

        public string ShowBanner { get; set; }

        public string ShowDescription { get; set; }

        public int ShowId { get; set; }

        public string ShowName { get; set; }

        public int ShowStatus { get; set; }

        public int TheTvDbId { get; set; }
    }

    public class AirTime
    {
        public int Hour { get; set; }

        public int Minute { get; set; }
    }
}