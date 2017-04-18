namespace TrackTv.DataRetrieval.Services
{
    using System.Threading.Tasks;

    public interface IExternalShowsService
    {
        Task<CatalogShow[]> GetShowsByImdbIdAsync(string imdbId);

        Task<CatalogShow[]> GetShowsByNameAsync(string query);
    }
}