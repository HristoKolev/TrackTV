namespace TrackTv.Services.MyShows
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TrackTv.Data.Models;
    using TrackTv.Services.Data;
    using TrackTv.Services.Data.Models;
    using TrackTv.Services.Exceptions;
    using TrackTv.Services.MyShows.Models;

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

            var showIds = await this.SubscriptionRepository.GetSubscriptionsByProfileIdAsync(profileId).ConfigureAwait(false);
 
            var episodesSummaries = await this.EpisodeRepository.GetEpisodesSummariesAsync(showIds, time).ConfigureAwait(false);

            return episodesSummaries;
        }

        private static MyEpisode EpisodeToModel(Episode episode)
        {
            if (episode == null)
            {
                return null;
            }

            return new MyEpisode
            {
                FirstAired = episode.FirstAired,
                EpisodeTitle = episode.EpisodeTitle,
                EpisodeNumber = episode.EpisodeNumber,
                SeasonNumber = episode.SeasonNumber
            };
        }

        private static MyShow[] MapToModels(IEnumerable<Show> shows, EpisodesSummary[] episodesSummaries)
        {
            var models = new List<MyShow>();

            foreach (var show in shows)
            {
                var summary = episodesSummaries.First(x => x.ShowId == show.ShowId);

                models.Add(new MyShow
                {
                    ShowName = show.ShowName,
                    ShowBanner = show.ShowBanner,
                    ShowStatus = show.ShowStatus,
                    LastEpisode = EpisodeToModel(summary.LastEpisode),
                    NextEpisode = EpisodeToModel(summary.NextEpisode),
                    ShowId = show.ShowId
                });
            }

            return models.ToArray();
        }
    }
}