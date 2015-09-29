namespace TrackTV.Web.ViewModels.MyShows
{
    using TrackTV.Models;
    using TrackTV.Web.Infrastructure.Mapping.Contracts;

    public class MyShowViewModel : IMapFrom<Show>
    {
        public int Id { get; set; }

        public SimpleEpisodeViewModel LastEpisode { get; set; }

        public string Name { get; set; }

        public SimpleEpisodeViewModel NextEpisode { get; set; }

        public string StringId { get; set; }
    }
}