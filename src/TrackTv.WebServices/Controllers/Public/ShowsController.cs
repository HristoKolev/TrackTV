namespace TrackTv.WebServices.Controllers.Public
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using TrackTv.Services.Shows;
    using TrackTv.Services.Shows.Models;
    using TrackTv.WebServices.Infrastructure;

    [Route("api/public/[controller]")]
    public class ShowsController : Controller
    {
        public ShowsController(IShowsService showsService)
        {
            this.ShowsService = showsService;
        }

        private IShowsService ShowsService { get; }

        [ExposeError(typeof(GenreNotFoundException), "Can't find the genre you are looking for.")]
        public async Task<IActionResult> Post([FromBody] ShowsViewModel model)
        {
            return this.Success(await this.ShowsService.GetShowsAsync(model.ShowName, model.GenreId, model.Page, model.PageSize)
                                          .ConfigureAwait(false));
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