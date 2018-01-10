namespace TrackTv.Services.MyShows
{
    using TrackTv.Data.Enums;

    public class MyShow
    {
        public string ShowBanner { get; set; }

        public MyEpisode LastEpisode { get; set; }

        public string ShowName { get; set; }

        public MyEpisode NextEpisode { get; set; }

        public ShowStatus ShowStatus { get; set; }

        public int ShowId { get; set; }
    }
}