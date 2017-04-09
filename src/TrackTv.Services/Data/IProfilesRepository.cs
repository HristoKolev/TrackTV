namespace TrackTv.Services.Data
{
    using System.Threading.Tasks;

    using TrackTv.Data.Models;

    public interface IProfilesRepository
    {
        Task AddSubscriptionAsync(int profileId, int showId);

        Task<int> CreateProfileAsync(string username);

        Task<Profile> GetProfileByIdAsync(int profileId);

        Task<Subscription> GetSubscriptionAsync(int profileId, int showId);

        Task<bool> IsUserSubscribedAsync(int profileId, int showId);

        Task RemoveSubscriptionAsync(Subscription subscription);
    }
}