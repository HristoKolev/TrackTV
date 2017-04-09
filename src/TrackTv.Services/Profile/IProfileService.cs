namespace TrackTv.Services.Profile
{
    using System.Threading.Tasks;

    using TrackTv.Services.Profile.Model;

    public interface IProfileService
    {
        Task<int> CreateProfileAsync(string username);

        Task<FullProfile> GetProfileAsync(int profileId);
    }
}