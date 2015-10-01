namespace TrackTV.Logic
{
    using System.Linq;

    using TrackTV.Data;
    using TrackTV.Models;

    public class ShowManager
    {
        private readonly ITrackTVData data;

        public ShowManager(ITrackTVData data)
        {
            this.data = data;
        }

        public IQueryable<Show> GetEndedShows()
        {
            return Order(this.GetShows(ShowStatus.Ended));
        }

        public IQueryable<Show> GetEndedShowsByGenre(int id)
        {
            return Order(FilterByGenre(this.GetShows(ShowStatus.Ended), id));
        }

        public IQueryable<Show> GetRunningShows()
        {
            return Order(this.GetShows(ShowStatus.Continuing));
        }

        public IQueryable<Show> GetRunningShowsByGenre(int id)
        {
            return Order(FilterByGenre(this.GetShows(ShowStatus.Continuing), id));
        }

        public Show GetShowById(int id)
        {
            return this.GetAllShows().FirstOrDefault(show => show.Id == id);
        }

        public Show GetShowByStringId(string stringId)
        {
            return this.GetAllShows().FirstOrDefault(show => show.StringId == stringId);
        }

        public IQueryable<Show> GetShowsByNetwork(int id)
        {
            return this.GetAllShows().Where(show => show.Network.Id == id);
        }

        public IQueryable<Show> GetUserShows(string userId)
        {
            return this.GetAllShows().Where(show => show.Subscribers.Any(user => user.Id == userId));
        }

        public void RemoveShow(int id)
        {
            this.data.Shows.Delete(id);
            this.data.SaveChanges();
        }

        public IQueryable<Show> SearchShow(string query)
        {
            query = query.Trim();

            return Order(this.GetAllShows().Where(show => show.Name.Contains(query)));
        }

        private static IQueryable<Show> FilterByGenre(IQueryable<Show> shows, int id)
        {
            return shows.Where(show => show.Genres.Any(g => g.Id == id));
        }

        private static IQueryable<Show> Order(IQueryable<Show> shows)
        {
            return shows.OrderByDescending(show => show.Subscribers.Count).ThenByDescending(show => show.Name);
        }

        private IQueryable<Show> GetAllShows()
        {
            return this.data.Shows.All();
        }

        private IQueryable<Show> GetShows(ShowStatus status)
        {
            return this.GetAllShows().Where(show => show.Status == status);
        }
    }
}