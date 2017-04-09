namespace TrackTv.Services.Data
{
    using System.Threading.Tasks;

    using TrackTv.Data.Models;

    public interface ISubscriptionRepository
    {
        Task AddSubscriptionAsync(int profileId, int showId);

        Task<Subscription> GetSubscriptionAsync(int profileId, int showId);

        Task<bool> IsUserSubscribedAsync(int profileId, int showId);

        Task RemoveSubscriptionAsync(int subscriptionId);
    }
}