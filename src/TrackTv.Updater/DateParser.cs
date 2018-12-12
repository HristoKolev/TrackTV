namespace TrackTv.Updater
{
    using System;
    using System.Globalization;

    public static class DateParser
    {
        private const int AbbreviationLength = 2;

        private const string Am = "am";

        private const string Pm = "pm";

        public static DateTime? ParseAirTime(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidOperationException("The value is null or white space.");
            }

            string time = value.Trim().ToLower();

            string abbreviation = null;

            if (time.EndsWith(Am) || time.EndsWith(Pm))
            {
                abbreviation = time.Substring(time.Length - AbbreviationLength, AbbreviationLength);
                time = time.Remove(time.Length - AbbreviationLength, AbbreviationLength).Trim();
            }

            if (!time.Contains(":"))
            {
                return null;
            }

            string[] hoursAndMinutes = time.Split(':');

            string stringHours = hoursAndMinutes[0];
            string stringMinutes = hoursAndMinutes[1];

            int hour;

            if (!int.TryParse(stringHours, out hour))
            {
                return null;
            }

            if (abbreviation != null)
            {
                if (hour < 1 || hour > 12)
                {
                    return null;
                }
            }
            else
            {
                if (hour < 0 || hour > 23)
                {
                    return null;
                }
            }

            int minute;

            if (!int.TryParse(stringMinutes, out minute))
            {
                return null;
            }

            if (minute < 0 || minute > 59)
            {
                return null;
            }

            if (abbreviation == Am && hour == 12)
            {
                return Create(hour - 12, minute);
            }

            if (abbreviation == Pm && hour != 12)
            {
                return Create(hour + 12, minute);
            }

            return Create(hour, minute);
        }

        public static DateTime? ParseFirstAired(string value)
        {
            try
            {
                return DateTime.ParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                return null;
            }
        }

        public static DateTime? ParseActorLastUpdated(string date)
        {
            if (string.IsNullOrWhiteSpace(date))
            {
                return null;
            }

            if (date == "0000-00-00 00:00:00")
            {
                return null;
            }

            DateTime value;
            if (!DateTime.TryParse(date, out value))
            {
                return null;
            }

            if (value == DateTime.MinValue)
            {
                return null;
            }

            return value;
        }

        private static DateTime Create(int hour, int minute)
        {
            return new DateTime(1, 1, 1, hour, minute, 0);
        }
    }
}