namespace TrackTv.Models.Joint
{
    public class ShowsUsers
    {
        public Show Show { get; set; }

        public int ShowId { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }
    }
}