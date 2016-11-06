namespace TrackTv.Services.Data
{
    using System.Threading.Tasks;

    public interface IUsersRepository
    {
        Task AddSubscriptionAsync(int userId, int showId);

        Task<bool> IsUserSubscribedAsync(int userId, int showId);

        Task RemoveSubscriptionAsync(int userId, int showId);
    }
}