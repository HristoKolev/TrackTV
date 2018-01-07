namespace TrackTv.WebServices.Controllers.User
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using TrackTv.Services.Calendar;
    using TrackTv.WebServices.Infrastructure;

    [Authorize]
    [Route("api/user/[controller]")]
    public class CalendarController : Controller
    {
        public CalendarController(CalendarService calendarService)
        {
            this.CalendarService = calendarService;
        }

        private CalendarService CalendarService { get; }

        public async Task<IActionResult> Get()
        {
            return this.Success(
                await this.CalendarService.GetCalendarAsync(this.User.GetProfileId(), DateTime.UtcNow).ConfigureAwait(false));
        }
    }
}