namespace TrackTv.Services.Data
{
    using System.Threading.Tasks;

    using LinqToDB;

    using TrackTv.Data;

    public class ProfilesRepository
    {
        public ProfilesRepository(IDbService dbService)
        {
            this.DbService = dbService;
        }

        private IDbService DbService { get; }

        public Task<bool> ProfileExistsAsync(int profileId)
        {
            return this.DbService.Poco.Profiles.AnyAsync(x => x.ProfileID == profileId);
        }
    }
}