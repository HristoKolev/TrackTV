namespace TrackTv.DataRetrieval.Data.Contracts
{
    using System.Threading.Tasks;

    using TrackTv.Models;

    public interface IGenresRepository
    {
        Task<Genre[]> GetGenresByNamesAsync(string[] names);
    }
}