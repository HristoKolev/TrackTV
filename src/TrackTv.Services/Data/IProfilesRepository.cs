namespace TrackTv.Services.Data
{
    using System.Threading.Tasks;

    public interface IProfilesRepository
    {
        Task AddSubscriptionAsync(int profileId, int showId);

        Task<bool> IsUserSubscribedAsync(int profileId, int showId);

        Task RemoveSubscriptionAsync(int profileId, int showId);
    }
}