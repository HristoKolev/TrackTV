namespace TrackTv.Services
{
    using System.Linq;
    using System.Threading.Tasks;

    using LinqToDB;

    using TrackTv.Data;

    public class GenresService
    {
        public GenresService(IDbService dbService)
        {
            this.DbService = dbService;
        }

        private IDbService DbService { get; }

        public async Task<FullGenre[]> GetGenresAsync()
        {
            var genres = await this.DbService.Genres.ToListAsync();

            return genres.Select(genre => new FullGenre
                         {
                             GenreName = genre.GenreName,
                             GenreId = genre.GenreID
                         })
                         .ToArray();
        }
    }

    public class FullGenre
    {
        public int GenreId { get; set; }

        public string GenreName { get; set; }
    }
}