namespace TrackTV.Logic.Extensions
{
    using System;

    public static class DateTimeExtensions
    {
        public static bool IsToday(this DateTime obj)
        {
            DateTime now = DateTime.Now;

            return obj.Year == now.Year && obj.Month == now.Month && obj.Day == now.Day;
        }
    }
}