namespace TrackTv.Services.Shows
{
    using System.Threading.Tasks;

    using TrackTv.Services.Data;
    using TrackTv.Services.Shows.Models;

    public interface IShowsService
    {
        Task<PagedResponse<ShowSummary[]>> GetShowsAsync(string showName, int? genreId, int page, int pageSize);
    }
}