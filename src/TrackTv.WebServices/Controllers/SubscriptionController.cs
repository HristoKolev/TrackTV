namespace TrackTv.WebServices.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using TrackTv.Services.Subscription;
    using TrackTv.WebServices.Infrastructure;

    [Authorize]
    [Route("api/[controller]")]
    public class SubscriptionController : Controller
    {
        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            this.SubscriptionService = subscriptionService;
        }

        private ISubscriptionService SubscriptionService { get; }

        [HttpPut("{showId:int}")]
        public async Task<ActionResult> Put(int showId)
        {
            int profileId = this.User.GetProfileId();

            await this.SubscriptionService.Subscribe(profileId, showId).ConfigureAwait(false);

            return this.Ok();
        }

        [HttpDelete("{showId:int}")]
        public async Task<ActionResult> Delete(int showId)
        {
            int profileId = this.User.GetProfileId();

            await this.SubscriptionService.Unsubscribe(profileId, showId).ConfigureAwait(false);

            return this.Ok();
        }
    }
}