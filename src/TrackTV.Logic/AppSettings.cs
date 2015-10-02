namespace TrackTV.Logic
{
    using System.Web;

    using NetInfrastructure.Configuration;

    public class AppSettings : IAppSettings
    {
        public AppSettings(IConfigurationDocument document)
        {
            this.Section = "TrackTV";
            this.Document = document;
            this.Document.DefaultSection = this.Section;
        }

        public string ApiKey => this.Document["TheTVDBApiKey"];

        public string BannerDirectory => HttpContext.Current.Server.MapPath(this.BannerPath.Replace("/", "\\"));

        public string BannerPath => this.Document["BannerPath"];

        public string Name => this.Document["Name"];

        public string PosterDirectory => HttpContext.Current.Server.MapPath(this.PosterPath.Replace("/", "\\"));

        public string PosterPath => this.Document["PosterPath"];

        public string Section { get; set; }

        private IConfigurationDocument Document { get; }
    }
}