namespace TrackTv.DataRetrieval.Tests
{
    using System;
    using System.Globalization;

    using TrackTv.Updater;

    using Xunit;

    public class DateParserTest
    {
        [Fact]

        // ReSharper disable once InconsistentNaming
        public void ParseAirTime_should_calculate_12_hour_clock_correctly()
        {
            for (int hour = 0; hour < 23; hour++)
            {
                for (int minute = 0; minute < 59; minute++)
                {
                    var time = new DateTime(1, 1, 1, hour, minute, 0);

                    string value = time.ToString("hh:mm tt", CultureInfo.InvariantCulture);

                    // ReSharper disable once PossibleInvalidOperationException
                    var parsed = DateParser.ParseAirTime(value).Value;

                    Assert.Equal(time.ToString("HH:mm"), parsed.ToString("HH:mm"));
                }
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public void ParseAirTime_should_return_null_if_hour_is_not_an_integer()
        {
            Assert.Null(DateParser.ParseAirTime("A:00"));
        }

        [Theory]
        [InlineData("13:00 pm")]
        [InlineData("00:00 pm")]
        [InlineData("-1:00")]
        [InlineData("24:00")]

        // ReSharper disable once InconsistentNaming
        public void ParseAirTime_should_return_null_if_hour_is_out_of_range(string value)
        {
            Assert.Null(DateParser.ParseAirTime(value));
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public void ParseAirTime_should_return_null_if_minute_is_not_an_integer()
        {
            Assert.Null(DateParser.ParseAirTime("00:A"));
        }

        [Theory]
        [InlineData("00:-1")]
        [InlineData("00:60")]

        // ReSharper disable once InconsistentNaming
        public void ParseAirTime_should_return_null_if_minute_is_out_of_range(string value)
        {
            Assert.Null(DateParser.ParseAirTime(value));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        [InlineData("\t")]
        [InlineData("\r")]
        [InlineData("\n")]

        // ReSharper disable once InconsistentNaming
        public void ParseAirTime_should_throw_if_passed_null_or_white_space(string value)
        {
            Assert.Throws<InvalidOperationException>(() => DateParser.ParseAirTime(value));
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public void ParseAirTime_should_throw_should_return_null_if_colon_is_missing()
        {
            Assert.Null(DateParser.ParseAirTime("9000"));
        }
    }
}