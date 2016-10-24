namespace TrackTV.Data.Repositories
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTV.Data.Repositories.Contracts;

    using TrackTv.Models;

    public class NetworkRepository : INetworkRepository
    {
        public NetworkRepository(TrackTvDbContext context)
        {
            this.Context = context;
        }

        private TrackTvDbContext Context { get; }

        public async Task<Network> GetNetworkByNameAsync(string name)
        {
            return await this.Context.Networks.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
        }
    }
}