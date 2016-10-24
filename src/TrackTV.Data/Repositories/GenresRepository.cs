namespace TrackTV.Data.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTV.Data.Repositories.Contracts;

    using TrackTv.Models;

    public class GenresRepository : IGenresRepository
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