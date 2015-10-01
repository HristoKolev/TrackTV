namespace TrackTV.Logic
{
    using System.Configuration;
    using System.Web;

    public static class ApplicationSettings
    {
        public static string ApiKey => ConfigurationManager.AppSettings["TheTVDBApiKey"];

        public static string BannerDirectory => HttpContext.Current.Server.MapPath(BannerPath.Replace("/", "\\"));

        public static string BannerPath => ConfigurationManager.AppSettings["BannerPath"];

        public static string Name => ConfigurationManager.AppSettings["Name"];

        public static string PosterDirectory => HttpContext.Current.Server.MapPath(PosterPath.Replace("/", "\\"));

        public static string PosterPath => ConfigurationManager.AppSettings["PosterPath"];
    }
}