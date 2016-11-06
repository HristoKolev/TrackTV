namespace TrackTv.DataRetrieval.Fetchers
{
    using System.Threading.Tasks;

    using TrackTv.Models;

    public interface IGenreFetcher
    {
        Task PopulateGenresAsync(Show show, string[] genreNames);
    }
}