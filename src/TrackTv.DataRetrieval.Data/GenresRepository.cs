namespace TrackTv.DataRetrieval.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Data;
    using TrackTv.DataRetrieval.Data.Contracts;
    using TrackTv.Models;

    public class GenresRepository : IGenresRepository
    {
        public GenresRepository(ICoreDataStore dataStore)
        {
            this.DataStore = dataStore;
        }

        private ICoreDataStore DataStore { get; }

        public Task<Genre[]> GetGenresByNamesAsync(string[] names)
        {
            return this.DataStore.Genres.Where(genre => names.Contains(genre.Name)).OrderBy(x => x.Name).ToArrayAsync();
        }
    }
}