namespace TrackTV.Web.Controllers
{
    using System.Web.Mvc;

    using NetInfrastructure.Data.Repositories;

    using TrackTV.Models;
    using TrackTV.Services;

    public class HomeController : BaseController
    {
        public HomeController(IRepository<ApplicationUser, string> users, CalendarService calendarService)
            : base(users)
        {
            this.CalendarService = calendarService;
        }

        private CalendarService CalendarService { get; }

        public ActionResult Calendar(int year, int month)
        {
            if (!this.IsLoggedIn)
            {
                return this.RedirectToAction("Index", "Shows");
            }

            return this.View("Index", this.CalendarService.GetCalendarModel(year, month, this.CurrentUserId));
        }

        public ActionResult Index()
        {
            if (!this.IsLoggedIn)
            {
                return this.RedirectToAction("Index", "Shows");
            }

            return this.View(this.CalendarService.GetCalendarModel(this.CurrentUserId));
        }
    }
}