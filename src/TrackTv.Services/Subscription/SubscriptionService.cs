namespace TrackTv.Services.Subscription
{
    using System.Threading.Tasks;

    using TrackTv.Services.Data;
    using TrackTv.Services.Data.Exceptions;

    public class SubscriptionService : ISubscriptionService
    {
        public SubscriptionService(ISubscriptionRepository subscriptionRepository)
        {
            this.SubscriptionRepository = subscriptionRepository;
        }

        private ISubscriptionRepository SubscriptionRepository { get; }

        public async Task Subscribe(int profileId, int showId)
        {
            CheckInput(profileId, showId);

            if (await this.SubscriptionRepository.IsUserSubscribedAsync(profileId, showId).ConfigureAwait(false))
            {
                throw new SubscriptionException($"The user is already subscribed to this show: (ProfileId={profileId}, ShowId={showId})");
            }

            await this.SubscriptionRepository.AddSubscriptionAsync(profileId, showId).ConfigureAwait(false);
        }

        public async Task Unsubscribe(int profileId, int showId)
        {
            CheckInput(profileId, showId);

            var subscription = await this.SubscriptionRepository.GetSubscriptionAsync(profileId, showId).ConfigureAwait(false);

            if (subscription == null)
            {
                throw new SubscriptionException(
                    $"The user is not subscribed to the specified show: (ProfileId={profileId}, ShowId={showId})");
            }

            await this.SubscriptionRepository.RemoveSubscriptionAsync(subscription.Id).ConfigureAwait(false);
        }

        private static void CheckInput(int profileId, int showId)
        {
            if (profileId == default(int))
            {
                throw new SubscriptionException("Invalid ProfileId.");
            }

            if (showId == default(int))
            {
                throw new SubscriptionException("Invalid ShowId.");
            }
        }
    }
}