namespace TrackTv.Services.Show
{
    using System.Threading.Tasks;

    using TrackTv.Services.Data;
    using TrackTv.Services.Exceptions;

    public class ShowService
    {
        public ShowService(
            ShowRepository showRepository,
            SubscriptionRepository subscriptionRepository,
            ProfilesRepository profilesRepository)
        {
            this.ShowRepository = showRepository;
            this.SubscriptionRepository = subscriptionRepository;
            this.ProfilesRepository = profilesRepository;
        }

        private ProfilesRepository ProfilesRepository { get; }

        private ShowRepository ShowRepository { get; }

        private SubscriptionRepository SubscriptionRepository { get; }

        public async Task<FullShow> GetFullShowAsync(int showId, int profileId)
        {
            var show = await this.GetFullShowAsync(showId).ConfigureAwait(false);

            if (!await this.ProfilesRepository.ProfileExistsAsync(profileId).ConfigureAwait(false))
            {
                throw new ProfileNotFoundException(profileId);
            }

            show.IsUserSubscribed = await this.SubscriptionRepository.IsProfileSubscribedAsync(profileId, showId).ConfigureAwait(false);

            return show;
        }

        public async Task<FullShow> GetFullShowAsync(int showId)
        {
            var show = await this.ShowRepository.GetShowWithNetworkByIdAsync(showId).ConfigureAwait(false);

            if (show == null)
            {
                throw new ShowNotFoundException(showId);
            }

            if (show.AirTimeDate.HasValue)
            {
                show.AirTime = new AirTime(show.AirTimeDate.Value.Hour, show.AirTimeDate.Value.Minute);
            }

            return show;
        }
    }
}