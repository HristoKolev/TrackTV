namespace TrackTV.Web.Controllers
{
    using System.Web.Mvc;

    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult PageNotFound()
        {
            string model = "I don't know what... it's missing...";

            return this.View(model);
        }
    }
}