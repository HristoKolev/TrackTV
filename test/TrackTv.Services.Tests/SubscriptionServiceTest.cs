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
            var repo = Substitute.For<IProfilesRepository>();

            var service = CreateService(repo);

            await service.Subscribe(1, 2).ConfigureAwait(false);

            await repo.Received().AddSubscriptionAsync(1, 2).ConfigureAwait(false);
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task Unsubscribe_removes_a_subscription_with_the_userId_and_showId()
        {
            var repo = Substitute.For<IProfilesRepository>();

            var subscription = new Subscription(1, 2);

            repo.GetSubscriptionAsync(1, 2).Returns(subscription);

            var service = CreateService(repo);

            await service.Unsubscribe(1, 2).ConfigureAwait(false);

            await repo.Received().RemoveSubscriptionAsync(subscription).ConfigureAwait(false);
        }

        private static SubscriptionService CreateService(IProfilesRepository profilesRepository)
        {
            return new SubscriptionService(profilesRepository);
        }
    }
}