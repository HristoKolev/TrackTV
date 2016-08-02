namespace TrackTV.Logic
{
    using System.Linq;

    using NetInfrastructure.Data.Repositories;

    using TrackTV.Models;

    public class EpisodeManager
    {
        public EpisodeManager(IRepository<Episode> episodes)
        {
            this.Episodes = episodes;
        }

        private IRepository<Episode> Episodes { get; }

        public Episode GetLastEpisode(Show show)
        {
            Episode lastEpisode = null;

            if (show.LastEpisodeId.HasValue)
            {
                lastEpisode = this.GetById(show.LastEpisodeId.Value);
            }

            return lastEpisode;
        }

        public Episode GetNextEpisode(Show show)
        {
            Episode nextEpisode = null;

            if (show.NextEpisodeId.HasValue)
            {
                nextEpisode = this.GetById(show.NextEpisodeId.Value);
            }

            return nextEpisode;
        }

        public IQueryable<Episode> GetSeasonEpisodes(int showId, int seassonNumber)
        {
            return
                this.GetAllEpisodes()
                    .Where(episode => episode.Season.Show.Id == showId && episode.Season.Number == seassonNumber)
                    .OrderBy(episode => episode.Number);
        }

        private IQueryable<Episode> GetAllEpisodes()
        {
            return this.Episodes.All();
        }

        private Episode GetById(int id)
        {
            return this.GetAllEpisodes().FirstOrDefault(episode => episode.Id == id);
        }
    }
}