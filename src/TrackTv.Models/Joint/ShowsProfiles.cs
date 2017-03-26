namespace TrackTv.Models.Joint
{
    public class ShowsProfiles
    {
        public ShowsProfiles()
        {
        }

        public ShowsProfiles(int profileId, int showId)
        {
            this.ProfileId = profileId;
            this.ShowId = showId;
        }

        public Profile Profile { get; set; }

        public int ProfileId { get; set; }

        public Show Show { get; set; }

        public int ShowId { get; set; }
    }
}