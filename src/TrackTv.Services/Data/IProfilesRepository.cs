namespace TrackTv.Services.Data
{
    using System.Threading.Tasks;

    using TrackTv.Data.Models;

    public interface IProfilesRepository
    {
        Task<int> CreateProfileAsync(string username);

        Task<Profile> GetProfileByIdAsync(int profileId);

        Task<bool> ProfileExistsAsync(int profileId);
    }
}