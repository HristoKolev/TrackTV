namespace TrackTv.Services.Genres
{
    using System.Threading.Tasks;

    using LinqToDB;

    using TrackTv.Data;

    public class GenresRepository
    {
        public GenresRepository(IDbService dbService)
        {
            this.DbService = dbService;
        }

        private IDbService DbService { get; }

        public Task<GenrePoco[]> GetGenresAsync()
        {
            return this.DbService.Genres.ToArrayAsync();
        }
    }
}