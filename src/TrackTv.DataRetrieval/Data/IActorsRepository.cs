namespace TrackTv.DataRetrieval.Data
{
    using System.Threading.Tasks;

    using TrackTv.Data.Models;

    public interface IActorsRepository
    {
        Task<Actor[]> GetActorsByTheTvDbIdsAsync(int[] ids);
    }
}