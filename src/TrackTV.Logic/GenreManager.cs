namespace TrackTV.Logic
{
    using System.Linq;

    using TrackTV.Data;
    using TrackTV.Models;

    public class GenreManager
    {
        private readonly ITrackTVData data;

        public GenreManager(ITrackTVData data)
        {
            this.data = data;
        }

        public IQueryable<Genre> GetAllGenres()
        {
            return this.data.Genres.All();
        }

        public Genre GetByStringId(string stringId)
        {
            return this.data.Genres.All().FirstOrDefault(g => g.StringId == stringId);
        }
    }
}