﻿namespace TrackTv.Services.Calendar
{
    using System;
    using System.Threading.Tasks;

    using TrackTv.Services.Calendar.Models;

    public interface ICalendarService
    {
        Task<CalendarDay[][]> GetCalendarAsync(int profileId, DateTime time);
    }
}