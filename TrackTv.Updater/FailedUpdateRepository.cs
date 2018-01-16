namespace TrackTv.Updater
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LinqToDB;

    using TrackTv.Data;

    public class FailedUpdateRepository
    {
        public FailedUpdateRepository(IDbService dbService)
        {
            this.DbService = dbService;
        }

        private IDbService DbService { get; }

        public async Task AddFailedUpdate(FailedUpdatePoco poco)
        {
            var update = await this.DbService.FailedUpdates.FirstOrDefaultAsync(p => p.TheTvDbUpdateId == poco.TheTvDbUpdateId)
                                   .ConfigureAwait(false) ?? poco;

            update.FailedTime = poco.FailedTime;
            update.NumberOfFails++;

            await this.DbService.SaveAsync(update).ConfigureAwait(false);
        }

        public Task<List<FailedUpdatePoco>> GetFailedUpdates()
        {
            return this.DbService.FailedUpdates.ToListAsync();
        }

        public Task RemoveFailedUpdate(FailedUpdatePoco poco)
        {
            return this.DbService.DeleteAsync(poco);
        }
    }
}