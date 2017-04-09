using System.Threading.Tasks;

namespace TrackTv.Services.Profile
{
    using TrackTv.Services.Profile.Model;

    public interface IProfileService
    {
        Task<FullProfile> GetProfileAsync(int profileId);
    }
}