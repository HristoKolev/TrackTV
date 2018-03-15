namespace TrackTv.Updater
{
    using System;
    using System.Threading.Tasks;

    using LinqToDB;

    using TrackTv.Data;

    public class ApiChangeRepository
    {
        public ApiChangeRepository(IDbService dbService)
        {
            this.DbService = dbService;
        }

        private IDbService DbService { get; }

        public async Task IncrementFailedCount(int thetvdbid)
        {
            var apiChange = await this.DbService.ApiChanges.FirstOrDefaultAsync(p => p.ApiChangeThetvdbid == thetvdbid)
                                      .ConfigureAwait(false) ;

            apiChange.ApiChangeLastFailedTime = DateTime.UtcNow;
            apiChange.ApiChangeFailCount++;

            await this.DbService.Save(apiChange).ConfigureAwait(false);
        }

        public Task<ApiChangePoco[]> GetCurrentChangeList()
        {
            return this.DbService.ApiChanges.ToArrayAsync();
        }

        public async Task RemoveApiChange(int thetvdbid)
        {
            var poco = await this.DbService.ApiChanges.FirstOrDefaultAsync(p => p.ApiChangeThetvdbid == thetvdbid).ConfigureAwait(false);

            if (poco != null)
            {
                await this.DbService.Delete(poco).ConfigureAwait(false);
            }
        }
    }

    public enum ApiChangeType
    {
        Show = 1,

        Episode = 2,
    }
}