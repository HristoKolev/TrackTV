namespace TrackTv.Tests
{
    using Xunit;

    public class SampleClassTest
    {
        [Fact]

        // ReSharper disable once InconsistentNaming
        public void Get42_Should_Return_42()
        {
            var sample = new SampleClass();

            Assert.Equal(42, sample.Get42());
        }
    }
}