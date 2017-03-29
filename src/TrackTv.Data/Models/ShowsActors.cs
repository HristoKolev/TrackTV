namespace TrackTv.Models
{
    public class ShowsActors
    {
        public ShowsActors()
        {
        }

        public ShowsActors(Actor actor, string role)
        {
            this.Actor = actor;
            this.Role = role;
        }

        public Actor Actor { get; set; }

        public int ActorId { get; set; }

        public string Role { get; set; }

        public Show Show { get; set; }

        public int ShowId { get; set; }
    }
}