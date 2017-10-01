namespace TrackTv.Data.Models
{
    using System.Collections.Generic;

    public class Network
    {
        public Network(string networkName)
        {
            this.NetworkName = networkName;
        }

        public Network()
        {
        }

        public int NetworkId { get; set; }

        public string NetworkName { get; set; }

        public virtual ICollection<Show> Shows { get; } = new List<Show>();
    }
}