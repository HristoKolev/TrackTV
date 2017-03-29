namespace TrackTv.DataRetrieval.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using NSubstitute;

    using TrackTv.Data.Models;
    using TrackTv.DataRetrieval.ClientExtensions;
    using TrackTv.DataRetrieval.Fetchers;

    using TvDbSharper.BaseSchemas;
    using TvDbSharper.Clients.Episodes;
    using TvDbSharper.Clients.Episodes.Json;
    using TvDbSharper.Clients.Series.Json;

    using Xunit;

    public class EpisodeFetcherTest
    {
        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task AddAllEpisodesAsync_should_add_all_episodes_to_show()
        {
            var episodesClient = Substitute.For<IEpisodesClient>();
            var advancedEpisodesClient = Substitute.For<IAdvancedEpisodeClient>();
            var advancedSeriesClient = Substitute.For<IAdvancedSeriesClient>();

            var fetcher = new EpisodeFetcher(episodesClient, advancedEpisodesClient, advancedSeriesClient);

            var show = new Show
            {
                TheTvDbId = 42
            };

            var basics = new[]
            {
                new BasicEpisode
                {
                    Id = 1
                },
                new BasicEpisode
                {
                    Id = 2
                },
                new BasicEpisode
                {
                    Id = 3
                }
            };

            var episodeRecords = basics.Select(x => new EpisodeRecord
            {
                Id = x.Id,
                AiredEpisodeNumber = 0,
                AiredSeason = 0
            });

            Expression<Predicate<IEnumerable<int>>> isIdsOfBasics = x => x.SequenceEqual(basics.Select(e => e.Id));

            advancedSeriesClient.GetBasicEpisodesAsync(show.TheTvDbId).Returns(basics);

            advancedEpisodesClient.GetFullEpisodesAsync(Arg.Is(isIdsOfBasics)).Returns(episodeRecords);

            await fetcher.AddAllEpisodesAsync(show);

            Assert.Equal(basics.Length, show.Episodes.Count);

            Assert.True(show.Episodes.Select(x => x.TheTvDbId).SequenceEqual(basics.Select(e => e.Id)));
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task AddAllEpisodesAsync_should_get_basic_episodesd_from_the_client()
        {
            var episodesClient = Substitute.For<IEpisodesClient>();
            var advancedEpisodesClient = Substitute.For<IAdvancedEpisodeClient>();
            var advancedSeriesClient = Substitute.For<IAdvancedSeriesClient>();

            var fetcher = new EpisodeFetcher(episodesClient, advancedEpisodesClient, advancedSeriesClient);

            var show = new Show
            {
                TheTvDbId = 42
            };

            advancedSeriesClient.GetBasicEpisodesAsync(show.TheTvDbId).Returns(Array.Empty<BasicEpisode>());
            advancedEpisodesClient.GetFullEpisodesAsync(Arg.Is<IEnumerable<int>>(x => !x.Any())).Returns(Array.Empty<EpisodeRecord>());

            await fetcher.AddAllEpisodesAsync(show);

            await advancedSeriesClient.Received().GetBasicEpisodesAsync(show.TheTvDbId);
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task AddAllEpisodesAsync_should_get_full_episodesd_from_the_client()
        {
            var episodesClient = Substitute.For<IEpisodesClient>();
            var advancedEpisodesClient = Substitute.For<IAdvancedEpisodeClient>();
            var advancedSeriesClient = Substitute.For<IAdvancedSeriesClient>();

            var fetcher = new EpisodeFetcher(episodesClient, advancedEpisodesClient, advancedSeriesClient);

            var show = new Show
            {
                TheTvDbId = 42
            };

            var basics = new[]
            {
                new BasicEpisode
                {
                    Id = 1
                },
                new BasicEpisode
                {
                    Id = 2
                },
                new BasicEpisode
                {
                    Id = 3
                }
            };

            advancedSeriesClient.GetBasicEpisodesAsync(show.TheTvDbId).Returns(basics);

            Expression<Predicate<IEnumerable<int>>> isIdsOfBasics = x => x.SequenceEqual(basics.Select(e => e.Id));

            advancedEpisodesClient.GetFullEpisodesAsync(Arg.Is(isIdsOfBasics)).Returns(Array.Empty<EpisodeRecord>());

            await fetcher.AddAllEpisodesAsync(show);

            await advancedEpisodesClient.Received().GetFullEpisodesAsync(Arg.Is(isIdsOfBasics));
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task AddAllEpisodesAsync_should_map_episodes()
        {
            var episodesClient = Substitute.For<IEpisodesClient>();
            var advancedEpisodesClient = Substitute.For<IAdvancedEpisodeClient>();
            var advancedSeriesClient = Substitute.For<IAdvancedSeriesClient>();

            var fetcher = new EpisodeFetcher(episodesClient, advancedEpisodesClient, advancedSeriesClient);

            var show = new Show
            {
                TheTvDbId = 42
            };

            var basics = new[]
            {
                new BasicEpisode
                {
                    Id = 1
                }
            };

            var episodeRecords = new[]
            {
                new EpisodeRecord
                {
                    Id = 1,
                    EpisodeName = "Rose",
                    Overview = "Rose Tyler stumbles across a man called The Doctor...",
                    ImdbId = "tt0562992",
                    AiredEpisodeNumber = 1,
                    AiredSeason = 1,
                    FirstAired = "2005-03-26",
                    LastUpdated = 1477941112
                }
            };

            Expression<Predicate<IEnumerable<int>>> isIdsOfBasics = x => x.SequenceEqual(basics.Select(e => e.Id));

            advancedSeriesClient.GetBasicEpisodesAsync(show.TheTvDbId).Returns(basics);

            advancedEpisodesClient.GetFullEpisodesAsync(Arg.Is(isIdsOfBasics)).Returns(episodeRecords);

            await fetcher.AddAllEpisodesAsync(show);

            var record = episodeRecords.First();
            var episode = show.Episodes.First();

            Assert.Equal(record.EpisodeName, episode.Title);
            Assert.Equal(record.Overview, episode.Description);
            Assert.Equal(record.ImdbId, episode.ImdbId);
            Assert.Equal(record.AiredEpisodeNumber, episode.Number);
            Assert.Equal(record.AiredSeason, episode.SeasonNumber);

            Assert.Equal(new DateTime(2005, 3, 26), episode.FirstAired);

            Assert.Equal(new DateTime(2016, 10, 31, 19, 11, 52), episode.LastUpdated);
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task AddNewEpisodesAsync_should_add_new_episodes_to_show()
        {
            var episodesClient = Substitute.For<IEpisodesClient>();
            var advancedEpisodesClient = Substitute.For<IAdvancedEpisodeClient>();
            var advancedSeriesClient = Substitute.For<IAdvancedSeriesClient>();

            var fetcher = new EpisodeFetcher(episodesClient, advancedEpisodesClient, advancedSeriesClient);

            var show = new Show
            {
                TheTvDbId = 42,
                Episodes =
                {
                    new Episode
                    {
                        TheTvDbId = 1
                    }
                }
            };

            var basics = new[]
            {
                new BasicEpisode
                {
                    Id = show.Episodes.First().TheTvDbId
                },
                new BasicEpisode
                {
                    Id = 2
                },
                new BasicEpisode
                {
                    Id = 3
                }
            };

            var episodeRecords = basics.Skip(1).Select(x => new EpisodeRecord
            {
                Id = x.Id,
                AiredEpisodeNumber = 0,
                AiredSeason = 0
            });

            Expression<Predicate<IEnumerable<int>>> isIdsOfBasicsExceptFirst = x => x.SequenceEqual(basics.Skip(1).Select(e => e.Id));

            advancedSeriesClient.GetBasicEpisodesAsync(show.TheTvDbId).Returns(basics);

            advancedEpisodesClient.GetFullEpisodesAsync(Arg.Is(isIdsOfBasicsExceptFirst)).Returns(episodeRecords);

            await fetcher.AddNewEpisodesAsync(show);

            Assert.Equal(basics.Length, show.Episodes.Count);

            int[] ids =
            {
                1,
                2,
                3
            };

            Assert.True(show.Episodes.Select(x => x.TheTvDbId).SequenceEqual(ids));
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task AddNewEpisodesAsync_should_get_basic_episodesd_from_the_client()
        {
            var episodesClient = Substitute.For<IEpisodesClient>();
            var advancedEpisodesClient = Substitute.For<IAdvancedEpisodeClient>();
            var advancedSeriesClient = Substitute.For<IAdvancedSeriesClient>();

            var fetcher = new EpisodeFetcher(episodesClient, advancedEpisodesClient, advancedSeriesClient);

            var show = new Show
            {
                TheTvDbId = 42
            };

            advancedSeriesClient.GetBasicEpisodesAsync(show.TheTvDbId).Returns(Array.Empty<BasicEpisode>());
            advancedEpisodesClient.GetFullEpisodesAsync(Arg.Is<IEnumerable<int>>(x => !x.Any())).Returns(Array.Empty<EpisodeRecord>());

            await fetcher.AddNewEpisodesAsync(show);

            await advancedSeriesClient.Received().GetBasicEpisodesAsync(show.TheTvDbId);
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task AddNewEpisodesAsync_should_get_full_episodesd_from_the_client()
        {
            var episodesClient = Substitute.For<IEpisodesClient>();
            var advancedEpisodesClient = Substitute.For<IAdvancedEpisodeClient>();
            var advancedSeriesClient = Substitute.For<IAdvancedSeriesClient>();

            var fetcher = new EpisodeFetcher(episodesClient, advancedEpisodesClient, advancedSeriesClient);

            var show = new Show
            {
                TheTvDbId = 42,
                Episodes =
                {
                    new Episode
                    {
                        TheTvDbId = 1
                    }
                }
            };

            var basics = new[]
            {
                new BasicEpisode
                {
                    Id = show.Episodes.First().TheTvDbId
                },
                new BasicEpisode
                {
                    Id = 2
                },
                new BasicEpisode
                {
                    Id = 3
                }
            };

            advancedSeriesClient.GetBasicEpisodesAsync(show.TheTvDbId).Returns(basics);

            Expression<Predicate<IEnumerable<int>>> isIdsOfBasicsExceptFirst = x => x.SequenceEqual(basics.Skip(1).Select(e => e.Id));

            advancedEpisodesClient.GetFullEpisodesAsync(Arg.Is(isIdsOfBasicsExceptFirst)).Returns(Array.Empty<EpisodeRecord>());

            await fetcher.AddNewEpisodesAsync(show);

            await advancedEpisodesClient.Received().GetFullEpisodesAsync(Arg.Is(isIdsOfBasicsExceptFirst));
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task AddNewEpisodesAsync_should_map_episodes()
        {
            var episodesClient = Substitute.For<IEpisodesClient>();
            var advancedEpisodesClient = Substitute.For<IAdvancedEpisodeClient>();
            var advancedSeriesClient = Substitute.For<IAdvancedSeriesClient>();

            var fetcher = new EpisodeFetcher(episodesClient, advancedEpisodesClient, advancedSeriesClient);

            var show = new Show
            {
                TheTvDbId = 42,
                Episodes =
                {
                    new Episode
                    {
                        TheTvDbId = 1
                    }
                }
            };

            var basics = new[]
            {
                new BasicEpisode
                {
                    Id = 1
                },
                new BasicEpisode
                {
                    Id = 2
                }
            };

            var episodeRecords = new[]
            {
                new EpisodeRecord
                {
                    Id = 2,
                    EpisodeName = "Rose",
                    Overview = "Rose Tyler stumbles across a man called The Doctor...",
                    ImdbId = "tt0562992",
                    AiredEpisodeNumber = 1,
                    AiredSeason = 1,
                    FirstAired = "2005-03-26",
                    LastUpdated = 1477941112
                }
            };

            Expression<Predicate<IEnumerable<int>>> isIdsOfBasicsExceptFirst = x => x.SequenceEqual(basics.Skip(1).Select(e => e.Id));

            advancedSeriesClient.GetBasicEpisodesAsync(show.TheTvDbId).Returns(basics);

            advancedEpisodesClient.GetFullEpisodesAsync(Arg.Is(isIdsOfBasicsExceptFirst)).Returns(episodeRecords);

            await fetcher.AddNewEpisodesAsync(show);

            var record = episodeRecords.First();
            var episode = show.Episodes.Skip(1).First();

            Assert.Equal(record.EpisodeName, episode.Title);
            Assert.Equal(record.Overview, episode.Description);
            Assert.Equal(record.ImdbId, episode.ImdbId);
            Assert.Equal(record.AiredEpisodeNumber, episode.Number);
            Assert.Equal(record.AiredSeason, episode.SeasonNumber);

            Assert.Equal(new DateTime(2005, 3, 26), episode.FirstAired);

            Assert.Equal(new DateTime(2016, 10, 31, 19, 11, 52), episode.LastUpdated);
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task PopulateEpisodeAsync_should_call_the_client()
        {
            var episodesClient = Substitute.For<IEpisodesClient>();

            var fetcher = new EpisodeFetcher(episodesClient, Substitute.For<IAdvancedEpisodeClient>(),
                Substitute.For<IAdvancedSeriesClient>());

            var episode = new Episode
            {
                TheTvDbId = 42
            };

            var response = new TvDbResponse<EpisodeRecord>
            {
                Data = new EpisodeRecord
                {
                    Id = 42,
                    AiredSeason = 1,
                    AiredEpisodeNumber = 1
                }
            };

            episodesClient.GetAsync(episode.TheTvDbId).Returns(response);

            await fetcher.PopulateEpisodeAsync(episode);

            await episodesClient.Received().GetAsync(episode.TheTvDbId);
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task PopulateEpisodeAsync_should_map_the_episode()
        {
            var episodesClient = Substitute.For<IEpisodesClient>();

            var fetcher = new EpisodeFetcher(episodesClient, Substitute.For<IAdvancedEpisodeClient>(),
                Substitute.For<IAdvancedSeriesClient>());

            var episode = new Episode
            {
                TheTvDbId = 42
            };

            var response = new TvDbResponse<EpisodeRecord>
            {
                Data = new EpisodeRecord
                {
                    Id = 42,
                    EpisodeName = "Rose",
                    Overview = "Rose Tyler stumbles across a man called The Doctor...",
                    ImdbId = "tt0562992",
                    AiredEpisodeNumber = 1,
                    AiredSeason = 1,
                    FirstAired = "2005-03-26",
                    LastUpdated = 1477941112
                }
            };

            episodesClient.GetAsync(episode.TheTvDbId).Returns(response);

            await fetcher.PopulateEpisodeAsync(episode);

            var record = response.Data;

            Assert.Equal(record.EpisodeName, episode.Title);
            Assert.Equal(record.Overview, episode.Description);
            Assert.Equal(record.ImdbId, episode.ImdbId);
            Assert.Equal(record.AiredEpisodeNumber, episode.Number);
            Assert.Equal(record.AiredSeason, episode.SeasonNumber);

            Assert.Equal(new DateTime(2005, 3, 26), episode.FirstAired);

            Assert.Equal(new DateTime(2016, 10, 31, 19, 11, 52), episode.LastUpdated);
        }
    }
}