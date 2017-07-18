namespace TrackTv.WebServices.Controllers.User
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using TrackTv.Services.Subscription;
    using TrackTv.WebServices.Infrastructure;

    [Authorize]
    [Route("api/user/[controller]")]
    public class SubscriptionController : Controller
    {
        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            this.SubscriptionService = subscriptionService;
        }

        private ISubscriptionService SubscriptionService { get; }

        [HttpDelete("{showId:int}")]
        public async Task<IActionResult> Delete(int showId)
        {
            await this.SubscriptionService
                      .Unsubscribe(this.User.GetProfileId(), showId)
                      .ConfigureAwait(false);

            return this.Success();
        }

        [HttpPut("{showId:int}")]
        public async Task<IActionResult> Put(int showId)
        {
            await this.SubscriptionService
                      .Subscribe(this.User.GetProfileId(), showId)
                      .ConfigureAwait(false);

            return this.Success();
        }
    }
}