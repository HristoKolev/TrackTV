namespace TrackTv.DataRetrieval
{
    using System.Linq;
    using System.Threading.Tasks;

    using TrackTv.Models;
    using TrackTv.Models.Extensions;
    using TrackTv.Models.Joint;
    using TrackTv.Repositories;

    public class GenreFetcher
    {
        public GenreFetcher(GenresRepository genresRepository)
        {
            this.GenresRepository = genresRepository;
        }

        private GenresRepository GenresRepository { get; }

        public async Task AddGenresAsync(Show show, string[] genreNames)
        {
            var existingGenres = await this.GenresRepository.GetGenres(genreNames);

            var existingGenresByName = existingGenres.ToDictionary(genre => genre.Name, genre => genre);

            foreach (string genreName in genreNames)
            {
                Genre genre;

                if (existingGenresByName.ContainsKey(genreName))
                {
                    genre = existingGenresByName[genreName];
                }
                else
                {
                    genre = new Genre(genreName);
                }

                if (!show.IsPersisted() || !genre.IsPersisted())
                {
                    show.ShowsGenres.Add(new ShowsGenres(genre));
                }
                else
                {
                    if (!show.ShowsGenres.Any(x => (x.ShowId == show.Id) && (x.GenreId == genre.Id)))
                    {
                        show.ShowsGenres.Add(new ShowsGenres(genre));
                    }
                }
            }
        }
    }
}