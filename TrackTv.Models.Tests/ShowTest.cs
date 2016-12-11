namespace TrackTv.Models.Tests
{
    using System.Collections.Generic;

    using TrackTv.Models.Joint;

    using Xunit;

    public class ShowTest
    {
        [Fact]

        // ReSharper disable once InconsistentNaming
        public void Episodes_Is_Initialized()
        {
            var show = new Show();

            Assert.NotNull(show.Episodes);

            Assert.IsType<List<Episode>>(show.Episodes);
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public void HasActor_Detects_If_Actor_Is_Loaded()
        {
            var actor = new Actor();

            var show = new Show();

            show.ShowsActors.Add(new ShowsActors(actor, null));

            Assert.True(show.HasActor(actor));
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public void HasActor_Does_Not_Detect_If_ActorId_Is_Zero()
        {
            var actor = new Actor();

            var show = new Show();

            show.ShowsActors.Add(new ShowsActors
                {
                    ActorId = 0
                });

            Assert.False(show.HasActor(actor));
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public void HasActors_Detects_If_ActorId_Matches()
        {
            var actor = new Actor
            {
                Id = 42
            };

            var show = new Show();

            show.ShowsActors.Add(new ShowsActors
                {
                    ActorId = actor.Id
                });

            Assert.True(show.HasActor(actor));
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public void HasGenre_Detects_If_Genre_Is_Loaded()
        {
            var genre = new Genre();

            var show = new Show();

            show.ShowsGenres.Add(new ShowsGenres(genre));

            Assert.True(show.HasGenre(genre));
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public void HasGenre_Detects_If_GenreId_Matches()
        {
            var genre = new Genre
            {
                Id = 42
            };

            var show = new Show();

            show.ShowsGenres.Add(new ShowsGenres
                {
                    GenreId = genre.Id
                });

            Assert.True(show.HasGenre(genre));
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public void HasGenre_Does_Not_Detect_If_GenreId_Is_Zero()
        {
            var genre = new Genre();

            var show = new Show();

            show.ShowsGenres.Add(new ShowsGenres
                {
                    GenreId = 0
                });

            Assert.False(show.HasGenre(genre));
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public void HasNetwork_Returns_False_If_NetworkId_Is_Zero()
        {
            var show = new Show
            {
                NetworkId = 0
            };

            Assert.False(show.HasNetwork());
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public void HasNetwork_Returns_True_If_Network_Is_Attached()
        {
            var network = new Network();

            var show = new Show
            {
                Network = network
            };

            Assert.True(show.HasNetwork());
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public void HasNetwork_Returns_True_If_NetworkId_Is_Greater_Than_Zero()
        {
            var show = new Show
            {
                NetworkId = 42
            };

            Assert.True(show.HasNetwork());
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public void ShowsActors_Is_Initialized()
        {
            var show = new Show();

            Assert.NotNull(show.ShowsActors);

            Assert.IsType<List<ShowsActors>>(show.ShowsActors);
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public void ShowsGenres_Is_Initialized()
        {
            var show = new Show();

            Assert.NotNull(show.ShowsGenres);

            Assert.IsType<List<ShowsGenres>>(show.ShowsGenres);
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public void ShowsUsers_Is_Initialized()
        {
            var show = new Show();

            Assert.NotNull(show.ShowsUsers);

            Assert.IsType<List<ShowsUsers>>(show.ShowsUsers);
        }
    }
}