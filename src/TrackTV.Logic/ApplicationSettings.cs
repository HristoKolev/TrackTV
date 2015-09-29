namespace TrackTV.Logic
{
    using System.Configuration;
    using System.Web;

    public static class ApplicationSettings
    {
        public static string ApiKey
        {
            get
            {
                return ConfigurationManager.AppSettings["TheTVDBApiKey"];
            }
        }

        public static string BannerDirectory
        {
            get
            {
                return HttpContext.Current.Server.MapPath(BannerPath.Replace("/", "\\"));
            }
        }

        public static string BannerPath
        {
            get
            {
                return ConfigurationManager.AppSettings["BannerPath"];
            }
        }

        public static string Name
        {
            get
            {
                return ConfigurationManager.AppSettings["Name"];
            }
        }

        public static string PosterDirectory
        {
            get
            {
                return HttpContext.Current.Server.MapPath(PosterPath.Replace("/", "\\"));
            }
        }

        public static string PosterPath
        {
            get
            {
                return ConfigurationManager.AppSettings["PosterPath"];
            }
        }
    }
}