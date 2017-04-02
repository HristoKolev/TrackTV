namespace TrackTv.Services.Subscription
{
    using System.Threading.Tasks;

    using TrackTv.Services.Data;

    public class SubscriptionService : ISubscriptionService
    {
        public SubscriptionService(IProfilesRepository profilesRepository)
        {
            this.ProfilesRepository = profilesRepository;
        }

        private IProfilesRepository ProfilesRepository { get; }

        public Task Subscribe(int profileId, int showId)
        {
            return this.ProfilesRepository.AddSubscriptionAsync(profileId, showId);
        }

        public Task UnSubscribe(int profileId, int showId)
        {
            return this.ProfilesRepository.RemoveSubscriptionAsync(profileId, showId);
        }
    }
}