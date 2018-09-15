namespace TrackTv.WebServices.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using TrackTv.Services;
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
            var now = DateTime.UtcNow;

            return this.Success(await this.CalendarService.GetCalendarAsync(this.User.GetProfileId(), now, now.Date));
        }
    }

    [Authorize]
    [Route("api/user/[controller]")]
    public class MyShowsController : Controller
    {
        public MyShowsController(MyShowsService myShowsService)
        {
            this.MyShowsService = myShowsService;
        }

        private MyShowsService MyShowsService { get; }

        public async Task<IActionResult> Get()
        {
            return this.Success(await this.MyShowsService.GetAllAsync(this.User.GetProfileId(), DateTime.UtcNow));
        }
    }

    [Authorize]
    [Route("api/user/[controller]")]
    public class ProfileController : Controller
    {
        public ProfileController(ProfileService profileService)
        {
            this.ProfileService = profileService;
        }

        private ProfileService ProfileService { get; }

        public async Task<IActionResult> Get()
        {
            return this.Success(await this.ProfileService.GetProfileAsync(this.User.GetProfileId()));
        }
    }

    [Authorize]
    [Route("api/user/[controller]")]
    public class SubscriptionController : Controller
    {
        public SubscriptionController(SubscriptionService subscriptionService)
        {
            this.SubscriptionService = subscriptionService;
        }

        private SubscriptionService SubscriptionService { get; }

        [HttpDelete("{showId:int}")]
        [ExposeError(typeof(SubscriptionException), "You are not subscribed to that show.")]
        public async Task<IActionResult> Delete(int showId)
        {
            await this.SubscriptionService.Unsubscribe(this.User.GetProfileId(), showId);

            return this.Success();
        }

        [HttpPut("{showId:int}")]
        [ExposeError(typeof(SubscriptionException), "You are already subscribed to that show.")]
        public async Task<IActionResult> Put(int showId)
        {
            await this.SubscriptionService.Subscribe(this.User.GetProfileId(), showId);

            return this.Success();
        }
    }
}