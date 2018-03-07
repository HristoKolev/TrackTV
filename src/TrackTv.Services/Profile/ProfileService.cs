namespace TrackTv.Services.Profile
{
    using System.Threading.Tasks;

    using TrackTv.Services.Data;
    using TrackTv.Services.Exceptions;

    public class ProfileService
    {
        public ProfileService(ProfilesRepository profilesRepository)
        {
            this.ProfilesRepository = profilesRepository;
        }

        private ProfilesRepository ProfilesRepository { get; }

        public Task<int> CreateProfileAsync(string username)
        {
            return this.ProfilesRepository.CreateProfileAsync(username);
        }

        public async Task<FullProfile> GetProfileAsync(int profileId)
        {
            var profile = await this.ProfilesRepository.GetProfileByIdAsync(profileId).ConfigureAwait(false);

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
}