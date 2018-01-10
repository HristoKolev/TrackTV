namespace TrackTv.Services.MyShows
{
    using System;
    using System.Threading.Tasks;

    using TrackTv.Services.Data;
    using TrackTv.Services.Exceptions;

    public class MyShowsService
    {
        public MyShowsService(
            EpisodeRepository episodeRepository,
            SubscriptionRepository subscriptionRepository,
            ProfilesRepository profilesRepository)
        {
            this.EpisodeRepository = episodeRepository;
            this.SubscriptionRepository = subscriptionRepository;
            this.ProfilesRepository = profilesRepository;
        }

        private EpisodeRepository EpisodeRepository { get; }

        private ProfilesRepository ProfilesRepository { get; }

        private SubscriptionRepository SubscriptionRepository { get; }

        public async Task<MyShow[]> GetAllAsync(int profileId, DateTime time)
        {
            if (!await this.ProfilesRepository.ProfileExistsAsync(profileId).ConfigureAwait(false))
            {
                throw new ProfileNotFoundException(profileId);
            }

            var showIds = await this.SubscriptionRepository.GetSubscriptionIdsByProfileIdAsync(profileId).ConfigureAwait(false);

            var episodesSummaries = await this.EpisodeRepository.GetEpisodesSummariesAsync(showIds, time).ConfigureAwait(false);

            return episodesSummaries;
        }
    }
}