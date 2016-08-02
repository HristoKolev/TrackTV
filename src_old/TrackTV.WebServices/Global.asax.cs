namespace TrackTV.WebServices
{
    using System;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;

    using TrackTV.WebServices.Routing;

    public class WebApiApplication : HttpApplication
    {
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            this.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}