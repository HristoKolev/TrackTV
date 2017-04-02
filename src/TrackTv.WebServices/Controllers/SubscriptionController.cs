namespace TrackTv.WebServices.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc;

    using TrackTv.Services.Subscription;

    public class SubscriptionController : Controller
    {
        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            this.SubscriptionService = subscriptionService;
        }

        private ISubscriptionService SubscriptionService { get; }

        [HttpPut]
        public ActionResult Put()
        {
            string userId = this.User.FindFirstValue("sub");

            // try
            // {
            // this.SubscriptionService.Subscribe()
            // }
            // catch (Exception exception)
            // {
            // Console.WriteLine(exception);
            // throw;
            // }
            return this.Ok();
        }
    }
}