namespace TrackTv.Services.Profile
{
    using System.Threading.Tasks;

    using TrackTv.Services.Data;
    using TrackTv.Services.Profile.Model;

    public class ProfileService : IProfileService
    {
        public ProfileService(IProfilesRepository profilesRepository)
        {
            this.ProfilesRepository = profilesRepository;
        }

        private IProfilesRepository ProfilesRepository { get; }

        public async Task<FullProfile> GetProfileAsync(int profileId)
        {
            var profile = await this.ProfilesRepository.GetProfileByIdAsync(profileId);

            return new FullProfile
            {
                Id = profile.Id,
                Username = profile.Username
            };
        }
    }
}