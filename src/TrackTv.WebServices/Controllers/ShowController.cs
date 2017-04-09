namespace TrackTv.WebServices.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using TrackTv.Services.Show;
    using TrackTv.WebServices.Infrastructure;

    [Route("api/[controller]")]
    public class ShowController : Controller
    {
        public ShowController(IShowService showService, IServiceProvider serviceProvider)
        {
            this.ShowService = showService;
            this.ServiceProvider = serviceProvider;
        }

        private IServiceProvider ServiceProvider { get; }

        private IShowService ShowService { get; }

        [HttpGet("{showId:int}")]
        public async Task<IActionResult> Get(int showId)
        {
            return this.Ok(await this.ShowService.GetFullShowAsync(showId, this.User.GetProfileId()).ConfigureAwait(false));
        }
    }
}