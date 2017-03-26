namespace TrackTv.Services.Calendar
{
    using System;
    using System.Threading.Tasks;

    using TrackTv.Services.Calendar.Models;

    public interface IEpisodeCalendar
    {
        Task<CalendarDay[][]> CreateAsync(int profileId, DateTime currentDate);
    }
}