namespace TrackTV.WebServices.Controllers
{
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using TrackTV.Services;

    public class CalendarController : ApiController
    {
        public CalendarController(CalendarService calendar)
        {
            this.Calendar = calendar;
        }

        private CalendarService Calendar { get; }

        [HttpGet]
        [Authorize]
        public IHttpActionResult Index()
        {
            string currentUserId = this.User.Identity.GetUserId();

            var model = this.Calendar.GetCalendarModel(currentUserId);

            return this.Ok(model);
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult Month(int year, int month)
        {
            string currentUserId = this.User.Identity.GetUserId();

            var model = this.Calendar.GetCalendarModel(currentUserId, year, month);

            return this.Ok(model);
        }
    }
}