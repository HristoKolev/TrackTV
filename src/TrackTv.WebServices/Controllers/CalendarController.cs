﻿namespace TrackTv.WebServices.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using TrackTv.Services.Calendar;
    using TrackTv.WebServices.Infrastructure;

    [Authorize]
    [Route("api/[controller]")]
    public class CalendarController : Controller
    {
        public CalendarController(ICalendarService calendarService)
        {
            this.CalendarService = calendarService;
        }

        private ICalendarService CalendarService { get; }

        public async Task<IActionResult> Get()
        {
            return this.Ok(await this.CalendarService.GetCalendarAsync(this.User.GetProfileId()).ConfigureAwait(false));
        }
    }
}