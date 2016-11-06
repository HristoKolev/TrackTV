namespace TrackTv.Data.Repositories
{
    using System.Threading.Tasks;

    using TrackTv.Data.Repositories.Models;
    using TrackTv.Models;

    public interface IShowsRepository
    {
        Task<int> CountAllAsync();

        Task<int> CountAllResultsAsync(string query);

        Task<int> CountByGenreAsync(string genreName);

        Task<SubscriberSummary[]> CountSubscribersAsync(int[] ids);

        Task<Show[]> GetShowsByUserIdAsync(int userId);

        Task<Show> GetShowWithNetworkByIdAsync(int id);

        Task<Show[]> GetTopAsync(int page, int pageSize);

        Task<Show[]> GetTopByGenreAsync(string genreName, int page, int pageSize);

        Task<Show[]> SearchTopAsync(string query, int page, int pageSize);
    }
}