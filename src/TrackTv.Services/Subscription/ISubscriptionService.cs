namespace TrackTv.Services.Subscription
{
    using System.Threading.Tasks;

    public interface ISubscriptionService
    {
        Task Subscribe(int profileId, int showId);

        Task Unsubscribe(int profileId, int showId);
    }
}