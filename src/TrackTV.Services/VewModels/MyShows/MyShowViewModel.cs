namespace TrackTV.Services.VewModels.MyShows
{
    using NetInfrastructure.AutoMapper;

    using TrackTV.Models;

    [MapFrom(typeof(Show))]
    public class MyShowViewModel
    {
        public int Id { get; set; }

        public SimpleEpisodeViewModel LastEpisode { get; set; }

        public string Name { get; set; }

        public SimpleEpisodeViewModel NextEpisode { get; set; }

        public string StringId { get; set; }
    }
}