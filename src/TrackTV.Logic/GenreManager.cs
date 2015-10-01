namespace TrackTV.Logic
{
    using System.Linq;

    using NetInfrastructure.Data.Repositories;

    using TrackTV.Models;

    public class GenreManager
    {
        public GenreManager(IRepository<Genre> genres)
        {
            this.Genres = genres;
        }

        private IRepository<Genre> Genres { get; set; }

        public IQueryable<Genre> GetAllGenres()
        {
            return this.Genres.All();
        }

        public Genre GetByStringId(string stringId)
        {
            return this.Genres.All().FirstOrDefault(g => g.StringId == stringId);
        }
    }
}