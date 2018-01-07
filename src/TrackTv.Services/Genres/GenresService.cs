namespace TrackTv.Services.Genres
{
    using System.Linq;
    using System.Threading.Tasks;

    using TrackTv.Services.Data;
    using TrackTv.Services.Genres.Models;

    public class GenresService
    {
        public GenresService(GenresRepository genresRepository)
        {
            this.GenresRepository = genresRepository;
        }

        private GenresRepository GenresRepository { get; }

        public async Task<FullGenre[]> GetGenresAsync()
        {
            var genres = await this.GenresRepository.GetGenresAsync().ConfigureAwait(false);

            return genres.Select(genre => new FullGenre(genre.GenreId, genre.GenreName)).ToArray();
        }
    }
}