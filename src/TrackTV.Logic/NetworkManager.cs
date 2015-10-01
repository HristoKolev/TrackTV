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

        public Network GetByUserFriendlyId(string userFriendlyId)
        {
            return this.Networks.All().FirstOrDefault(n => n.UserFriendlyId == userFriendlyId);
        }
    }
}