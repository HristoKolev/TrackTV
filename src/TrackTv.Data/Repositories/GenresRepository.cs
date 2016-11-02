namespace TrackTV.Data.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTV.Data.Repositories.Contracts;

    using TrackTv.Models;

    public class GenresRepository : IGenresRepository
    {
        public GenresRepository(ICoreDataContext context)
        {
            this.Context = context;
        }

        private ICoreDataContext Context { get; }

        public Task<Genre[]> GetGenresByNamesAsync(string[] names)
        {
            return this.Context.Genres.Where(genre => names.Contains(genre.Name)).OrderBy(x => x.Name).ToArrayAsync();
        }
    }
}