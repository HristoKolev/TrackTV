namespace TrackTv.WebServices.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using TrackTv.Services.Show;
    using TrackTv.WebServices.Infrastructure;

    [Route("api/[controller]")]
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
            int profileId = default(int);

            if (this.User.Identity.IsAuthenticated)
            {
                profileId = this.User.GetProfileId();
            }

            return this.Ok(await this.ShowService.GetFullShowAsync(showId, profileId).ConfigureAwait(false));
        }
    }
}