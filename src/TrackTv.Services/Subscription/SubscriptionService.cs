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

        public async Task Subscribe(int userId, int showId)
        {
            await this.UsersRepository.AddSubscriptionAsync(userId, showId);
        }

        public async Task UnSubscribe(int userId, int showId)
        {
            await this.UsersRepository.RemoveSubscriptionAsync(userId, showId);
        }
    }
}