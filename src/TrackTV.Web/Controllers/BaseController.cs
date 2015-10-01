namespace TrackTV.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using TrackTV.Data;

    using TrackTV.Models;

    public abstract class BaseController : Controller
    {
        protected readonly ITrackTVData Data;

        public BaseController(ITrackTVData data)
        {
            this.Data = data;
        }

        public string CurrentUserId => this.User.Identity.GetUserId();

        public bool IsLoggedIn => this.User.Identity.IsAuthenticated;

        public ApplicationUser GetCurrentUser()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return null;
            }

            string userId = this.User.Identity.GetUserId();

            return this.Data.Users.All().FirstOrDefault(user => user.Id == userId);
        }

        public ActionResult NotFound(string message = "I don't know what... it's missing...")
        {
            return this.View("~/Views/Error/PageNotFound.cshtml", null, message);
        }
    }
}