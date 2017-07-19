using TrackTv.Services.Shows.Models;
using TrackTv.WebServices.Infrastructure;

namespace TrackTv.WebServices.Controllers.Public
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using TrackTv.Services.Shows;

    [Route("api/public/[controller]")]
    public class ShowsController : Controller
    {
        public ShowsController(IShowsService showsService)
        {
            this.ShowsService = showsService;
        }

        private IShowsService ShowsService { get; }

        [HttpGet("[action]/{genreId:int}/{page:int}/{pageSize:int}")]
        [ExposeError(typeof(GenreNotFoundException), "Can't find the genre you are looking for.")]
        public async Task<IActionResult> Genre(int genreId, int page, int pageSize)
        {
            return this.Success(await this.ShowsService
                                          .GetByGenreAsync(genreId, page, pageSize)
                                          .ConfigureAwait(false));
        }

        [HttpGet("[action]/{query}/{page:int}/{pageSize:int}")]
        public async Task<IActionResult> Search(string query, int page, int pageSize)
        {
            return this.Success(await this.ShowsService
                                          .SearchTopShowsAsync(query, page, pageSize)
                                          .ConfigureAwait(false));
        }

        [HttpGet("[action]/{page:int}/{pageSize:int}")]
        public async Task<IActionResult> Top(int page, int pageSize)
        {
            return this.Success(await this.ShowsService
                                          .GetTopShowsAsync(page, pageSize)
                                          .ConfigureAwait(false));
        }
    }
}