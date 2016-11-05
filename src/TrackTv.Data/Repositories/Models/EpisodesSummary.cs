namespace TrackTv.Data.Repositories.Models
{
    using TrackTv.Models;

    public class EpisodesSummary
    {
        public Episode LastEpisode { get; set; }

        public Episode NextEpisode { get; set; }

        public int ShowId { get; set; }
    }
}