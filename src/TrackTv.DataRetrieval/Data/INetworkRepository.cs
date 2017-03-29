namespace TrackTv.DataRetrieval.Data
{
    using System.Threading.Tasks;

    using TrackTv.Data.Models;

    public interface INetworkRepository
    {
        Task<Network> GetNetworkByNameAsync(string name);
    }
}