namespace TrackTv.WebServices.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using TrackTv.Services;
    using TrackTv.WebServices.Infrastructure;

    [Route("api/public/[controller]")]
    public class ShowsController : Controller
    {
        public ShowsController(ShowsService showsService)
        {
            this.ShowsService = showsService;
        }

        private ShowsService ShowsService { get; }

        public async Task<IActionResult> Post([FromBody] ShowsViewModel model)
        {
            return this.Success(await this.ShowsService.GetShowsAsync(model.ShowName, model.GenreId, model.Page, model.PageSize));
        }
    }

    [Route("api/public/[controller]")]
    public class ShowController : Controller
    {
        public ShowController(ShowService showService)
        {
            this.ShowService = showService;
        }

        private ShowService ShowService { get; }

        [HttpGet("{showId:int}")]
        [ExposeError(typeof(ShowNotFoundException), "Show not found.")]
        public async Task<IActionResult> Get(int showId)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.Success(await this.ShowService.GetFullShowAsync(showId, this.User.GetProfileId()));
            }

            return this.Success(await this.ShowService.GetFullShowAsync(showId));
        }
    }

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
            return this.Success(await this.GenresService.GetGenresAsync());
        }
    }

    public class ShowsViewModel
    {
        public int? GenreId { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public string ShowName { get; set; }
    }
}