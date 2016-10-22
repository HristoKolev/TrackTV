namespace TrackTv.Models
{
    using System.Collections.Generic;

    public class Network
    {
        public Network(string name)
        {
            this.Name = name;
        }

        public Network()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Show> Shows { get; } = new List<Show>();
    }
}