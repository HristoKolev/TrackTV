﻿namespace TrackTv.Services.Data
{
    using System.Threading.Tasks;

    using LinqToDB;

    using TrackTv.Data;

    public class ProfilesRepository
    {
        public ProfilesRepository(DbService dbService)
        {
            this.DbService = dbService;
        }

        private DbService DbService { get; }

        public Task<int> CreateProfileAsync(string username)
        {
            return this.DbService.InsertAsync(new ProfilePoco
            {
                Username = username
            });
        }

        public Task<ProfilePoco> GetProfileByIdAsync(int profileId)
        {
            return this.DbService.Profiles.FirstOrDefaultAsync(poco => poco.ProfileId == profileId);
        }

        public Task<bool> ProfileExistsAsync(int profileId)
        {
            return this.DbService.Profiles.AnyAsync(x => x.ProfileId == profileId);
        }
    }
}