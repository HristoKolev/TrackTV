namespace TrackTv.Services.Data
{
    using System.Threading.Tasks;

    using TrackTv.Data.Models;
    using TrackTv.Services.Data.Models;

    public interface IShowsRepository
    {
        Task<int> CountAllAsync();

        Task<int> CountAllResultsAsync(string query);

        Task<int> CountByGenreAsync(int genreId);

        Task<SubscriberSummary[]> CountSubscribersAsync(int[] showIds);

        Task<Show> GetShowWithNetworkByIdAsync(int id);

        Task<Show[]> GetTopAsync(int page, int pageSize);

        Task<Show[]> GetTopByGenreAsync(int genreId, int page, int pageSize);

        Task<Show[]> SearchTopAsync(string query, int page, int pageSize);
    }
}