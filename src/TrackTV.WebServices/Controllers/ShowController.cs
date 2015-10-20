namespace TrackTV.WebServices.Controllers
{
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using TrackTV.Services;

    public class ShowController : ApiController
    {
        public ShowController(ShowService showService)
        {
            this.ShowService = showService;
        }

        private ShowService ShowService { get; }

        public IHttpActionResult Get(string userFriendlyId)
        {
            var model = this.ShowService.Show(this.User.Identity.GetUserId(), userFriendlyId);

            if (model == null)
            {
                return this.NotFound();
            }

            return this.Ok(model);
        }
    }
}