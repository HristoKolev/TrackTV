namespace TrackTv.Updater
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using log4net;

    using LinqToDB;

    using TrackTv.Data;
    using TrackTv.DataRetrieval;
    using TrackTv.Services.Data;

    using TvDbSharper;
    using TvDbSharper.Dto;

    public class DataSynchronizer
    {
        public DataSynchronizer(IDbService dbService, ITvDbClient client, ILog log, FailedUpdateRepository failedUpdateRepository)
        {
            this.DbService = dbService;
            this.Client = client;
            this.Log = log;
            this.FailedUpdateRepository = failedUpdateRepository;

            this.DateParser = new DateParser();
        }

        private ITvDbClient Client { get; }

        private DateParser DateParser { get; }

        private IDbService DbService { get; }

        private FailedUpdateRepository FailedUpdateRepository { get; }

        private ILog Log { get; }

        public async Task<DateTime> UpdateAllAsync(
            DateTime fromUtcDate,
            Func<Exception, Task> errorHandler,
            Func<DateTime, Task> onSuccessfulUpdate)
        {
            var updates = await this.GetUpdates(fromUtcDate).ConfigureAwait(false);

            var failedUpdates = await this.FailedUpdateRepository.GetFailedUpdates().ConfigureAwait(false);

            updates = updates.Concat(failedUpdates.Select(poco => new Update
                             {
                                 Id = poco.TheTvDbUpdateId,
                                 LastUpdated = poco.TheTvDbLastUpdated.ToUnixEpochTime()
                             }))
                             .ToArray();

            this.Log.Debug($"{updates.Length} updates available.");

            this.Log.Debug($"{failedUpdates.Count} failed from last time.");

            if (!updates.Any())
            {
                return fromUtcDate;
            }

            int i = 0;

            foreach (var update in updates)
            {
                this.Log.Debug($"Performing update {i + 1} of {updates.Length}, updateId = {update.Id}");
                i++;

                await this.DbService.ExecuteInTransaction(async transaction =>
                          {
                              try
                              {
                                  var context = new UpdateContext
                                  {
                                      ExistingShowIds =
                                          new HashSet<int>(await this.DbService.Shows.Select(poco => poco.TheTvDbId)
                                                                     .ToListAsync()
                                                                     .ConfigureAwait(false)),
                                      ExistingEpisodeIds =
                                          new HashSet<int>(await this.DbService.Episodes.Select(poco => poco.TheTvDbId)
                                                                     .ToListAsync()
                                                                     .ConfigureAwait(false)),
                                  };

                                  await this.ProcessUpdateAsync(update.Id, context).ConfigureAwait(false);

                                  await onSuccessfulUpdate(update.LastUpdated.ToDateTime()).ConfigureAwait(false);

                                  var failedUpdate = failedUpdates.FirstOrDefault(poco => poco.TheTvDbUpdateId == update.Id);

                                  if (failedUpdate != null)
                                  {
                                      await this.FailedUpdateRepository.RemoveFailedUpdate(failedUpdate).ConfigureAwait(false);
                                  }
                              }
                              catch (Exception ex)
                              {
                                  transaction.Rollback();

                                  await this.FailedUpdateRepository.AddFailedUpdate(new FailedUpdatePoco
                                            {
                                                TheTvDbUpdateId = update.Id,
                                                TheTvDbLastUpdated = update.LastUpdated.ToDateTime(),
                                                FailedTime = DateTime.UtcNow
                                            })
                                            .ConfigureAwait(false);

                                  await errorHandler(new DataSyncException($"DataSynchronizer error. UpdateId: {update.Id}.", ex))
                                      .ConfigureAwait(false);
                              }
                          })
                          .ConfigureAwait(false);

                GC.Collect();
            }

            return updates.Select(update => update.LastUpdated).Max().ToDateTime();
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

                return await this.DbService.InsertAsync(genre).ConfigureAwait(false);
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

                return await this.DbService.InsertAsync(network).ConfigureAwait(false);
            }

            return network.NetworkId;
        }

        private async Task<Update[]> GetUpdates(DateTime time)
        {
            var response = await this.Client.Updates.GetAccumulatedAsync(time, DateTime.UtcNow).ConfigureAwait(false);

            if (response.Data == null)
            {
                return Array.Empty<Update>();
            }

            return response.Data.OrderBy(update => update.LastUpdated).ToArray();
        }

        private void MapToEpisode(EpisodePoco episode, EpisodeRecord data)
        {
            episode.EpisodeTitle = data.EpisodeName;
            episode.EpisodeDescription = data.Overview;

            if (!string.IsNullOrWhiteSpace(data.ImdbId))
            {
                episode.ImdbId = data.ImdbId;
            }

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

            if (!string.IsNullOrWhiteSpace(data.Banner))
            {
                show.ShowBanner = data.Banner;
            }

            if (!string.IsNullOrWhiteSpace(data.ImdbId))
            {
                show.ImdbId = data.ImdbId;
            }

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
                var series = await this.GetExternalShowAsync(updateId).ConfigureAwait(false);

                if (string.IsNullOrWhiteSpace(series.SeriesName))
                {
                    return false;
                }

                await this.UpdateShow(updateId, context).ConfigureAwait(false);
                return true;
            }

            var externalShow = await this.GetExternalShowAsync(updateId).ConfigureAwait(false);
            if (externalShow != null)
            {
                var series = await this.GetExternalShowAsync(updateId).ConfigureAwait(false);

                if (string.IsNullOrWhiteSpace(series.SeriesName) || series.SeriesName.StartsWith("***Duplicate"))
                {
                    return false;
                }

                await this.UpdateShow(updateId, context).ConfigureAwait(false);
                return true;
            }

            var externalEpisode = await this.GetExternalEpisodeAsync(updateId).ConfigureAwait(false);
            if (externalEpisode != null && int.TryParse(externalEpisode.SeriesId, out var seriesId))
            {
                var series = await this.GetExternalShowAsync(seriesId).ConfigureAwait(false);

                if (string.IsNullOrWhiteSpace(series.SeriesName))
                {
                    return false;
                }

                await this.UpdateShow(seriesId, context).ConfigureAwait(false);
                return true;
            }

            return false;
        }

        private async Task UpdateActors(int theTvDbId, int showId)
        {
            var response = await this.Client.Series.GetActorsAsync(theTvDbId).ConfigureAwait(false);

            var actors = response.Data;

            if (actors == null)
            {
                return;
            }

            var actorIds = actors.Select(actor => actor.Id).ToArray();

            var myActors = await this.DbService.Actors.Where(poco => actorIds.Contains(poco.TheTvDbId)).ToListAsync().ConfigureAwait(false);

            foreach (var actor in actors)
            {
                var myActor = myActors.FirstOrDefault(poco => poco.TheTvDbId == actor.Id) ?? new ActorPoco();

                if (!string.IsNullOrWhiteSpace(actor.Image))
                {
                    myActor.ActorImage = actor.Image;
                }

                myActor.TheTvDbId = actor.Id;
                myActor.ActorName = actor.Name;
                myActor.LastUpdated = DateTime.Parse(actor.LastUpdated);

                myActor.ActorId = await this.DbService.SaveAsync(myActor).ConfigureAwait(false);

                var role = await this.DbService.Roles.FirstOrDefaultAsync(poco => poco.ShowId == showId && poco.ActorId == myActor.ActorId)
                                     .ConfigureAwait(false) ?? new RolePoco();

                role.ShowId = showId;
                role.ActorId = myActor.ActorId;
                role.RoleName = actor.Role;

                role.RoleId = await this.DbService.SaveAsync(role).ConfigureAwait(false);
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

            if (!basicEpisodes.Any())
            {
                return;
            }

            // Delete episodes
            var deletedEpisodeIds = context.ExistingEpisodeIds.Except(basicEpisodes.Select(e => e.Id)).ToArray();
            var deletedEpisodes = await this.DbService.Episodes
                                            .Where(poco => deletedEpisodeIds.Contains(poco.TheTvDbId) && poco.ShowId == showId)
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

            var existingGenreIds = await this.DbService.ShowsGenres.Where(poco => poco.ShowId == showId)
                                             .Select(poco => poco.GenreId)
                                             .ToListAsync()
                                             .ConfigureAwait(false);

            foreach (int genreId in genreIds.Except(existingGenreIds))
            {
                await this.DbService.InsertAsync(new ShowGenrePoco
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
            myShow.ShowId = await this.DbService.SaveAsync(myShow).ConfigureAwait(false);

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

    public class DataSyncException : Exception
    {
        public DataSyncException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public DataSyncException(string message)
            : base(message)
        {
        }
    }

    public enum AirDay
    {
        Unknown = 0,

        Monday = 1,

        Tuesday = 2,

        Wednesday = 3,

        Thursday = 4,

        Friday = 5,

        Saturday = 6,

        Sunday = 7,

        Daily = 8
    }
}