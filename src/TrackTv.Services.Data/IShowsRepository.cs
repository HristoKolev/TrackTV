namespace TrackTv.Services.Data
{
    using System.Threading.Tasks;

    using TrackTv.Models;
    using TrackTv.Services.Data.Models;

    public interface IShowsRepository
    {
        Task<int> CountAllAsync();

        Task<int> CountAllResultsAsync(string query);

        Task<int> CountByGenreAsync(string genreName);

        Task<SubscriberSummary[]> CountSubscribersAsync(int[] showIds);

        Task<Show[]> GetShowsByProfileIdAsync(int profileId);

        Task<Show> GetShowWithNetworkByIdAsync(int id);

        Task<Show[]> GetTopAsync(int page, int pageSize);

        Task<Show[]> GetTopByGenreAsync(string genreName, int page, int pageSize);

        Task<Show[]> SearchTopAsync(string query, int page, int pageSize);
    }
}