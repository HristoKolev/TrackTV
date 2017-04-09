namespace TrackTv.Services.Genres
{
    using System.Linq;
    using System.Threading.Tasks;

    using TrackTv.Services.Data;
    using TrackTv.Services.Genres.Models;

    public class GenresService : IGenresService
    {
        public GenresService(IGenresRepository genresRepository)
        {
            this.GenresRepository = genresRepository;
        }

        private IGenresRepository GenresRepository { get; }

        public async Task<FullGenre[]> GetGenresAsync()
        {
            var genres = await this.GenresRepository.GetGenresAsync().ConfigureAwait(false);

            return genres.Select(genre => new FullGenre(genre.Id, genre.Name)).ToArray();
        }
    }
}