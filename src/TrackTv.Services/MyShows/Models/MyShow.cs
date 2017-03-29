namespace TrackTv.Services.MyShows.Models
{
    using TrackTv.Data.Models.Enums;

    public class MyShow
    {
        public string Banner { get; set; }

        public MyEpisode LastEpisode { get; set; }

        public string Name { get; set; }

        public MyEpisode NextEpisode { get; set; }

        public ShowStatus Status { get; set; }
    }
}