namespace TrackTv.Models.Joint
{
    public class ShowsUsers
    {
        public ShowsUsers()
        {
        }

        public ShowsUsers(int userId, int showId)
        {
            this.UserId = userId;
            this.ShowId = showId;
        }

        public Show Show { get; set; }

        public int ShowId { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }
    }
}