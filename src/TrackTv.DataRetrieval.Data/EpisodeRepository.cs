namespace TrackTv.DataRetrieval.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Data;
    using TrackTv.Models;

    public class EpisodeRepository : IEpisodeRepository
    {
        public EpisodeRepository(ICoreDataStore dataStore)
        {
            this.DataStore = dataStore;
        }

        private ICoreDataStore DataStore { get; }

        public Task<Episode> GetEpisodeById(int id)
        {
            return this.DataStore.Episodes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Episode>> GetEpisodesByShowIdAsync(int id)
        {
            return this.DataStore.Episodes.Where(x => x.ShowId == id).ToListAsync();
        }

        public Task<List<Episode>> GetEpisodesByTheTvDbIdsAsync(int[] ids)
        {
            return this.DataStore.Episodes.Where(x => ids.Contains(x.TheTvDbId)).ToListAsync();
        }
    }
}