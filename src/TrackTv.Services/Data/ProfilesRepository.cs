namespace TrackTv.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Data;
    using TrackTv.Data.Models;

    public class ProfilesRepository : IProfilesRepository
    {
        public ProfilesRepository(TrackTvDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        private TrackTvDbContext DbContext { get; }

        public async Task AddSubscriptionAsync(int profileId, int showId)
        {
            this.DbContext.Subscriptions.Add(new Subscription(profileId, showId));

            await this.DbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<int> CreateProfile(string username)
        {
            var profile = new Profile(username);

            this.DbContext.Profiles.Add(profile);

            await this.DbContext.SaveChangesAsync().ConfigureAwait(false);

            return profile.Id;
        }

        public Task<Subscription> GetSubscriptionAsync(int profileId, int showId)
        {
            return this.DbContext.Subscriptions.SingleOrDefaultAsync(r => r.ProfileId == profileId && r.ShowId == showId);
        }

        public Task<bool> IsUserSubscribedAsync(int profileId, int showId)
        {
            return this.DbContext.Subscriptions.AnyAsync(x => x.ProfileId == profileId && x.ShowId == showId);
        }

        public async Task RemoveSubscriptionAsync(Subscription subscription)
        {
            this.DbContext.Subscriptions.Remove(subscription);

            await this.DbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}