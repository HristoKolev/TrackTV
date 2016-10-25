namespace TrackTV.DataRetrieval.Fetchers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TrackTV.Data.Repositories.Contracts;
    using TrackTV.DataRetrieval.Fetchers.Contracts;

    using TrackTv.Models;
    using TrackTv.Models.Joint;

    public class GenreFetcher : IGenreFetcher
    {
        public GenreFetcher(IGenresRepository genresRepository)
        {
            this.GenresRepository = genresRepository;
        }

        private IGenresRepository GenresRepository { get; }

        public async Task PopulateGenresAsync(Show show, string[] genreNames)
        {
            if (genreNames.Length == 0)
            {
                throw new ArgumentException("The genre names array is empty.", nameof(genreNames));
            }

            var existingGenres = await this.GenresRepository.GetGenresByNamesAsync(genreNames);

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