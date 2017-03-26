namespace TrackTv.Services.Data.Tests
{
    using System;
    using System.Linq;

    using Xunit;

    public class EnumerableExtensionsTest
    {
        private const int MaxPageSize = 50;

        [Fact]

        // ReSharper disable once InconsistentNaming
        public void Page_calculates_how_much_to_skip_and_how_much_to_take()
        {
            AssertPage(nums => nums.Page(3, 10), 20, 10);
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public void Page_does_not_allow_page_size_to_be_less_than_1()
        {
            AssertPage(nums => nums.Page(1, 0), 0, 1);
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public void Page_does_not_allow_page_size_to_be_more_than_the_maximum_allowed()
        {
            AssertPage(nums => nums.Page(1, MaxPageSize + 1), 0, MaxPageSize);
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public void Page_does_not_allow_page_to_be_less_than_1()
        {
            AssertPage(nums => nums.Page(0, 10), 0, 10);
        }

        private static void AssertPage(Func<IQueryable<int>, IQueryable<int>> func, int skip, int take)
        {
            string expected = $"System.Linq.Enumerable+RangeIterator.Skip({skip}).Take({take})";

            var start = Enumerable.Range(0, 100).AsQueryable();

            Assert.Equal(expected, func(start).ToString());
        }
    }
}