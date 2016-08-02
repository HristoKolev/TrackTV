namespace TrackTV.Services.VewModels.Shows
{
    using NetInfrastructure.AutoMapper.Attributes;

    using TrackTV.Models;

    [MapFrom(typeof(Genre))]
    public class GenreViewModel
    {
        public string Name { get; set; }

        public string UserFriendlyId { get; set; }
    }
}