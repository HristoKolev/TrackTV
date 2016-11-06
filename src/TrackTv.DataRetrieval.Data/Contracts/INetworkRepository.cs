namespace TrackTv.DataRetrieval.Data.Contracts
{
    using System.Threading.Tasks;

    using TrackTv.Models;

    public interface INetworkRepository
    {
        Task<Network> GetNetworkByNameAsync(string name);
    }
}