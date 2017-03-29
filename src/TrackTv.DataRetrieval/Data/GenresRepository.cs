namespace TrackTv.DataRetrieval.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Data;
    using TrackTv.Data.Models;

    public class GenresRepository : IGenresRepository
    {
        public GenresRepository(TrackTvDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        private TrackTvDbContext DbContext { get; }

        public Task<Genre[]> GetGenresByNamesAsync(string[] names)
        {
            return this.DbContext.Genres.Where(genre => names.Contains(genre.Name)).OrderBy(x => x.Name).ToArrayAsync();
        }
    }
}