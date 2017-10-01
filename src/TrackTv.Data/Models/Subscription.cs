namespace TrackTv.Data.Models
{
    public class Subscription
    {
        public Subscription()
        {
        }

        public Subscription(int profileId, int showId)
        {
            this.ProfileId = profileId;
            this.ShowId = showId;
        }

        public Profile Profile { get; set; }

        public int ProfileId { get; set; }

        public Show Show { get; set; }

        public int ShowId { get; set; }

        public int SubscriptionId { get; set; }
    }
}