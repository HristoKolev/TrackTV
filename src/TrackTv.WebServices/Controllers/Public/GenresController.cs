namespace TrackTv.WebServices.Controllers.Public
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using TrackTv.Services.Genres;

    [Route("api/[controller]")]
    public class GenresController : Controller
    {
        public GenresController(IGenresService genreService)
        {
            this.GenresService = genreService;
        }

        private IGenresService GenresService { get; }

        public async Task<IActionResult> Get()
        {
            return this.Ok(await this.GenresService.GetGenresAsync().ConfigureAwait(false));
        }
    }
}