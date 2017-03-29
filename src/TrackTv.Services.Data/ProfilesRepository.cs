﻿namespace TrackTv.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Data;
    using TrackTv.Models;
    using TrackTv.Services.Data.Exceptions;

    public class ProfilesRepository : IProfilesRepository
    {
        public ProfilesRepository(TrackTvDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        private TrackTvDbContext DbContext { get; }

        public async Task AddSubscriptionAsync(int profileId, int showId)
        {
            var relationship =
                await this.DbContext.ShowsProfiles.AsNoTracking()
                          .SingleOrDefaultAsync(r => r.ProfileId == profileId && r.ShowId == showId)
                          .ConfigureAwait(false);

            if (relationship != null)
            {
                throw new SubscriptionException($"The user is already subscribed to this show: (ProfileId={profileId}, ShowId={showId})");
            }

            this.DbContext.ShowsProfiles.Add(new ShowsProfiles(profileId, showId));

            await this.DbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public Task<bool> IsUserSubscribedAsync(int profileId, int showId)
        {
            return this.DbContext.ShowsProfiles.AnyAsync(x => x.ProfileId == profileId && x.ShowId == showId);
        }

        public async Task RemoveSubscriptionAsync(int profileId, int showId)
        {
            var relationship =
                await this.DbContext.ShowsProfiles.SingleOrDefaultAsync(r => r.ProfileId == profileId && r.ShowId == showId)
                          .ConfigureAwait(false);

            if (relationship == null)
            {
                throw new SubscriptionException(
                    $"The user is not subscribed to the specified show: (ProfileId={profileId}, ShowId={showId})");
            }

            this.DbContext.ShowsProfiles.Remove(relationship);

            await this.DbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}