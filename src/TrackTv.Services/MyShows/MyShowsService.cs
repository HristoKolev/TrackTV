namespace TrackTv.Services.MyShows
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TrackTv.Data.Models;
    using TrackTv.Services.Data;
    using TrackTv.Services.Data.Models;
    using TrackTv.Services.MyShows.Models;

    public class MyShowsService : IMyShowsService
    {
        public MyShowsService(IShowsRepository showsRepository, IEpisodeRepository episodeRepository)
        {
            this.ShowsRepository = showsRepository;
            this.EpisodeRepository = episodeRepository;
        }

        private IEpisodeRepository EpisodeRepository { get; }

        private IShowsRepository ShowsRepository { get; }

        public async Task<MyShow[]> GetAllAsync(int profileId)
        {
            var now = DateTime.UtcNow;

            var shows = await this.ShowsRepository.GetShowsByProfileIdAsync(profileId).ConfigureAwait(false);

            var ids = shows.Select(x => x.Id).ToArray();

            var episodesSummaries = await this.EpisodeRepository.GetEpisodesSummariesAsync(ids, now).ConfigureAwait(false);

            return MapToModels(shows, episodesSummaries);
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
                Title = episode.Title,
                Number = episode.Number,
                SeasonNumber = episode.SeasonNumber
            };
        }

        private static MyShow[] MapToModels(IEnumerable<Show> shows, EpisodesSummary[] episodesSummaries)
        {
            var models = new List<MyShow>();

            foreach (var show in shows)
            {
                var summary = episodesSummaries.First(x => x.ShowId == show.Id);

                models.Add(new MyShow
                {
                    Name = show.Name,
                    Banner = show.Banner,
                    Status = show.Status,
                    LastEpisode = EpisodeToModel(summary.LastEpisode),
                    NextEpisode = EpisodeToModel(summary.NextEpisode)
                });
            }

            return models.ToArray();
        }
    }
}