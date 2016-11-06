namespace TrackTv.DataRetrieval.Fetchers
{
    using System;
    using System.Globalization;

    public class DateParser
    {
        private const int AbbreviationLength = 2;

        private const string Am = "am";

        private const string Pm = "pm";

        public DateTime? ParseAirTime(string value)
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
                if ((hour < 1) || (hour > 12))
                {
                    return null;
                }
            }
            else
            {
                if ((hour < 0) || (hour > 23))
                {
                    return null;
                }
            }

            int minute;

            if (!int.TryParse(stringMinutes, out minute))
            {
                return null;
            }

            if ((minute < 0) || (minute > 59))
            {
                return null;
            }

            if ((abbreviation == Am) && (hour == 12))
            {
                return Create(hour - 12, minute);
            }

            if ((abbreviation == Pm) && (hour != 12))
            {
                return Create(hour + 12, minute);
            }

            return Create(hour, minute);
        }

        public DateTime ParseFirstAired(string value)
        {
            return DateTime.ParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        private static DateTime Create(int hour, int minute)
        {
            return new DateTime(1, 1, 1, hour, minute, 0);
        }
    }
}