namespace TrackTV.Services
{
    using System;

    using NetInfrastructure.Data.Repositories;

    using TrackTV.Logic.Calendar;
    using TrackTV.Models;
    using TrackTV.Services.VewModels;
    using TrackTV.Services.VewModels.Calendar;

    public class CalendarService
    {
        public CalendarService(IRepository<Episode> episodes)
        {
            this.Episodes = episodes;
        }

        private IRepository<Episode> Episodes { get; }

        public CalendarViewModel GetCalendarModel(string useerId)
        {
            DateTime now = DateTime.Now;

            return this.GetCalendarModel(now.Year, now.Month, useerId);
        }

        public CalendarViewModel GetCalendarModel(int year, int month, string useerId)
        {
            DateTime date;

            try
            {
                date = new DateTime(year, month, 1);
            }
            catch (ArgumentOutOfRangeException)
            {
                date = DateTime.Now;
            }

            EpisodeCalendar episodeCalendar = new EpisodeCalendar(this.Episodes, useerId);

            DayOfWeek[] daysOfWeek = {
                DayOfWeek.Monday, 
                DayOfWeek.Tuesday, 
                DayOfWeek.Wednesday, 
                DayOfWeek.Thursday, 
                DayOfWeek.Friday, 
                DayOfWeek.Saturday, 
                DayOfWeek.Sunday
            };

            CalendarViewModel model = new CalendarViewModel
            {
                Month = episodeCalendar.Create(date), 
                Date = date, 
                DaysOfWeek = daysOfWeek
            };
            return model;
        }
    }
}