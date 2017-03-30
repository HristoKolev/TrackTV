namespace TrackTv.Services.Tests.Data
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Data.Models;
    using TrackTv.Services.Data;
    using TrackTv.Services.Data.Exceptions;

    using Xunit;

    public class UsersRepositoryTest : BaseRepositoryTest
    {
        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task AddSubscriptionAsync_subscribes_the_user_to_the_show()
        {
            using (var context = CreateContext())
            {
                context.Shows.Add(new Show
                {
                    Id = 1
                });
                context.Profiles.Add(new Profile
                {
                    Id = 2
                });

                await context.SaveChangesAsync().ConfigureAwait(false);

                var repository = new ProfilesRepository(context);

                await repository.AddSubscriptionAsync(2, 1).ConfigureAwait(false);

                var relationship =
                    await context.ShowsProfiles.FirstOrDefaultAsync(showsUsers => showsUsers.ShowId == 1 && showsUsers.ProfileId == 2)
                                 .ConfigureAwait(false);

                Assert.NotNull(relationship);
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task AddSubscriptionAsync_throws_if_the_user_is_already_subscribed()
        {
            using (var context = CreateContext())
            {
                var repository = new ProfilesRepository(context);

                var show = new Show
                {
                    Id = 1
                };

                var user = new Profile
                {
                    Id = 2
                };

                context.Shows.Add(show);
                context.Profiles.Add(user);
                context.ShowsProfiles.Add(new ShowsProfiles(user.Id, show.Id));

                await context.SaveChangesAsync().ConfigureAwait(false);

                await Assert.ThrowsAsync<SubscriptionException>(
                    async () => await repository.AddSubscriptionAsync(2, 1).ConfigureAwait(false)).ConfigureAwait(false);
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task IsUserSubscribedAsync_returns_true_if_there_is_a_relationship()
        {
            using (var context = CreateContext())
            {
                var show = new Show
                {
                    Id = 1
                };

                var user = new Profile
                {
                    Id = 2
                };

                context.Shows.Add(show);
                context.Profiles.Add(user);
                context.ShowsProfiles.Add(new ShowsProfiles(user.Id, show.Id));

                await context.SaveChangesAsync().ConfigureAwait(false);

                var repository = new ProfilesRepository(context);

                Assert.True(await repository.IsUserSubscribedAsync(2, 1).ConfigureAwait(false));
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task IsUserSubscribedAsync_returns_true_if_there_is_no_relationship()
        {
            using (var context = CreateContext())
            {
                var repository = new ProfilesRepository(context);

                Assert.False(await repository.IsUserSubscribedAsync(2, 1).ConfigureAwait(false));
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task RemoveSubscriptionAsync_removes_the_subscription_if_it_exists()
        {
            using (var context = CreateContext())
            {
                var show = new Show
                {
                    Id = 1
                };

                var user = new Profile
                {
                    Id = 2
                };

                context.Shows.Add(show);
                context.Profiles.Add(user);
                context.ShowsProfiles.Add(new ShowsProfiles(user.Id, show.Id));

                await context.SaveChangesAsync().ConfigureAwait(false);

                var repository = new ProfilesRepository(context);

                await repository.RemoveSubscriptionAsync(2, 1).ConfigureAwait(false);

                Assert.Equal(0, await context.ShowsProfiles.CountAsync().ConfigureAwait(false));
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task RemoveSubscriptionAsync_throws_if_the_user_is_not_subscribed()
        {
            using (var context = CreateContext())
            {
                var repository = new ProfilesRepository(context);

                await Assert.ThrowsAsync<SubscriptionException>(
                    async () => await repository.RemoveSubscriptionAsync(2, 1).ConfigureAwait(false)).ConfigureAwait(false);
            }
        }
    }
}