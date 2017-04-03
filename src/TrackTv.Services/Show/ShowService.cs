namespace TrackTv.Services.Show
{
    using System.Threading.Tasks;

    using TrackTv.Services.Data;
    using TrackTv.Services.Show.Models;

    public class ShowService : IShowService
    {
        public ShowService(IShowsRepository showsRepository, IProfilesRepository profilesRepository)
        {
            this.ShowsRepository = showsRepository;
            this.ProfilesRepository = profilesRepository;
        }

        private IProfilesRepository ProfilesRepository { get; }

        private IShowsRepository ShowsRepository { get; }

        public async Task<FullShow> GetFullShowAsync(int id, int profileId = default(int))
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
                model.IsUserSubscribed = await this.ProfilesRepository.IsUserSubscribedAsync(profileId, id).ConfigureAwait(false);
            }

            return model;
        }
    }
}