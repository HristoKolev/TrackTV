namespace TrackTV.Data.Repositories
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    public class UsersRepository : IUsersRepository
    {
        public UsersRepository(IUsersContext context)
        {
            this.Context = context;
        }

        private IUsersContext Context { get; }

        public Task<bool> IsUserSubscribedAsync(int userId, int showId)
        {
            return this.Context.ShowsUsers.AnyAsync(x => (x.UserId == userId) && (x.ShowId == showId));
        }
    }
}