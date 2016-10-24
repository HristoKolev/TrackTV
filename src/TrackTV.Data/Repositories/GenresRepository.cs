namespace TrackTV.Data.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Models;

    public class GenresRepository
    {
        public GenresRepository(TrackTvDbContext context)
        {
            this.Context = context;
        }

        private TrackTvDbContext Context { get; }

        public async Task<Genre[]> GetGenres(string[] names)
        {
            return await this.Context.Genres.Where(genre => names.Contains(genre.Name)).ToArrayAsync();
        }
    }
}