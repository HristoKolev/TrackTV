namespace TrackTv.Services.Data
{
    using System.Threading.Tasks;

    using TrackTv.Data.Models;
    using TrackTv.Services.Data.Models;

    public interface IShowsRepository
    {
        Task<int> CountAllAsync(string showName, int? genreId);

        Task<SubscriberSummary[]> CountSubscribersAsync(int[] showIds);

        Task<Show[]> GetShowsAsync(string showName, int? genreId, int page, int pageSize);

        Task<Show> GetShowWithNetworkByIdAsync(int showId);
    }
}