namespace TrackTV.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using NetInfrastructure.Data.Repositories;

    using TrackTV.Data;
    using TrackTV.Logic;
    using TrackTV.Models;

    public abstract class BaseController : Controller
    {
        private IRepository<ApplicationUser, string> Users { get; }

        protected IAppSettings AppSettings { get; }

        protected BaseController(IRepository<ApplicationUser, string> users, IAppSettings appSettings)
        {
            this.Users = users;
            this.AppSettings = appSettings;
        }

        protected string CurrentUserId => this.User.Identity.GetUserId();

        protected bool IsLoggedIn => this.User.Identity.IsAuthenticated;

        public ApplicationUser GetCurrentUser()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return null;
            }

            string userId = this.User.Identity.GetUserId();

            return this.Users.All().FirstOrDefault(user => user.Id == userId);
        }

        public ActionResult NotFound(string message = "I don't know what... it's missing...")
        {
            return this.View("~/Views/Error/PageNotFound.cshtml", null, message);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this.ViewBag.Name = this.AppSettings.Name;

            base.OnActionExecuting(filterContext);
        }
    }
}