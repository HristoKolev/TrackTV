namespace TrackTV.Web.ViewModels.ShowDetails
{
    using System;

    using TrackTV.Models;

    public class AirTimeViewModel
    {
        public AirTimeViewModel(AirDay? airDay, TimeSpan? airTime)
        {
            this.AirDay = airDay;
            this.AirTime = airTime;
        }

        public AirDay? AirDay { get; set; }

        public TimeSpan? AirTime { get; set; }
    }
}