namespace TrackTv.Models.Tests
{
    using System;
    using System.Collections.Generic;

    using TrackTv.Data.Models;

    using Xunit;

    public class ActorTest
    {
        [Fact]

        // ReSharper disable once InconsistentNaming
        public void Constructor_Accepts_The_Rigth_Parameters()
        {
            int theTvDbId = 42;
            string name = "Just a name";
            DateTime lastUpdated = new DateTime(2000, 1, 1);
            string image = "just an image";

            var actor = new Actor(theTvDbId, name, lastUpdated, image);

            Assert.Equal(theTvDbId, actor.TheTvDbId);
            Assert.Equal(name, actor.Name);
            Assert.Equal(lastUpdated, actor.LastUpdated);
            Assert.Equal(image, actor.Image);
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public void ShowsActors_Is_Initialized()
        {
            Actor actor = new Actor();

            Assert.NotNull(actor.ShowsActors);
            Assert.IsType<List<ShowsActors>>(actor.ShowsActors);
        }
    }
}