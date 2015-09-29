namespace TrackTV.Web.ViewModels.Shows
{
    using TrackTV.Models;
    using TrackTV.Web.Infrastructure.Mapping.Contracts;

    public class GenreViewModel : IMapFrom<Genre>
    {
        public string Name { get; set; }

        public string StringId { get; set; }
    }
}