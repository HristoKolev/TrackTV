namespace TrackTv.WebServices.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using TrackTv.Services.MyShows;
    using TrackTv.WebServices.Infrastructure;

    [Authorize]
    [Route("api/[controller]")]
    public class MyShowsController : Controller
    {
        public MyShowsController(IMyShowsService myShowsService)
        {
            this.MyShowsService = myShowsService;
        }

        private IMyShowsService MyShowsService { get; }

        [HttpGet]
        public async Task<IActionResult> Get()
            => this.Ok(await this.MyShowsService.GetAllAsync(this.User.GetProfileId()).ConfigureAwait(false));
    }
}