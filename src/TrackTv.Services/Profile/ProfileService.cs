namespace TrackTv.Services.Profile
{
    using System.Threading.Tasks;

    using TrackTv.Services.Data;

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

    public class FullProfile
    {
        public int Id { get; set; }

        public string Username { get; set; }
    }
}