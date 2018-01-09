namespace TrackTv.DataRetrieval
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;

    using LinqToDB;

    using TrackTv.Data;
    using TrackTv.Data.Models.Enums;
    using TrackTv.DataRetrieval.ClientExtensions;
    using TrackTv.DataRetrieval.Fetchers;

    using TvDbSharper;
    using TvDbSharper.Dto;

    public class NewFetcher
    {
        public NewFetcher(DbService dbService, ITvDbClient client, IDbConnection dbConnection)
        {
            this.DbService = dbService;
            this.Client = client;
            this.DbConnection = dbConnection;

            this.DateParser = new DateParser();
        }

        private ITvDbClient Client { get; }

        private DateParser DateParser { get; }

        private IDbConnection DbConnection { get; }

        private DbService DbService { get; }

        public async Task UpdateAllAsync(Func<Exception, Task> errorHandler)
        {
            var context = new UpdateContext
            {
                ExistingShowIds =
                    new HashSet<int>(await this.DbService.Shows.Select(poco => poco.TheTvDbId).ToListAsync().ConfigureAwait(false)),
                ExistingEpisodeIds =
                    new HashSet<int>(await this.DbService.Episodes.Select(poco => poco.TheTvDbId).ToListAsync().ConfigureAwait(false)),
            };

            var updates = await this.GetUpdates(DateTime.UtcNow.Subtract(TimeSpan.FromDays(7))).ConfigureAwait(false);

            foreach (var update in updates)
            {
                using (var transaction = this.DbConnection.BeginTransaction(IsolationLevel.Snapshot))
                {
                    try
                    {
                        bool updateOccurred = await this.ProcessUpdateAsync(update.Id, context).ConfigureAwait(false);

                        if (updateOccurred)
                        {
                            transaction.Commit();
                        }
                        else
                        {
                            transaction.Rollback();
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        await errorHandler(ex).ConfigureAwait(false);
                    }
                }
            }
        }

        private async Task<EpisodeRecord> GetExternalEpisodeAsync(int updateId)
        {
            var response = await this.Client.Episodes.GetAsync(updateId).ConfigureAwait(false);

            return response.Data;
        }

        private async Task<Series> GetExternalShowAsync(int updateId)
        {
            var response = await this.Client.Series.GetAsync(updateId).ConfigureAwait(false);

            return response.Data;
        }

        private async Task<int> GetOrCreateGenre(string genreName)
        {
            var genre = await this.DbService.Genres
                                  .FirstOrDefaultAsync(poco => poco.GenreName.Trim().ToLower() == genreName.Trim().ToLower())
                                  .ConfigureAwait(false);

            if (genre == null)
            {
                genre = new GenrePoco
                {
                    GenreName = genreName
                };

                await this.DbService.InsertAsync(genre).ConfigureAwait(false);
            }

            return genre.GenreId;
        }

        private async Task<int> GetOrCreateNetwork(string networkName)
        {
            var network = await this.DbService.Networks
                                    .FirstOrDefaultAsync(poco => poco.NetworkName.Trim().ToLower() == networkName.Trim().ToLower())
                                    .ConfigureAwait(false);

            if (network == null)
            {
                network = new NetworkPoco
                {
                    NetworkName = networkName
                };

                await this.DbService.InsertAsync(network).ConfigureAwait(false);
            }

            return network.NetworkId;
        }

        private async Task<Update[]> GetUpdates(DateTime time)
        {
            var response = await this.Client.Updates.GetAsync(time).ConfigureAwait(false);

            return response.Data;
        }

        private void MapToEpisode(EpisodePoco episode, EpisodeRecord data)
        {
            episode.EpisodeTitle = data.EpisodeName;
            episode.EpisodeDescription = data.Overview;
            episode.ImdbId = data.ImdbId;
            episode.EpisodeNumber = data.AiredEpisodeNumber.Value;
            episode.SeasonNumber = data.AiredSeason.Value;
            episode.TheTvDbId = data.Id;

            if (!string.IsNullOrWhiteSpace(data.FirstAired))
            {
                episode.FirstAired = this.DateParser.ParseFirstAired(data.FirstAired);
            }

            episode.LastUpdated = data.LastUpdated.ToDateTime();
        }

        private void MapToShow(ShowPoco show, Series data)
        {
            show.TheTvDbId = data.Id;
            show.ShowName = data.SeriesName;
            show.ShowBanner = data.Banner;
            show.ImdbId = data.ImdbId;
            show.ShowDescription = data.Overview;

            show.LastUpdated = data.LastUpdated.ToDateTime();

            AirDay airDay;
            Enum.TryParse(data.AirsDayOfWeek, out airDay);
            show.AirDay = (int?)airDay;

            ShowStatus status;
            Enum.TryParse(data.Status, out status);
            show.ShowStatus = (int)status;

            if (!string.IsNullOrWhiteSpace(data.FirstAired))
            {
                show.FirstAired = this.DateParser.ParseFirstAired(data.FirstAired);
            }

            if (!string.IsNullOrWhiteSpace(data.AirsTime))
            {
                show.AirTime = this.DateParser.ParseAirTime(data.AirsTime);
            }
        }

        private async Task<bool> ProcessUpdateAsync(int updateId, UpdateContext context)
        {
            if (context.ExistingEpisodeIds.Contains(updateId))
            {
                await this.UpdateEpisodeAsync(updateId).ConfigureAwait(false);
                return true;
            }

            if (context.ExistingShowIds.Contains(updateId))
            {
                await this.UpdateShow(updateId, context).ConfigureAwait(false);
                return true;
            }

            var externalShow = await this.GetExternalShowAsync(updateId).ConfigureAwait(false);
            if (externalShow != null)
            {
                await this.UpdateShow(updateId, context).ConfigureAwait(false);
                return true;
            }

            var externalEpisode = await this.GetExternalEpisodeAsync(updateId).ConfigureAwait(false);
            if (externalEpisode != null && int.TryParse(externalEpisode.SeriesId, out var seriesId))
            {
                await this.UpdateShow(seriesId, context).ConfigureAwait(false);
                return true;
            }

            return false;
        }

        private async Task UpdateActors(int theTvDbId, int showId)
        {
            var response = await this.Client.Series.GetActorsAsync(theTvDbId).ConfigureAwait(false);

            var actors = response.Data;

            var myActors = await this.DbService.Actors.Where(poco => actors.Select(actor => actor.Id).Contains(poco.TheTvDbId))
                                     .ToListAsync()
                                     .ConfigureAwait(false);

            foreach (var actor in actors)
            {
                var myActor = myActors.FirstOrDefault(poco => poco.TheTvDbId == actor.Id) ?? new ActorPoco();

                myActor.ActorImage = actor.Image;
                myActor.TheTvDbId = actor.Id;
                myActor.ActorName = actor.Name;
                myActor.LastUpdated = DateTime.Parse(actor.LastUpdated);

                await this.DbService.SaveAsync(myActor).ConfigureAwait(false);

                var role = await this.DbService.Roles.FirstOrDefaultAsync(poco => poco.ShowId == showId && poco.ActorId == myActor.ActorId)
                                     .ConfigureAwait(false) ?? new RolePoco();

                role.ShowId = showId;
                role.ActorId = myActor.ActorId;
                role.RoleName = actor.Role;

                await this.DbService.SaveAsync(role).ConfigureAwait(false);
            }
        }

        private async Task UpdateEpisodeAsync(int updateId)
        {
            var myEpisode = await this.DbService.Episodes.FirstAsync(poco => poco.TheTvDbId == updateId).ConfigureAwait(false);

            var externalEpisode = await this.GetExternalEpisodeAsync(updateId).ConfigureAwait(false);

            this.MapToEpisode(myEpisode, externalEpisode);

            await this.DbService.UpdateAsync(myEpisode).ConfigureAwait(false);
        }

        private async Task UpdateEpisodes(int theTvDbId, UpdateContext context, int showId)
        {
            var basicEpisodes = await this.Client.Series.GetBasicEpisodesAsync(theTvDbId).ConfigureAwait(false);

            // Delete episodes
            var deletedEpisodeIds = context.ExistingEpisodeIds.Except(basicEpisodes.Select(e => e.Id)).ToArray();
            var deletedEpisodes = await this.DbService.Episodes.Where(poco => deletedEpisodeIds.Contains(poco.TheTvDbId))
                                            .ToListAsync()
                                            .ConfigureAwait(false);

            foreach (var episode in deletedEpisodes)
            {
                await this.DbService.DeleteAsync(episode).ConfigureAwait(false);
            }

            // Insert episodes
            var addedEpisodeIds = basicEpisodes.Select(e => e.Id).Except(context.ExistingEpisodeIds).ToArray();
            var addedEpisodes = await this.Client.Episodes.GetFullEpisodesAsync(addedEpisodeIds).ConfigureAwait(false);

            foreach (var episode in addedEpisodes)
            {
                var myEpisode = new EpisodePoco
                {
                    ShowId = showId,
                };

                this.MapToEpisode(myEpisode, episode);

                await this.DbService.InsertAsync(myEpisode).ConfigureAwait(false);
            }

            // Update episodes
            var existingEpisodeIds = basicEpisodes.Select(episode => episode.Id).Intersect(context.ExistingEpisodeIds).ToArray();
            var myExistingEpisodes = await this.DbService.Episodes.Where(poco => existingEpisodeIds.Contains(poco.TheTvDbId))
                                               .ToListAsync()
                                               .ConfigureAwait(false);

            var updatedEpisodes = myExistingEpisodes
                                  .Where(poco =>
                                      basicEpisodes.First(e => e.Id == poco.TheTvDbId).LastUpdated > poco.LastUpdated.ToUnixEpochTime())
                                  .ToList();

            var externalUpdatedEpisodes = await this.Client.Episodes.GetFullEpisodesAsync(updatedEpisodes.Select(poco => poco.TheTvDbId))
                                                    .ConfigureAwait(false);

            foreach (var myEpisode in updatedEpisodes)
            {
                var episode = externalUpdatedEpisodes.First(record => record.Id == myEpisode.TheTvDbId);

                this.MapToEpisode(myEpisode, episode);

                await this.DbService.UpdateAsync(myEpisode).ConfigureAwait(false);
            }
        }

        private async Task UpdateGenres(IEnumerable<string> genreNames, int showId)
        {
            var genreIds = new List<int>();

            foreach (string genreName in genreNames)
            {
                int genreId = await this.GetOrCreateGenre(genreName).ConfigureAwait(false);

                genreIds.Add(genreId);
            }

            var existingGenreIds = await this.DbService.Showsgenres.Where(poco => poco.ShowId == showId)
                                             .Select(poco => poco.GenreId)
                                             .ToListAsync()
                                             .ConfigureAwait(false);

            foreach (int genreId in genreIds.Except(existingGenreIds))
            {
                await this.DbService.InsertAsync(new ShowsgenrePoco
                          {
                              GenreId = genreId,
                              ShowId = showId
                          })
                          .ConfigureAwait(false);
            }
        }

        private async Task UpdateShow(int updateId, UpdateContext context)
        {
            var myShow = await this.DbService.Shows.FirstOrDefaultAsync(poco => poco.TheTvDbId == updateId).ConfigureAwait(false)
                         ?? new ShowPoco
                         {
                             TheTvDbId = updateId
                         };

            var externalShow = await this.GetExternalShowAsync(myShow.TheTvDbId).ConfigureAwait(false);

            this.MapToShow(myShow, externalShow);
            myShow.NetworkId = await this.GetOrCreateNetwork(externalShow.Network).ConfigureAwait(false);

            await this.DbService.SaveAsync(myShow).ConfigureAwait(false);

            await this.UpdateGenres(externalShow.Genre, myShow.ShowId).ConfigureAwait(false);
            await this.UpdateActors(myShow.TheTvDbId, myShow.ShowId).ConfigureAwait(false);
            await this.UpdateEpisodes(myShow.TheTvDbId, context, myShow.ShowId).ConfigureAwait(false);
        }

        private class UpdateContext
        {
            public HashSet<int> ExistingEpisodeIds { get; set; } = new HashSet<int>();

            public HashSet<int> ExistingShowIds { get; set; } = new HashSet<int>();
        }
    }
}