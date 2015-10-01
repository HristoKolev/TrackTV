namespace TrackTV.Logic.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using AutoMapper.QueryableExtensions;

    using NetInfrastructure.Data.Repositories;

    using TrackTV.Models;

    public class EpisodeCalendar
    {
        private const int NumberOfWeeks = 6;

        private readonly List<List<CalendarDay>> model;

        private readonly string userId;

        private List<CalendarEpisode> episodes;

        private int weekIndex;

        public EpisodeCalendar(IRepository<Episode> episodes, string userId)
        {
            this.Episodes = episodes;
            this.userId = userId;

            this.model = ConstructModel();
        }

        private IRepository<Episode> Episodes { get; set; }

        public List<List<CalendarDay>> Create(DateTime currentDate)
        {
            Calendar calendar = new GregorianCalendar();

            const int CalendarDays = 42;

            DateTime startDate = new DateTime(currentDate.Year, currentDate.Month, 1);

            startDate = startDate.Subtract(new TimeSpan((int)calendar.GetDayOfWeek(startDate) - (int)DayOfWeek.Monday, 0, 0, 0));

            DateTime endDate = startDate.Add(new TimeSpan(CalendarDays, 0, 0, 0));

            this.episodes = this.GetEpisodes(startDate, endDate);

            this.weekIndex = 0;

            for (int i = 0; i < CalendarDays; i++)
            {
                DayOfWeek dayOfWeek = calendar.GetDayOfWeek(startDate);

                this.AddEpisodes(startDate);

                startDate = startDate.AddDays(1);

                if (dayOfWeek == DayOfWeek.Sunday)
                {
                    this.weekIndex++;
                }
            }

            return this.model;
        }

        private static List<List<CalendarDay>> ConstructModel()
        {
            List<List<CalendarDay>> model = new List<List<CalendarDay>>();

            for (int i = 0; i < NumberOfWeeks; i++)
            {
                model.Add(new List<CalendarDay>());
            }

            return model;
        }

        private void AddEpisodes(DateTime startDate)
        {
            DateTime endDate = startDate.AddDays(1);

            List<CalendarEpisode> episodesForDay =
                this.episodes.Where(
                    viewModel => viewModel.FirstAired != null && viewModel.FirstAired.Value >= startDate && viewModel.FirstAired < endDate)
                    .ToList();

            CalendarDay day = new CalendarDay
            {
                Date = startDate, 
                Episodes = episodesForDay
            };

            if (episodesForDay.Any())
            {
                this.model[this.weekIndex].Add(day);
            }
            else
            {
                this.model[this.weekIndex].Add(new CalendarDay(startDate));
            }
        }

        private List<CalendarEpisode> GetEpisodes(DateTime startDay, DateTime endDay)
        {
            List<CalendarEpisode> episodes =
                this.Episodes.All()
                    .Where(
                        episode =>
                        episode.Season.Show.Subscribers.Any(user => user.Id == this.userId) && episode.FirstAired > startDay
                        && episode.FirstAired < endDay)
                    .Project()
                    .To<CalendarEpisode>()
                    .ToList();
            return episodes;
        }
    }
}