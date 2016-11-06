namespace TrackTv.DataRetrieval.Fetchers
{
    using System.Threading.Tasks;

    using TrackTv.Models;

    public interface IActorFetcher
    {
        Task PopulateActorsAsync(Show show);
    }
}