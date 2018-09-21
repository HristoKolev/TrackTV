namespace TrackTv.Services
{
    using System.Threading.Tasks;

    using LinqToDB;

    using TrackTv.Data;
    using TrackTv.Services.Exceptions;

    public class ProfileService
    {
        public ProfileService(IDbService dbService)
        {
            this.DbService = dbService;
        }

        private IDbService DbService { get; }
 

        public Task<int> CreateProfileAsync(string username)
        {
            return this.DbService.Insert(new ProfilePoco
            {
                ProfileName = username
            });
        }

        public async Task<FullProfile> GetProfileAsync(int profileId)
        {
            var profile = await this.DbService.Poco.Profiles.FirstOrDefaultAsync(poco => poco.ProfileID == profileId);

            if (profile == null)
            {
                throw new ProfileNotFoundException(profileId);
            }

            return new FullProfile
            {
                Username = profile.ProfileName
            };
        }
    }

    public class FullProfile
    {
        public string Username { get; set; }
    }
}