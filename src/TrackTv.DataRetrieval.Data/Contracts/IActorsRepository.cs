namespace TrackTv.DataRetrieval.Data.Contracts
{
    using System.Threading.Tasks;

    using TrackTv.Models;

    public interface IActorsRepository
    {
        Task<Actor[]> GetActorsByTheTvDbIdsAsync(int[] ids);
    }
}