namespace TrackTv.Models.Joint
{
    public class ShowsActors
    {
        public Actor Actor { get; set; }

        public int ActorId { get; set; }

        public Show Show { get; set; }

        public int ShowId { get; set; }
    }
}