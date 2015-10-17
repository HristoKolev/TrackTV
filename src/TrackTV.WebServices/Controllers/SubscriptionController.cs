namespace TrackTV.WebServices.Controllers
{
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using TrackTV.Services;

    [Authorize]
    public class SubscriptionController : ApiController
    {
        public SubscriptionController(SubscriptionService subscriptionService)
        {
            this.SubscriptionService = subscriptionService;
        }

        private SubscriptionService SubscriptionService { get; }

        [HttpPost]
        public IHttpActionResult Subscribe(int id)
        {
            this.SubscriptionService.Subscribe(this.User.Identity.GetUserId(), id);

            return this.Ok();
        }

        [HttpPost]
        public IHttpActionResult Unsubscribe(int id)
        {
            this.SubscriptionService.Unsubscribe(this.User.Identity.GetUserId(), id);

            return this.Ok();
        }
    }
}