namespace TrackTV.Services.VewModels.ShowDetails
{
    public class Time
    {
        public Time()
        {
        }

        public Time(int hours, int minutes)
        {
            this.Hours = hours;
            this.Minutes = minutes;
        }

        public int Hours { get; set; }

        public int Minutes { get; set; }
    }
}