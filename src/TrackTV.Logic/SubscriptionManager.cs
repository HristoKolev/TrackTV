namespace TrackTV.Logic
{
    using System;

    using TrackTV.Data;
    using TrackTV.Models;

    public class SubscriptionManager
    {
        public SubscriptionManager(ITrackTVData data)
        {
            this.Data = data;
        }
 
        private ITrackTVData Data { get; set; }

        public void Subscribe(ApplicationUser user, Show show)
        {
            if (user.Shows.Contains(show))
            {
                throw new InvalidOperationException("The user is already subscribed.");
            }

            user.Shows.Add(show);
            show.Subscribers.Add(user);

            this.Data.SaveChanges();
        }

        public void Unsubscribe(ApplicationUser user, Show show)
        {
            if (!user.Shows.Contains(show))
            {
                throw new InvalidOperationException("The user is not subscribed.");
            }

            user.Shows.Remove(show);
            show.Subscribers.Remove(user);

            this.Data.SaveChanges();
        }
    }
}