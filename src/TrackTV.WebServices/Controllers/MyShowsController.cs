namespace TrackTV.WebServices.Controllers
{
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using TrackTV.Services;

    public class MyShowsController : ApiController
    {
        public MyShowsController(MyShowsService myShows)
        {
            this.MyShows = myShows;
        }

        private MyShowsService MyShows { get; }

        [Authorize]
        [HttpGet]
        public IHttpActionResult Continuing(int? page)
        {
            return this.Ok(this.MyShows.Continuing(this.User.Identity.GetUserId(), page));
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult Ended(int? page)
        {
            return this.Ok(this.MyShows.Ended(this.User.Identity.GetUserId(), page));
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult Subscribe(int id)
        {
            this.MyShows.Subscribe(this.User.Identity.GetUserId(), id);

            return this.Ok();
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult Unsubscribe(int id)
        {
            this.MyShows.Unsubscribe(this.User.Identity.GetUserId(), id);

            return this.Ok();
        }
    }
}