namespace TrackTv.Data.Models
{
    using System.Collections.Generic;

    public class Profile
    {
        public Profile(string username)
        {
            this.Username = username;
        }

        public Profile()
        {
        }

        public int Id { get; set; }

        public virtual ICollection<Subscription> Subscriptions { get; } = new List<Subscription>();

        public string Username { get; set; }
    }
}