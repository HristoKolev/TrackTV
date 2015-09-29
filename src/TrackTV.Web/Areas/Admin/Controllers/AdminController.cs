namespace TrackTV.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;

    using TrackTV.Data.Contracts;
    using TrackTV.Web.Controllers;

    [Authorize(Roles = "Admin")]
    public abstract class AdminController : BaseController
    {
        public AdminController(ITrackTVData data)
            : base(data)
        {
        }
    }
}