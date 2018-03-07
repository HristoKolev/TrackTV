namespace TrackTv.Services.Show
{
    using System.Linq;
    using System.Threading.Tasks;

    using LinqToDB;

    using TrackTv.Data;

    public class ShowRepository
    {
        public ShowRepository(IDbService dbService)
        {
            this.DbService = dbService;
        }

        private IDbService DbService { get; }

        public Task<FullShow> GetShowWithNetworkByIdAsync(int showId)
        {
            var result = (from show in this.DbService.Shows
                          join network in this.DbService.Networks on show.NetworkID equals network.NetworkID
                          where show.ShowID == showId
                          select new FullShow
                          {
                              ShowId = show.ShowID,
                              TheTvDbId = show.Thetvdbid,
                              ShowName = show.ShowName,
                              FirstAired = show.FirstAired,
                              AirDay = show.AirDay,
                              AirTimeDate = show.AirTime,
                              ImdbId = show.Imdbid,
                              ShowBanner = show.ShowBanner,
                              ShowStatus = show.ShowStatus,
                              ShowDescription = show.ShowDescription,
                              NetworkName = network.NetworkName
                          }).FirstOrDefaultAsync();
            return result;
        }
    }
}