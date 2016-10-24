namespace TrackTV.DataRetrieval.Fetchers.Contracts
{
    using System.Threading.Tasks;

    using TrackTv.Models;

    public interface IActorFetcher
    {
        Task PopulateActorsAsync(Show show);
    }
}