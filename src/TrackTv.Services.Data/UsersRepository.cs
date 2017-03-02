namespace TrackTv.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Data;
    using TrackTv.Models.Joint;
    using TrackTv.Services.Data.Exceptions;

    public class UsersRepository : IUsersRepository
    {
        public UsersRepository(IUsersStore context)
        {
            this.Context = context;
        }

        private IUsersStore Context { get; }

        public async Task AddSubscriptionAsync(int userId, int showId)
        {
            var relationship =
                await this.Context.ShowsUsers.AsNoTracking().SingleOrDefaultAsync(r => r.UserId == userId && r.ShowId == showId);

            if (relationship != null)
            {
                throw new SubscriptionException($"The user is already subscribed to this show: (UserId={userId}, ShowId={showId})");
            }

            this.Context.ShowsUsers.Add(new ShowsUsers(userId, showId));

            await this.Context.SaveChangesAsync();
        }

        public Task<bool> IsUserSubscribedAsync(int userId, int showId)
        {
            return this.Context.ShowsUsers.AnyAsync(x => x.UserId == userId && x.ShowId == showId);
        }

        public async Task RemoveSubscriptionAsync(int userId, int showId)
        {
            var relationship =
                await this.Context.ShowsUsers.SingleOrDefaultAsync(r => r.UserId == userId && r.ShowId == showId);

            if (relationship == null)
            {
                throw new SubscriptionException($"The user is not subscribed to the specified show: (UserId={userId}, ShowId={showId})");
            }

            this.Context.ShowsUsers.Remove(relationship);

            await this.Context.SaveChangesAsync();
        }
    }
}