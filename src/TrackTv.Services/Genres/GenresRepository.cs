namespace TrackTv.Services.Genres
{
    using System.Threading.Tasks;

    using LinqToDB;

    using TrackTv.Data;

    public class GenresRepository
    {
        public GenresRepository(DbService dbService)
        {
            this.DbService = dbService;
        }

        private DbService DbService { get; }

        public Task<GenrePoco[]> GetGenresAsync()
        {
            return this.DbService.Genres.ToArrayAsync();
        }
    }
}