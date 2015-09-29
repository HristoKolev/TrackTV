namespace TrackTV.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using TrackTV.Logic.Calendar;

    public class CalendarViewModel
    {
        [UIHint("MonthName")]
        public DateTime Date { get; set; }

        public DayOfWeek[] DaysOfWeek { get; set; }

        public List<List<CalendarDay>> Month { get; set; }
    }
}