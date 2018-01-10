namespace TrackTv.Services.MyShows
{
    using TrackTv.Data.Enums;

    public class MyShow
    {
        public MyEpisode LastEpisode { get; set; }

        public MyEpisode NextEpisode { get; set; }

        public string ShowBanner { get; set; }

        public int ShowId { get; set; }

        public string ShowName { get; set; }

        public ShowStatus ShowStatus { get; set; }
    }
}