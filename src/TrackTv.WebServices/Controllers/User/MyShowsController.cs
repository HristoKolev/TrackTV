namespace TrackTv.WebServices.Controllers.User
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using TrackTv.Services.MyShows;
    using TrackTv.WebServices.Infrastructure;

    [Authorize]
    [Route("api/user/[controller]")]
    public class MyShowsController : Controller
    {
        public MyShowsController(IMyShowsService myShowsService)
        {
            this.MyShowsService = myShowsService;
        }

        private IMyShowsService MyShowsService { get; }

        public async Task<IActionResult> Get()
        {
            return this.Success(await this.MyShowsService
                                     .GetAllAsync(this.User.GetProfileId(), DateTime.UtcNow)
                                     .ConfigureAwait(false));
        }
    }
}