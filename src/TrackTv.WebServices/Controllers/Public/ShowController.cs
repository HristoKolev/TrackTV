namespace TrackTv.WebServices.Controllers.Public
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using TrackTv.Services.Show;
    using TrackTv.Services.Show.Models;
    using TrackTv.WebServices.Infrastructure;

    [Route("api/public/[controller]")]
    public class ShowController : Controller
    {
        public ShowController(IShowService showService)
        {
            this.ShowService = showService;
        }

        private IShowService ShowService { get; }

        [HttpGet("{showId:int}")]
        [ExposeError(typeof(ShowNotFoundException), "Show not found.")]
        public async Task<IActionResult> Get(int showId)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.Success(await this.ShowService.GetFullShowAsync(showId, this.User.GetProfileId()).ConfigureAwait(false));
            }

            return this.Success(await this.ShowService.GetFullShowAsync(showId).ConfigureAwait(false));
        }
    }
}