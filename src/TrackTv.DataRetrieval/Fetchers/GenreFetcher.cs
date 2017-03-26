namespace TrackTv.DataRetrieval.Fetchers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TrackTv.DataRetrieval.Data;
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

            var genres = await this.GenresRepository.GetGenresByNamesAsync(genreNames).ConfigureAwait(false);

            foreach (string genreName in genreNames)
            {
                var genre = GetOrCreateGenre(genres, genreName);

                if (!show.HasGenre(genre))
                {
                    show.ShowsGenres.Add(new ShowsGenres(genre));
                }
            }
        }

        private static Genre GetOrCreateGenre(IEnumerable<Genre> genres, string genreName)
        {
            var genre = genres.FirstOrDefault(x => x.Name == genreName);

            return genre ?? new Genre(genreName);
        }
    }
}