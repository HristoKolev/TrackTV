namespace TrackTV.Web.Helpers
{
    public static class DateHelper
    {
        private static readonly string[] Months = { string.Empty, "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

        public static string GetShortMonth(int month)
        {
            return Months[month];
        }
    }
}