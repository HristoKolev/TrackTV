namespace TrackTv.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Data;
    using TrackTv.Models.Joint;

    public class UsersRepository : IUsersRepository
    {
        public UsersRepository(IUsersStore context)
        {
            this.Context = context;
        }

        private IUsersStore Context { get; }

        public Task AddSubscriptionAsync(int userId, int showId)
        {
            this.Context.ShowsUsers.Add(new ShowsUsers(userId, showId));

            return this.Context.SaveChangesAsync();
        }

        public Task<bool> IsUserSubscribedAsync(int userId, int showId)
        {
            return this.Context.ShowsUsers.AnyAsync(x => (x.UserId == userId) && (x.ShowId == showId));
        }

        public Task RemoveSubscriptionAsync(int userId, int showId)
        {
            this.Context.ShowsUsers.Remove(new ShowsUsers(userId, showId));

            return this.Context.SaveChangesAsync();
        }
    }
}