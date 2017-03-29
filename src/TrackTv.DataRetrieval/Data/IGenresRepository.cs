namespace TrackTv.DataRetrieval.Data
{
    using System.Threading.Tasks;

    using TrackTv.Data.Models;

    public interface IGenresRepository
    {
        Task<Genre[]> GetGenresByNamesAsync(string[] names);
    }
}