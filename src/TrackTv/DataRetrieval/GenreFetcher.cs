namespace TrackTv.DataRetrieval
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TrackTv.Models;
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

            var genres = existingGenres.ToDictionary(genre => genre.Name, genre => genre);

            foreach (string genreName in genreNames)
            {
                var genre = GetOrCreateGenre(genres, genreName);

                if (!show.HasGenre(genre))
                {
                    show.ShowsGenres.Add(new ShowsGenres(genre));
                }
            }
        }

        private static Genre GetOrCreateGenre(IReadOnlyDictionary<string, Genre> genres, string genreName)
        {
            if (genres.ContainsKey(genreName))
            {
                return genres[genreName];
            }

            return new Genre(genreName);
        }
    }
}