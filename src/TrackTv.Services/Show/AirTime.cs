﻿namespace TrackTv.Services.Show
{
    public class AirTime
    {
        public AirTime(int hour, int minute)
        {
            this.Hour = hour;
            this.Minute = minute;
        }

        public int Hour { get; set; }

        public int Minute { get; set; }
    }
}