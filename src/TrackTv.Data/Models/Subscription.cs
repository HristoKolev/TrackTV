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

        public int Id { get; set; }

        public Profile Profile { get; set; }

        public int ProfileId { get; set; }

        public Show Show { get; set; }

        public int ShowId { get; set; }
    }
}