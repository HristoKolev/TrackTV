using System.Threading.Tasks;

namespace TrackTv.Services.Profile
{
    public interface IProfileService
    {
        Task<FullProfile> GetProfileAsync(int profileId);
    }
}