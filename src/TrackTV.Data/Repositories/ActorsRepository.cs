namespace TrackTV.Data.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTV.Data.Repositories.Contracts;

    using TrackTv.Models;

    public class ActorsRepository : IActorsRepository
    {
        public ActorsRepository(ICoreDataContext context)
        {
            this.Context = context;
        }

        private ICoreDataContext Context { get; }

        public Task<Actor[]> GetActorsByTheTvDbIdsAsync(int[] ids)
        {
            return this.Context.Actors.Where(actor => ids.Contains(actor.TheTvDbId)).ToArrayAsync();
        }
    }
}