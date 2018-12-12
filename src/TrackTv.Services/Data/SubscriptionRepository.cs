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

        public Task AddSubscriptionAsync(int profileID, int showID)
        {
            return this.DbService.Insert(new SubscriptionPoco
            {
                ProfileID = profileID,
                ShowID = showID
            });
        }

        public Task<SubscriptionPoco> GetSubscriptionAsync(int profileID, int showID)
        {
            return this.DbService.Poco.Subscriptions.FirstOrDefaultAsync(r => r.ProfileID == profileID && r.ShowID == showID);
        }

        public Task<int[]> GetSubscriptionIdsByProfileIdAsync(int profileId)
        {
            return this.DbService.Poco.Subscriptions.Where(x => x.ProfileID == profileId).Select(x => x.ShowID).ToArrayAsync();
        }

        public Task<bool> IsProfileSubscribedAsync(int profileId, int showId)
        {
            return this.DbService.Poco.Subscriptions.AnyAsync(x => x.ProfileID == profileId && x.ShowID == showId);
        }

        public async Task RemoveSubscriptionAsync(int subscriptionId)
        {
            var subscription = await this.DbService.Poco.Subscriptions.FirstOrDefaultAsync(x => x.SubscriptionID == subscriptionId);

            await this.DbService.Delete(subscription);
        }
    }
}