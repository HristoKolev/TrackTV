namespace TrackTV.Data.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTV.Data.Repositories.Contracts;

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