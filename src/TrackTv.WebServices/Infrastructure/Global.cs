namespace TrackTv.WebServices.Infrastructure
{
    using Microsoft.Extensions.Configuration;

    public class Global
    {
        public static IConfigurationRoot AppConfig { get; set; }
    }
}