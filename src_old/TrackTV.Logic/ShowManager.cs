namespace TrackTV.Logic
{
    using System.Linq;

    using NetInfrastructure.Data.Repositories;

    using TrackTV.Models;

    public class ShowManager
    {
        public ShowManager(IRepository<Show> shows)
        {
            this.Shows = shows;
        }

        private IRepository<Show> Shows { get; }

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

        public Show GetShowByUserFriendlyId(string userFriendlyId)
        {
            return this.GetAllShows().FirstOrDefault(show => show.UserFriendlyId == userFriendlyId);
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
            this.Shows.Delete(id);
            this.Shows.SaveChanges();
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
            return this.Shows.All();
        }

        private IQueryable<Show> GetShows(ShowStatus status)
        {
            return this.GetAllShows().Where(show => show.Status == status);
        }
    }
}