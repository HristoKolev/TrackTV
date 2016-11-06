namespace TrackTv.Services.Data
{
    using System.Threading.Tasks;

    public interface IUsersRepository
    {
        Task<bool> IsUserSubscribedAsync(int userId, int showId);
    }
}