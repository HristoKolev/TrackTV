namespace TrackTV.Data.Repositories
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Models;

    public class NetworkRepository
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