namespace TrackTv.Data.Models
{
    public class Role
    {
        public Role()
        {
        }

        public Role(Actor actor, string roleName)
        {
            this.Actor = actor;
            this.RoleName = roleName;
        }

        public Actor Actor { get; set; }

        public int ActorId { get; set; }

        public int Id { get; set; }

        public string RoleName { get; set; }

        public Show Show { get; set; }

        public int ShowId { get; set; }
    }
}