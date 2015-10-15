namespace TrackTV.Logic
{
    using System.Linq;

    using NetInfrastructure.Data.Repositories;

    using TrackTV.Models;

    public class UserManager
    {
        public UserManager(IRepository<ApplicationUser, string> users)
        {
            this.Users = users;
        }

        private IRepository<ApplicationUser, string> Users { get; }

        public ApplicationUser GetUserById(string id)
        {
            return this.Users.All().FirstOrDefault(user => user.Id == id);
        }
    }
}