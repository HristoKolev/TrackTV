namespace TrackTv.Services.Show
{
    using System.Threading.Tasks;

    using TrackTv.Services.Data;
    using TrackTv.Services.Show.Models;

    public class ShowService : IShowService
    {
        public ShowService(IShowsRepository showsRepository, ISubscriptionRepository subscriptionRepository)
        {
            this.ShowsRepository = showsRepository;
            this.SubscriptionRepository = subscriptionRepository;
        }

        private IShowsRepository ShowsRepository { get; }

        private ISubscriptionRepository SubscriptionRepository { get; }

        public async Task<FullShow> GetFullShowAsync(int id, int profileId)
        {
            var show = await this.ShowsRepository.GetShowWithNetworkByIdAsync(id).ConfigureAwait(false);

            var model = new FullShow
            {
                Id = show.Id,
                TheTvDbId = show.TheTvDbId,
                Name = show.Name,
                FirstAired = show.FirstAired,
                AirDay = show.AirDay,
                ImdbId = show.ImdbId,
                Banner = show.Banner,
                Status = show.Status,
                Description = show.Description,
                NetworkName = show.Network.Name
            };

            if (show.AirTime.HasValue)
            {
                model.AirTime = new AirTime(show.AirTime.Value.Hour, show.AirTime.Value.Minute);
            }

            if (profileId != default(int))
            {
                model.IsUserSubscribed = await this.SubscriptionRepository.IsUserSubscribedAsync(profileId, id).ConfigureAwait(false);
            }

            return model;
        }
    }
}