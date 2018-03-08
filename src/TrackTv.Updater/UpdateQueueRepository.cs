namespace TrackTv.Updater
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LinqToDB;

    using TrackTv.Data;

    public class UpdateQueueRepository
    {
        public UpdateQueueRepository(IDbService dbService)
        {
            this.DbService = dbService;
        }

        private IDbService DbService { get; }

        public async Task AddFailedUpdate(UpdateQueuePoco poco)
        {
            var update = await this.DbService.UpdateQueue.FirstOrDefaultAsync(p => p.ThetvdbUpdateID == poco.ThetvdbUpdateID)
                                   .ConfigureAwait(false) ?? poco;

            update.LastFailedTime = poco.LastFailedTime;
            update.FailCount++;

            await this.DbService.Save(update).ConfigureAwait(false);
        }

        public Task<List<UpdateQueuePoco>> GetFailedUpdates()
        {
            return this.DbService.UpdateQueue.ToListAsync();
        }

        public Task RemoveFailedUpdate(UpdateQueuePoco poco)
        {
            return this.DbService.Delete(poco);
        }
    }
}