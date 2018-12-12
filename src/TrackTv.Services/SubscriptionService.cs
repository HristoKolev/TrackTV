namespace TrackTv.Services
{
    using System;
    using System.Threading.Tasks;

    using TrackTv.Services.Data;

    public class SubscriptionService
    {
        public SubscriptionService(SubscriptionRepository subscriptionRepository)
        {
            this.SubscriptionRepository = subscriptionRepository;
        }

        private SubscriptionRepository SubscriptionRepository { get; }

        public async Task Subscribe(int profileId, int showId)
        {
            if (await this.SubscriptionRepository.IsProfileSubscribedAsync(profileId, showId))
            {
                throw new SubscriptionException($"The user is already subscribed to this show: (ProfileId={profileId}, ShowId={showId})");
            }

            await this.SubscriptionRepository.AddSubscriptionAsync(profileId, showId);
        }

        public async Task Unsubscribe(int profileId, int showId)
        {
            var subscription = await this.SubscriptionRepository.GetSubscriptionAsync(profileId, showId);

            if (subscription == null)
            {
                throw new SubscriptionException(
                    $"The user is not subscribed to the specified show: (ProfileId={profileId}, ShowId={showId})");
            }

            await this.SubscriptionRepository.RemoveSubscriptionAsync(subscription.SubscriptionID);
        }
    }

    public class SubscriptionException : Exception
    {
        public SubscriptionException(string message)
            : base(message)
        {
        }

        public SubscriptionException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}