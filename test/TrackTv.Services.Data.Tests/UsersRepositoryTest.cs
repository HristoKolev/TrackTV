namespace TrackTv.Services.Data.Tests
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Models;
    using TrackTv.Models.Joint;
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
                context.Users.Add(new User
                {
                    Id = 2
                });

                await context.SaveChangesAsync();

                var repository = new UsersRepository(context);

                await repository.AddSubscriptionAsync(2, 1);

                var relationship =
                    await context.ShowsUsers.FirstOrDefaultAsync(showsUsers => showsUsers.ShowId == 1 && showsUsers.UserId == 2);

                Assert.NotNull(relationship);
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task AddSubscriptionAsync_throws_if_the_user_is_already_subscribed()
        {
            using (var context = CreateContext())
            {
                var repository = new UsersRepository(context);

                var show = new Show
                {
                    Id = 1
                };

                var user = new User
                {
                    Id = 2
                };

                context.Shows.Add(show);
                context.Users.Add(user);
                context.ShowsUsers.Add(new ShowsUsers(user.Id, show.Id));

                await context.SaveChangesAsync();

                await Assert.ThrowsAsync<SubscriptionException>(async () => await repository.AddSubscriptionAsync(2, 1));
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

                var user = new User
                {
                    Id = 2
                };

                context.Shows.Add(show);
                context.Users.Add(user);
                context.ShowsUsers.Add(new ShowsUsers(user.Id, show.Id));

                await context.SaveChangesAsync();

                var repository = new UsersRepository(context);

                Assert.True(await repository.IsUserSubscribedAsync(2, 1));
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task IsUserSubscribedAsync_returns_true_if_there_is_no_relationship()
        {
            using (var context = CreateContext())
            {
                var repository = new UsersRepository(context);

                Assert.False(await repository.IsUserSubscribedAsync(2, 1));
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

                var user = new User
                {
                    Id = 2
                };

                context.Shows.Add(show);
                context.Users.Add(user);
                context.ShowsUsers.Add(new ShowsUsers(user.Id, show.Id));

                await context.SaveChangesAsync();

                var repository = new UsersRepository(context);

                await repository.RemoveSubscriptionAsync(2, 1);

                Assert.Equal(0, await context.ShowsUsers.CountAsync());
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task RemoveSubscriptionAsync_throws_if_the_user_is_not_subscribed()
        {
            using (var context = CreateContext())
            {
                var repository = new UsersRepository(context);

                await Assert.ThrowsAsync<SubscriptionException>(async () => await repository.RemoveSubscriptionAsync(2, 1));
            }
        }
    }
}