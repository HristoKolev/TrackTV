namespace TrackTV.Web.Controllers
{
    using System.Web.Mvc;

    using TrackTV.Data;
    using TrackTV.Logic;
    using TrackTV.Services;

    public class HomeController : BaseController
    {
        public HomeController(ITrackTVData data, CalendarService calendarService)
            : base(data)
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