namespace TrackTv.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TrackTv.Data;
    using TrackTv.Models;
    using TrackTv.Models.Joint;

    using Xunit;

    public class EpisodeRepositoryTest : BaseRepositoryTest
    {
        private const int ReferenceProfileId = 10;

        private const int WeekLength = 7;

        private DateTime ReferenceDate { get; } = new DateTime(2000, 1, 1);

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task GetEpisodesSummariesAsync_returns_the_correct_last_and_next_episodes()
        {
            using (var context = CreateContext())
            {
                await this.SeedEpisodesAsync(context).ConfigureAwait(false);

                var repository = new EpisodeRepository(context);

                int[] ids =
                {
                    1,
                    2,
                    3
                };

                for (int i = 0; i <= 60; i++)
                {
                    var summaries = await repository.GetEpisodesSummariesAsync(ids, this.ReferenceDate.AddDays(i)).ConfigureAwait(false);

                    foreach (var summary in summaries)
                    {
                        if (i < WeekLength)
                        {
                            Assert.Equal(null, summary.LastEpisode);
                        }
                        else
                        {
                            Assert.Equal(this.ReferenceDate.AddDays((i / WeekLength) * WeekLength), summary.LastEpisode.FirstAired);
                        }

                        Assert.Equal(this.ReferenceDate.AddDays(((i / WeekLength) + 1) * WeekLength), summary.NextEpisode.FirstAired);
                    }
                }
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task GetMonthlyEpisodesAsync_includes_the_show()
        {
            using (var context = CreateContext())
            {
                await this.SeedEpisodesAsync(context).ConfigureAwait(false);

                var repository = new EpisodeRepository(context);

                var episodes =
                    await repository.GetMonthlyEpisodesAsync(ReferenceProfileId, this.ReferenceDate, this.ReferenceDate.AddDays(30))
                                    .ConfigureAwait(false);

                foreach (var episode in episodes)
                {
                    Assert.NotNull(episode.Show);
                }
            }
        }

        private async Task SeedEpisodesAsync(TrackTvDbContext context, IEnumerable<Show> shows = null)
        {
            if (shows == null)
            {
                var list = new List<Show>();

                for (int i = 1; i <= 10; i++)
                {
                    var show = new Show
                    {
                        Id = i
                    };

                    show.ShowsUsers.Add(new ShowsProfiles
                    {
                        ProfileId = ReferenceProfileId
                    });

                    for (int j = 1; j < 10; j++)
                    {
                        var episode = new Episode
                        {
                            Id = (i * 100) + j,
                            FirstAired = this.ReferenceDate.AddDays(j * WeekLength)
                        };

                        show.Episodes.Add(episode);
                    }

                    list.Add(show);
                }

                shows = list;
            }

            context.Shows.AddRange(shows);
            await context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}