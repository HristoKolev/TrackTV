namespace TrackTv.DataRetrieval.Data
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Data;
    using TrackTv.Models;

    public class NetworkRepository : INetworkRepository
    {
        public NetworkRepository(ICoreDataStore dataStore)
        {
            this.DataStore = dataStore;
        }

        private ICoreDataStore DataStore { get; }

        public Task<Network> GetNetworkByNameAsync(string name)
        {
            return this.DataStore.Networks.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
        }
    }
}