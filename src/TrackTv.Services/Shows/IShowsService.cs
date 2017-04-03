namespace TrackTv.Services.Shows
{
    using System.Threading.Tasks;

    using TrackTv.Services.Shows.Models;

    public interface IShowsService
    {
        Task<PagedResponse<ShowSummary[]>> GetByGenreAsync(string genreName, int page, int pageSize);

        Task<PagedResponse<ShowSummary[]>> GetTopShowsAsync(int page, int pageSize);

        Task<PagedResponse<ShowSummary[]>> SearchTopShowsAsync(string query, int page, int pageSize);
    }
}