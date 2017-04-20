namespace TrackTv.WebServices.Infrastructure
{
    using System.Threading.Tasks;

    using Microsoft.Extensions.Caching.Memory;

    using TrackTv.Data.Models;
    using TrackTv.Services.Data;

    public class CacheProfilesRepository : IProfilesRepository
    {
        public CacheProfilesRepository(IProfilesRepository profilesRepository, IMemoryCache memoryCache)
        {
            this.ProfilesRepository = profilesRepository;
            this.MemoryCache = memoryCache;
        }

        private IMemoryCache MemoryCache { get; }

        private IProfilesRepository ProfilesRepository { get; }

        public Task<int> CreateProfileAsync(string username)
        {
            return this.ProfilesRepository.CreateProfileAsync(username);
        }

        public Task<Profile> GetProfileByIdAsync(int profileId)
        {
            return this.MemoryCache.GetOrCreateAsync(
                nameof(CacheProfilesRepository) + "_" + nameof(this.GetProfileByIdAsync) + "_" + profileId,
                entry => this.ProfilesRepository.GetProfileByIdAsync(profileId));
        }

        public Task<bool> ProfileExistsAsync(int profileId)
        {
            return this.MemoryCache.GetOrCreateAsync(
                nameof(CacheProfilesRepository) + "_" + nameof(this.ProfileExistsAsync) + "_" + profileId,
                entry => this.ProfilesRepository.ProfileExistsAsync(profileId));
        }
    }
}