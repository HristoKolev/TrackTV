namespace TrackTV.Logic.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using AutoMapper.QueryableExtensions;

    using TrackTV.Models;

    public class EpisodeCalendar
    {
        private const int NumberOfWeeks = 6;

        public List<List<CalendarDay>> Create(IQueryable<Episode> episodeCollection, DateTime currentDate)
        {
            List<List<CalendarDay>> model = this.ConstructModel();

            Calendar calendar = new GregorianCalendar();

            const int CalendarDays = 42;

            DateTime startDate = new DateTime(currentDate.Year, currentDate.Month, 1);

            startDate = startDate.Subtract(new TimeSpan((int)calendar.GetDayOfWeek(startDate) - (int)DayOfWeek.Monday, 0, 0, 0));

            DateTime endDate = startDate.Add(new TimeSpan(CalendarDays, 0, 0, 0));

            List<CalendarEpisode> episodes = this.GetEpisodes(episodeCollection, startDate, endDate);

            int weekIndex = 0;

            for (int i = 0; i < CalendarDays; i++)
            {
                DayOfWeek dayOfWeek = calendar.GetDayOfWeek(startDate);

                this.AddEpisodes(model, episodes, startDate, weekIndex);

                startDate = startDate.AddDays(1);

                if (dayOfWeek == DayOfWeek.Sunday)
                {
                    weekIndex++;
                }
            }

            return model;
        }

        private void AddEpisodes(List<List<CalendarDay>> model, List<CalendarEpisode> episodes, DateTime startDate, int weekIndex)
        {
            DateTime endDate = startDate.AddDays(1);

            List<CalendarEpisode> episodesForDay =
                episodes.Where(
                    viewModel => viewModel.FirstAired != null && viewModel.FirstAired.Value >= startDate && viewModel.FirstAired < endDate)
                        .ToList();

            CalendarDay day = new CalendarDay
            {
                Date = startDate, 
                Episodes = episodesForDay
            };

            if (episodesForDay.Any())
            {
                model[weekIndex].Add(day);
            }
            else
            {
                model[weekIndex].Add(new CalendarDay(startDate));
            }
        }

        private List<List<CalendarDay>> ConstructModel()
        {
            List<List<CalendarDay>> model = new List<List<CalendarDay>>();

            for (int i = 0; i < NumberOfWeeks; i++)
            {
                model.Add(new List<CalendarDay>());
            }

            return model;
        }

        private List<CalendarEpisode> GetEpisodes(IQueryable<Episode> episodeCollection, DateTime startDay, DateTime endDay)
        {
            List<CalendarEpisode> episodes =
                episodeCollection.Where(episode => episode.FirstAired > startDay && episode.FirstAired < endDay)
                                 .Project()
                                 .To<CalendarEpisode>()
                                 .ToList();

            return episodes;
        }
    }
}