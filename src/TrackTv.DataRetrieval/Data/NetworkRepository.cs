namespace TrackTv.DataRetrieval.Data
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Data;
    using TrackTv.Data.Models;

    public class NetworkRepository : INetworkRepository
    {
        public NetworkRepository(TrackTvDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        private TrackTvDbContext DbContext { get; }

        public Task<Network> GetNetworkByNameAsync(string name)
        {
            return this.DbContext.Networks.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
        }
    }
}