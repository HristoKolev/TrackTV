namespace TrackTv.Services.Tests
{
    using System.Threading.Tasks;

    using NSubstitute;

    using TrackTv.Data.Models;
    using TrackTv.Services.Data;
    using TrackTv.Services.Subscription;

    using Xunit;

    public class SubscriptionServiceTest
    {
        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task Subscribe_adds_a_subscription_with_the_userId_and_showId()
        {
            var repo = Substitute.For<ISubscriptionRepository>();

            var service = CreateService(repo);

            await service.Subscribe(1, 2).ConfigureAwait(false);

            await repo.Received().AddSubscriptionAsync(1, 2).ConfigureAwait(false);
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task Unsubscribe_removes_a_subscription_with_the_userId_and_showId()
        {
            var repo = Substitute.For<ISubscriptionRepository>();

            var subscription = new Subscription(1, 2)
            {
                Id = 42
            };

            repo.GetSubscriptionAsync(1, 2).Returns(subscription);

            var service = CreateService(repo);

            await service.Unsubscribe(1, 2).ConfigureAwait(false);

            await repo.Received().RemoveSubscriptionAsync(subscription.Id).ConfigureAwait(false);
        }

        private static ISubscriptionService CreateService(ISubscriptionRepository subscriptionRepository)
        {
            return new SubscriptionService(subscriptionRepository);
        }
    }
}