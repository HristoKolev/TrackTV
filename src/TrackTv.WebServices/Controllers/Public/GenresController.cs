namespace TrackTv.WebServices.Controllers.Public
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using TrackTv.Services.Genres;
    using TrackTv.WebServices.Infrastructure;

    [Route("api/public/[controller]")]
    public class GenresController : Controller
    {
        public GenresController(GenresService genreService)
        {
            this.GenresService = genreService;
        }

        private GenresService GenresService { get; }

        public async Task<IActionResult> Get()
        {
            return this.Success(await this.GenresService.GetGenresAsync().ConfigureAwait(false));
        }
    }
}