namespace TrackTv.Services.Show
{
    using System.Threading.Tasks;

    using TrackTV.Data.Repositories;
    using TrackTV.Data.Repositories.Contracts;

    using TrackTv.Services.Show.Models;

    public class ShowService
    {
        public ShowService(IShowsRepository showsRepository, IUsersRepository usersRepository)
        {
            this.ShowsRepository = showsRepository;
            this.UsersRepository = usersRepository;
        }

        private IShowsRepository ShowsRepository { get; }

        private IUsersRepository UsersRepository { get; }

        public async Task<FullShow> GetFullShowAsync(int id)
        {
            var show = await this.ShowsRepository.GetShowWithNetworkById(id);

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

            return model;
        }

        public async Task<FullShow> GetFullShowAsync(int id, int userId)
        {
            var model = await this.GetFullShowAsync(id);

            model.IsUserSubscribed = await this.UsersRepository.IsUserSubscribedAsync(userId, id);

            return model;
        }
    }
}