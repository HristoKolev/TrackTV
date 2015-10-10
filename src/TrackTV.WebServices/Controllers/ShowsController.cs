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
        public IHttpActionResult Top()
        {
            return this.Ok(this.Shows.GetTopShows());
        }
    }
}