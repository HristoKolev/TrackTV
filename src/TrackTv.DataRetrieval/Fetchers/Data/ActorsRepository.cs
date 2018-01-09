namespace TrackTv.DataRetrieval.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Data;
    using TrackTv.Data.Models;

    public class ActorsRepository
    {
        public ActorsRepository(TrackTvDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        private TrackTvDbContext DbContext { get; }

        public Task<Actor[]> GetActorsByTheTvDbIdsAsync(int[] ids)
        {
            return this.DbContext.Actors.Where(actor => ids.Contains(actor.TheTvDbId)).ToArrayAsync();
        }
    }
}