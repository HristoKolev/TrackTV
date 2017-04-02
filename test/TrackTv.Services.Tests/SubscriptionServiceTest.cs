namespace TrackTv.Services.Tests
{
    using NSubstitute;

    using TrackTv.Services.Data;
    using TrackTv.Services.Subscription;

    using Xunit;

    public class SubscriptionServiceTest
    {
        [Fact]

        // ReSharper disable once InconsistentNaming
        public void Subscribe_adds_a_subscription_with_the_userId_and_showId()
        {
            var repo = Substitute.For<IProfilesRepository>();

            var service = CreateService(repo);

            service.Subscribe(1, 2);

            repo.Received().AddSubscriptionAsync(1, 2);
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public void Unsubscribe_removes_a_subscription_with_the_userId_and_showId()
        {
            var repo = Substitute.For<IProfilesRepository>();

            var service = CreateService(repo);

            service.Unsubscribe(1, 2);

            repo.Received().RemoveSubscriptionAsync(1, 2);
        }

        private static SubscriptionService CreateService(IProfilesRepository profilesRepository)
        {
            return new SubscriptionService(profilesRepository);
        }
    }
}