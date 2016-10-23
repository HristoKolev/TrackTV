namespace TrackTv.DataRetrieval
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Models;
    using TrackTv.Models.Extensions;
    using TrackTv.Models.Joint;

    public class GenreFetcher
    {
        public GenreFetcher(TrackTvDbContext context)
        {
            this.Context = context;
        }

        private TrackTvDbContext Context { get; }

        public async Task AddGenresAsync(Show show, string[] genreNames)
        {
            var existingGenres = await this.Context.Genres.Where(genre => genreNames.Contains(genre.Name)).ToListAsync();
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