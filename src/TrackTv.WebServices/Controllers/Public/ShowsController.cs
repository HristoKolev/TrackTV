namespace TrackTv.WebServices.Controllers.Public
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using TrackTv.Services.Shows;
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