namespace TrackTv.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Models;

    public class ActorsRepository
    {
        public ActorsRepository(TrackTvDbContext context)
        {
            this.Context = context;
        }

        private TrackTvDbContext Context { get; }

        public async Task<Actor[]> GetActors(int[] ids)
        {
            return await this.Context.Actors.Where(actor => ids.Contains(actor.TvDbId)).ToArrayAsync();
        }
    }
}