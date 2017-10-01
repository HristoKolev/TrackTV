namespace TrackTv.Services.Show
{
    using System.Threading.Tasks;

    using TrackTv.Services.Data;
    using TrackTv.Services.Exceptions;
    using TrackTv.Services.Show.Models;

    public class ShowService : IShowService
    {
        public ShowService(
            IShowsRepository showsRepository,
            ISubscriptionRepository subscriptionRepository,
            IProfilesRepository profilesRepository)
        {
            this.ShowsRepository = showsRepository;
            this.SubscriptionRepository = subscriptionRepository;
            this.ProfilesRepository = profilesRepository;
        }

        private IProfilesRepository ProfilesRepository { get; }

        private IShowsRepository ShowsRepository { get; }

        private ISubscriptionRepository SubscriptionRepository { get; }

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
            var show = await this.ShowsRepository.GetShowWithNetworkByIdAsync(showId).ConfigureAwait(false);

            if (show == null)
            {
                throw new ShowNotFoundException(showId);
            }

            var model = new FullShow
            {
                ShowId = show.ShowId,
                TheTvDbId = show.TheTvDbId,
                ShowName = show.ShowName,
                FirstAired = show.FirstAired,
                AirDay = show.AirDay,
                ImdbId = show.ImdbId,
                ShowBanner = show.ShowBanner,
                ShowStatus = show.ShowStatus,
                ShowDescription = show.ShowDescription,
                NetworkName = show.Network.NetworkName
            };

            if (show.AirTime.HasValue)
            {
                model.AirTime = new AirTime(show.AirTime.Value.Hour, show.AirTime.Value.Minute);
            }

            return model;
        }
    }
}