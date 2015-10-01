namespace TrackTV.Logic
{
    using System;

    using NetInfrastructure.Data.Repositories;

    using TrackTV.Models;

    public class SubscriptionManager
    {
        public SubscriptionManager(IRepository<Show> shows)
        {
            this.Shows = shows;
        }

        private IRepository<Show> Shows { get; set; }

        public void Subscribe(ApplicationUser user, Show show)
        {
            if (user.Shows.Contains(show))
            {
                throw new InvalidOperationException("The user is already subscribed.");
            }

            user.Shows.Add(show);
            show.Subscribers.Add(user);

            this.Shows.SaveChanges();
        }

        public void Unsubscribe(ApplicationUser user, Show show)
        {
            if (!user.Shows.Contains(show))
            {
                throw new InvalidOperationException("The user is not subscribed.");
            }

            user.Shows.Remove(show);
            show.Subscribers.Remove(user);

            this.Shows.SaveChanges();
        }
    }
}