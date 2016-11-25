namespace TrackTv.Services.Subscription
{
    using System.Threading.Tasks;

    using TrackTv.Services.Data;

    public class SubscriptionService
    {
        public SubscriptionService(IUsersRepository usersRepository)
        {
            this.UsersRepository = usersRepository;
        }

        private IUsersRepository UsersRepository { get; }

        public Task Subscribe(int userId, int showId)
        {
            return this.UsersRepository.AddSubscriptionAsync(userId, showId);
        }

        public Task UnSubscribe(int userId, int showId)
        {
            return this.UsersRepository.RemoveSubscriptionAsync(userId, showId);
        }
    }
}