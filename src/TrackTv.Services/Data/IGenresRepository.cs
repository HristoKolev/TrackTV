namespace TrackTv.Services.Data
{
    using System.Threading.Tasks;

    using TrackTv.Data.Models;

    public interface IGenresRepository
    {
        Task<bool> GenreExistsAsync(int genreId);

        Task<Genre[]> GetGenresAsync();
    }
}