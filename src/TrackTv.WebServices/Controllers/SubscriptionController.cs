namespace TrackTv.WebServices.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using TrackTv.Services.Subscription;

    public class SubscriptionController : Controller
    {
        public SubscriptionController(SubscriptionService subscriptionService)
        {
            this.SubscriptionService = subscriptionService;
        }

        private SubscriptionService SubscriptionService { get; }

        // [HttpPut]

        // public ActionResult Put()
        // {
        // string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

        // try
        // {
        // this.SubscriptionService.Subscribe()
        // }
        // catch (Exception exception)
        // {
        // Console.WriteLine(exception);
        // throw;
        // }

        // return this.Ok();
        // }
    }
}