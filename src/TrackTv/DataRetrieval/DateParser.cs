namespace TrackTv.DataRetrieval
{
    using System;
    using System.Globalization;

    public class DateParser
    {
        public DateTime? ParseAirTime(string value)
        {
            value = value.Trim();

            string abbreviation = null;

            if (value.ToLower().EndsWith("am") || value.ToLower().EndsWith("pm"))
            {
                abbreviation = value.Substring(value.Length - 2, 2).ToLower();
                value = value.Remove(value.Length - 2, 2).Trim();
            }

            string hoursAndMinutes = value;

            if (!hoursAndMinutes.Contains(":"))
            {
                return null;
            }

            string stringHours = hoursAndMinutes.Split(':')[0];

            int hours;

            if (!int.TryParse(stringHours, out hours))
            {
                return null;
            }

            if ((hours < 1) || (hours > 12))
            {
                return null;
            }

            string stringMinutes = hoursAndMinutes.Replace(stringHours + ":", string.Empty);

            int minutes;

            if (!int.TryParse(stringMinutes, out minutes))
            {
                return null;
            }

            if ((minutes < 0) || (minutes > 59))
            {
                return null;
            }

            if (abbreviation != null)
            {
                var formattableString = $"0001-01-01 {stringHours.PadLeft(2, '0')}:{stringMinutes.PadLeft(2, '0')} {abbreviation}";

                return DateTime.ParseExact(formattableString, "yyyy-MM-dd hh:mm tt", CultureInfo.InvariantCulture);
            }

            return new DateTime(1, 1, 1, hours, minutes, 0);
        }

        public DateTime ParseFirstAired(string value)
        {
            return DateTime.ParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }
    }
}