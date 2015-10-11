namespace TrackTV.WebServices.Controllers
{
    using System.Web.Http;

    using TrackTV.Services;

    public class ShowsController : ApiController
    {
        public ShowsController(ShowsService shows)
        {
            this.Shows = shows;
        }

        private ShowsService Shows { get; }

        [HttpGet]
        public IHttpActionResult Genre(string genre)
        {
            var shows = this.Shows.GetByGenre(genre);

            if (shows == null)
            {
                return this.NotFound();
            }

            return this.Ok(shows);
        }

        [HttpGet]
        public IHttpActionResult Search(string query, int? page)
        {
            return this.Ok(this.Shows.Search(query, page));
        }

        [HttpGet]
        public IHttpActionResult Top()
        {
            return this.Ok(this.Shows.GetTopShows());
        }
    }
}