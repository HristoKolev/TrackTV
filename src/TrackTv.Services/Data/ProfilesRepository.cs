﻿namespace TrackTv.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Data;
    using TrackTv.Data.Models;

    public class ProfilesRepository : IProfilesRepository
    {
        public ProfilesRepository(TrackTvDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        private TrackTvDbContext DbContext { get; }

        public async Task<int> CreateProfileAsync(string username)
        {
            var profile = new Profile(username);

            this.DbContext.Profiles.Add(profile);

            await this.DbContext.SaveChangesAsync().ConfigureAwait(false);

            return profile.Id;
        }

        public Task<Profile> GetProfileByIdAsync(int profileId)
        {
            return this.DbContext.Profiles.AsNoTracking().FirstOrDefaultAsync(x => x.Id == profileId);
        }
    }
}