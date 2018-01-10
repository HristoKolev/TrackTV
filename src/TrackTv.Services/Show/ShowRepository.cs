namespace TrackTv.Services.Show
{
    using System.Linq;
    using System.Threading.Tasks;

    using LinqToDB;

    using TrackTv.Data;

    public class ShowRepository
    {
        public ShowRepository(DbService dbService)
        {
            this.DbService = dbService;
        }

        private DbService DbService { get; }

        public Task<FullShow> GetShowWithNetworkByIdAsync(int showId)
        {
            var result = (from show in this.DbService.Shows
                          join network in this.DbService.Networks on show.NetworkId equals network.NetworkId
                          where show.ShowId == showId
                          select new FullShow
                          {
                              ShowId = show.ShowId,
                              TheTvDbId = show.TheTvDbId,
                              ShowName = show.ShowName,
                              FirstAired = show.FirstAired,
                              AirDay = show.AirDay,
                              AirTimeDate = show.AirTime,
                              ImdbId = show.ImdbId,
                              ShowBanner = show.ShowBanner,
                              ShowStatus = show.ShowStatus,
                              ShowDescription = show.ShowDescription,
                              NetworkName = network.NetworkName
                          }).FirstOrDefaultAsync();
            return result;
        }
    }
}