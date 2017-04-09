namespace TrackTv.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Data;
    using TrackTv.Data.Models;

    public class SubscriptionRepository : ISubscriptionRepository
    {
        public SubscriptionRepository(TrackTvDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        private TrackTvDbContext DbContext { get; }

        public Task AddSubscriptionAsync(int profileId, int showId)
        {
            this.DbContext.Subscriptions.Add(new Subscription(profileId, showId));

            return this.DbContext.SaveChangesAsync();
        }

        public Task<Subscription> GetSubscriptionAsync(int profileId, int showId)
        {
            return this.DbContext.Subscriptions.AsNoTracking().SingleOrDefaultAsync(r => r.ProfileId == profileId && r.ShowId == showId);
        }

        public Task<bool> IsUserSubscribedAsync(int profileId, int showId)
        {
            return this.DbContext.Subscriptions.AnyAsync(x => x.ProfileId == profileId && x.ShowId == showId);
        }

        public async Task RemoveSubscriptionAsync(int subscriptionId)
        {
            var subscription = await this.DbContext.Subscriptions.SingleAsync(x => x.Id == subscriptionId);

            this.DbContext.Subscriptions.Remove(subscription);

            await this.DbContext.SaveChangesAsync();
        }
    }
}