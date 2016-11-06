namespace TrackTv.DataRetrieval.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Data;
    using TrackTv.DataRetrieval.Data.Contracts;
    using TrackTv.Models;

    public class ActorsRepository : IActorsRepository
    {
        public ActorsRepository(ICoreDataStore dataStore)
        {
            this.DataStore = dataStore;
        }

        private ICoreDataStore DataStore { get; }

        public Task<Actor[]> GetActorsByTheTvDbIdsAsync(int[] ids)
        {
            return this.DataStore.Actors.Where(actor => ids.Contains(actor.TheTvDbId)).ToArrayAsync();
        }
    }
}