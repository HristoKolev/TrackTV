namespace TrackTV.Logic
{
    using System.Linq;

    using NetInfrastructure.Data.Repositories;

    using TrackTV.Models;

    public class NetworkManager
    {
        public NetworkManager(IRepository<Network> networks)
        {
            this.Networks = networks;
        }

        private IRepository<Network> Networks { get; }

        public Network GetByStringId(string stringId)
        {
            return this.Networks.All().FirstOrDefault(n => n.StringId == stringId);
        }
    }
}