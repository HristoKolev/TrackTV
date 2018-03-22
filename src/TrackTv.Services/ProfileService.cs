namespace TrackTv.Services
{
    using System.Threading.Tasks;

    using LinqToDB;

    using TrackTv.Data;
    using TrackTv.Services.Data;
    using TrackTv.Services.Exceptions;

    public class ProfileService
    {
        public ProfileService(ProfilesRepository profilesRepository, IDbService dbService)
        {
            this.ProfilesRepository = profilesRepository;
            this.DbService = dbService;
        }

        private IDbService DbService { get; }

        private ProfilesRepository ProfilesRepository { get; }

        public Task<int> CreateProfileAsync(string username)
        {
            return this.DbService.Insert(new ProfilePoco
            {
                ProfileName = username
            });
        }

        public async Task<FullProfile> GetProfileAsync(int profileId)
        {
            var profile = await this.DbService.Profiles.FirstOrDefaultAsync(poco => poco.ProfileID == profileId).ConfigureAwait(false);

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