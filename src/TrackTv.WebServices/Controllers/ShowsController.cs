namespace TrackTv.WebServices.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using TrackTv.Services.Data.Exceptions;
    using TrackTv.Services.Shows;
    using TrackTv.WebServices.Infrastructure;

    [Route("api/[controller]")]
    [HandleException(typeof(InvalidQueryException))]
    public class ShowsController : Controller
    {
        public ShowsController(IShowsService showsService)
        {
            this.ShowsService = showsService;
        }

        private IShowsService ShowsService { get; }

        [HttpGet("[action]/{genreName}/{page:int}/{pageSize:int}")]
        public async Task<IActionResult> Genre(string genreName, int page, int pageSize)
        {
            return this.Ok(await this.ShowsService.GetByGenreAsync(genreName, page, pageSize).ConfigureAwait(false));
        }

        [HttpGet("[action]/{query}/{page:int}/{pageSize:int}")]
        public async Task<IActionResult> Search(string query, int page, int pageSize)
        {
            return this.Ok(await this.ShowsService.SearchTopShowsAsync(query, page, pageSize).ConfigureAwait(false));
        }

        [HttpGet("[action]/{page:int}/{pageSize:int}")]
        public async Task<IActionResult> Top(int page, int pageSize)
        {
            return this.Ok(await this.ShowsService.GetTopShowsAsync(page, pageSize).ConfigureAwait(false));
        }
    }
}