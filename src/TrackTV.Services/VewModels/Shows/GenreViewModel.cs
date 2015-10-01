namespace TrackTV.Services.VewModels.Shows
{
    using NetInfrastructure.AutoMapper;

    using TrackTV.Models;

    [MapFrom(typeof(Genre))]
    public class GenreViewModel
    {
        public string Name { get; set; }

        public string UserFriendlyId { get; set; }
    }
}