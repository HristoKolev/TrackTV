namespace TrackTV.Services
{
    using System;
    using System.Linq;

    using NetInfrastructure.Data.Repositories;

    using TrackTV.Logic.Calendar;
    using TrackTV.Models;
    using TrackTV.Services.VewModels.Calendar;

    public class CalendarService
    {
        public CalendarService(IRepository<Episode> episodes)
        {
            this.Episodes = episodes;
        }

        private IRepository<Episode> Episodes { get; }

        public CalendarViewModel GetCalendarModel(string userId)
        {
            DateTime now = DateTime.Now;

            return this.GetCalendarModel(userId, now.Year, now.Month);
        }

        public CalendarViewModel GetCalendarModel(string userId, int year, int month)
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

            IQueryable<Episode> episodes =
                this.Episodes.All().Where(episode => episode.Season.Show.Subscribers.Any(user => user.Id == userId));

            EpisodeCalendar episodeCalendar = new EpisodeCalendar();

            CalendarViewModel model = new CalendarViewModel
            {
                Month = episodeCalendar.Create(episodes, date), 
                Date = date
            };

            return model;
        }
    }
}