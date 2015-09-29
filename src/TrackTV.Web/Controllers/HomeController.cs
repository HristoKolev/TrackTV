namespace TrackTV.Web.Controllers
{
    using System;
    using System.Web.Mvc;

    using TrackTV.Data.Contracts;
    using TrackTV.Logic.Calendar;
    using TrackTV.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        public HomeController(ITrackTVData data)
            : base(data)
        {
        }

        public ActionResult Calendar(int year, int month)
        {
            DateTime date;

            try
            {
                date = new DateTime(year, month, 1);
            }
            catch (ArgumentOutOfRangeException)
            {
                date = DateTime.Now;
            }

            if (!this.IsLoggedIn)
            {
                return this.RedirectToAction("Index", "Shows");
            }

            CalendarViewModel model = this.GetCalendarModel(date.Year, date.Month);

            return this.View("Index", model);
        }

        public ActionResult Index()
        {
            if (!this.IsLoggedIn)
            {
                return this.RedirectToAction("Index", "Shows");
            }

            DateTime now = DateTime.Now;

            CalendarViewModel model = this.GetCalendarModel(now.Year, now.Month);

            return this.View(model);
        }

        private CalendarViewModel GetCalendarModel(int year, int month)
        {
            EpisodeCalendar episodeCalendar = new EpisodeCalendar(this.Data, this.CurrentUserId);

            DayOfWeek[] daysOfWeek = { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday };

            DateTime date = new DateTime(year, month, 1);

            CalendarViewModel model = new CalendarViewModel
            {
                Month = episodeCalendar.Create(date),
                Date = date,
                DaysOfWeek = daysOfWeek
            };
            return model;
        }
    }
}