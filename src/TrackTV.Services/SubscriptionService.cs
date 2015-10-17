namespace TrackTV.Services
{
    using TrackTV.Logic;
    using TrackTV.Models;

    public class SubscriptionService
    {
        public SubscriptionService(SubscriptionManager subscriptionManager, UserManager userManager, ShowManager showManager)
        {
            this.SubscriptionManager = subscriptionManager;
            this.UserManager = userManager;
            this.ShowManager = showManager;
        }

        private ShowManager ShowManager { get; }

        private SubscriptionManager SubscriptionManager { get; }

        private UserManager UserManager { get; }

        public void Subscribe(string userId, int showId)
        {
            Show show = this.ShowManager.GetShowById(showId);
            ApplicationUser user = this.UserManager.GetUserById(userId);

            this.SubscriptionManager.Subscribe(user, show);
        }

        public void Unsubscribe(string userId, int showId)
        {
            Show show = this.ShowManager.GetShowById(showId);
            ApplicationUser user = this.UserManager.GetUserById(userId);

            this.SubscriptionManager.Unsubscribe(user, show);
        }
    }
}