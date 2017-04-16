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
        public async Task<IActionResult> Get(int showId)
        {
            FullShow show;

            if (this.User.Identity.IsAuthenticated)
            {
                show = await this.ShowService.GetFullShowAsync(showId, this.User.GetProfileId()).ConfigureAwait(false);
            }
            else
            {
                show = await this.ShowService.GetFullShowAsync(showId).ConfigureAwait(false);
            }

            return this.Ok(show);
        }
    }
}