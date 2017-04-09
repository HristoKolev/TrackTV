namespace TrackTv.Services.Genres
{
    using System.Threading.Tasks;

    using TrackTv.Services.Genres.Models;

    public interface IGenresService
    {
        Task<FullGenre[]> GetGenresAsync();
    }
}