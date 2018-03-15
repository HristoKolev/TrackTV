namespace TrackTv.Updater
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LinqToDB;

    using Newtonsoft.Json;

    using TrackTv.Data;

    public class ApiChangeRepository
    {
        public ApiChangeRepository(IDbService dbService)
        {
            this.DbService = dbService;
        }

        private IDbService DbService { get; }

        public async Task AddFailedApiChange(ChangeListItem change)
        {
            var apiChange = await this.DbService.ApiChanges.FirstOrDefaultAsync(p => p.ApiChangeThetvdbid == change.TheTvDbID)
                                      .ConfigureAwait(false) ?? new ApiChangePoco();

            var utcNow = DateTime.UtcNow;

            apiChange.ApiChangeLastFailedTime = utcNow;
            apiChange.ApiChangeBody = JsonConvert.SerializeObject(change);

            apiChange.ApiChangeFailCount++;

            if (((IPoco)apiChange).IsNew())
            {
                apiChange.ApiChangeCreatedDate = utcNow;
                apiChange.ApiChangeThetvdbid = change.TheTvDbID;
                apiChange.ApiChangeThetvdbLastUpdated = change.LastUpdated;
            }

            await this.DbService.Save(apiChange).ConfigureAwait(false);
        }

        public async Task<List<ChangeListItem>> GetFailedUpdates()
        {
            var failedUpdates = await this.DbService.ApiChanges.ToListAsync().ConfigureAwait(false);

            return failedUpdates
                   .Select(p => p.ApiChangeBody)
                   .Select(JsonConvert.DeserializeObject<ChangeListItem>)
                   .ToList();
        }

        public async Task RemoveFailedUpdate(int thetvdbid)
        {
            var poco = await this.DbService.ApiChanges.FirstOrDefaultAsync(p => p.ApiChangeThetvdbid == thetvdbid).ConfigureAwait(false);

            if (poco != null)
            {
                await this.DbService.Delete(poco).ConfigureAwait(false);
            }
        }
    }
}