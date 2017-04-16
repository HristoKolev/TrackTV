namespace TrackTv.DataRetrieval.Services
{
    using System.Threading.Tasks;

    public interface IExternalShowsService
    {
        Task<CatalogShow[]> GetShowsByImdbIdAsync(string ImdbId);

        Task<CatalogShow[]> GetShowsByNameAsync(string query);
    }
}