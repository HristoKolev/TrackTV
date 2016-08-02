namespace TrackTV.WebServices.Controllers
{
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using TrackTV.Services;

    [Authorize]
    public class MyShowsController : ApiController
    {
        public MyShowsController(MyShowsService myShows)
        {
            this.MyShows = myShows;
        }

        private MyShowsService MyShows { get; }

        [HttpGet]
        public IHttpActionResult Continuing(int? page)
        {
            return this.Ok(this.MyShows.Continuing(this.User.Identity.GetUserId(), page));
        }

        [HttpGet]
        public IHttpActionResult Ended(int? page)
        {
            return this.Ok(this.MyShows.Ended(this.User.Identity.GetUserId(), page));
        }
    }
}