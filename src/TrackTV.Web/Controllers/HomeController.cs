namespace TrackTV.Web.Controllers
{
    using System.Web.Mvc;
    using System.Web.Mvc.Expressions;

    using NetInfrastructure.Data.Repositories;

    using TrackTV.Logic;
    using TrackTV.Models;
    using TrackTV.Services;

    public class HomeController : BaseController
    {
        public HomeController(IRepository<ApplicationUser, string> users, IAppSettings appSettings, CalendarService calendarService)
            : base(users, appSettings)
        {
            this.CalendarService = calendarService;
        }

        private CalendarService CalendarService { get; }

        public ActionResult Calendar(int year, int month)
        {
            if (!this.IsLoggedIn)
            {
                return this.RedirectToAction<ShowsController>(controller => controller.Index());
            }

            var model = this.CalendarService.GetCalendarModel(year, month, this.CurrentUserId);

            return this.View("Index", model);
        }

        public ActionResult Index()
        {
            if (!this.IsLoggedIn)
            {
                return this.RedirectToAction<ShowsController>(controller => controller.Index());
            }

            var model = this.CalendarService.GetCalendarModel(this.CurrentUserId);

            return this.View(model);
        }
    }
}