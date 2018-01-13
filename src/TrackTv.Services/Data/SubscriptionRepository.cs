namespace TrackTv.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using LinqToDB;

    using TrackTv.Data;

    public class SubscriptionRepository
    {
        public SubscriptionRepository(IDbService dbService)
        {
            this.DbService = dbService;
        }

        private IDbService DbService { get; }

        public Task AddSubscriptionAsync(int profileId, int showId)
        {
            return this.DbService.InsertAsync(new SubscriptionPoco
            {
                ProfileId = profileId,
                ShowId = showId
            });
        }

        public Task<SubscriptionPoco> GetSubscriptionAsync(int profileId, int showId)
        {
            return this.DbService.Subscriptions.FirstOrDefaultAsync(r => r.ProfileId == profileId && r.ShowId == showId);
        }

        public Task<int[]> GetSubscriptionIdsByProfileIdAsync(int profileId)
        {
            return this.DbService.Subscriptions.Where(x => x.ProfileId == profileId).Select(x => x.ShowId).ToArrayAsync();
        }

        public Task<bool> IsProfileSubscribedAsync(int profileId, int showId)
        {
            return this.DbService.Subscriptions.AnyAsync(x => x.ProfileId == profileId && x.ShowId == showId);
        }

        public async Task RemoveSubscriptionAsync(int subscriptionId)
        {
            var subscription = await this.DbService.Subscriptions.FirstOrDefaultAsync(x => x.SubscriptionId == subscriptionId)
                                         .ConfigureAwait(false);

            await this.DbService.DeleteAsync(subscription).ConfigureAwait(false);
        }
    }
}