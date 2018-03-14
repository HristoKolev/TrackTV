namespace TrackTv.Updater
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using log4net;

    using LinqToDB;

    using Newtonsoft.Json;

    using StructureMap;

    using TrackTv.Data;
    using TrackTv.Services;
    using TrackTv.Services.Data;

    using TvDbSharper;
    using TvDbSharper.Dto;

    public class ChangeListCompiler
    {
        public ChangeListCompiler(ITvDbClient client, IDbService dbService)
        {
            this.Client = client;
            this.DbService = dbService;
        }

        private ITvDbClient Client { get; }

        private IDbService DbService { get; }

        public async Task<List<ChangeListItem>> GetChangeList(Update[] updates)
        {
            var updateIDs = updates.Select(update => update.Id).ToArray();

            var registeredEpisodes = await this.DbService.Episodes.Where(poco => updateIDs.Contains(poco.Thetvdbid))
                                               .Select(poco => poco.Thetvdbid)
                                               .ToArrayAsync()
                                               .ConfigureAwait(false);

            var registeredShows = await this.DbService.Shows.Where(poco => updateIDs.Contains(poco.Thetvdbid))
                                            .Select(poco => poco.Thetvdbid)
                                            .ToArrayAsync()
                                            .ConfigureAwait(false);

            var updateRecordList = new List<ChangeListItem>();

            updateRecordList.AddRange(registeredEpisodes.Select(id => new ChangeListItem
            {
                Type = UpdateRecordType.Episode,
                TheTvDbID = id,
                EpisodeIDs = Array.Empty<int>(),
                LastUpdated = updates.First(u => u.Id == id).LastUpdated.ToDateTime(),
            }));

            var unknownUpdates = updates.Where(update => !registeredEpisodes.Contains(update.Id) && !registeredShows.Contains(update.Id))
                                        .ToArray();

            var unknownRecords = (await Task.WhenAll(unknownUpdates.Select(this.ResolveUnknownSeries).ToArray()).ConfigureAwait(false))
                                 .Where(record => record != null)
                                 .ToList();

            updateRecordList.AddRange(unknownRecords);

            return updateRecordList;
        }

        private static bool IsValidSeries(Series series)
        {
            return !string.IsNullOrWhiteSpace(series?.SeriesName) && !series.SeriesName.StartsWith("***Duplicate");
        }

        private async Task<EpisodeRecord> GetExternalEpisodeAsync(int updateId)
        {
            try
            {
                var response = await this.Client.Episodes.GetAsync(updateId).ConfigureAwait(false);

                return response?.Data;
            }
            catch (TvDbServerException ex)
            {
                if (ex.StatusCode == 404)
                {
                    return null;
                }

                throw;
            }
        }

        private async Task<Series> GetExternalShowAsync(int updateId)
        {
            try
            {
                var response = await this.Client.Series.GetAsync(updateId).ConfigureAwait(false);

                return response?.Data;
            }
            catch (TvDbServerException ex)
            {
                if (ex.StatusCode == 404)
                {
                    return null;
                }

                throw;
            }
        }

        private async Task<ChangeListItem> GetSeriesRecord(int seriesId, DateTime lastUpdated)
        {
            var series = await this.GetExternalShowAsync(seriesId).ConfigureAwait(false);

            if (IsValidSeries(series))
            {
                var episodes = await this.Client.Series.GetBasicEpisodesAsync(seriesId).ConfigureAwait(false);

                return new ChangeListItem
                {
                    Type = UpdateRecordType.Show,
                    TheTvDbID = seriesId,
                    EpisodeIDs = episodes.Select(e => e.Id).ToArray(),
                    LastUpdated = lastUpdated
                };
            }

            return null;
        }

        private async Task<ChangeListItem> ResolveUnknownSeries(Update update)
        {
            int seriesID = update.Id;

            var episode = await this.GetExternalEpisodeAsync(update.Id).ConfigureAwait(false);
            if (episode != null && int.TryParse(episode.SeriesId, out var episodeSeriesID))
            {
                seriesID = episodeSeriesID;
            }

            var record = await this.GetSeriesRecord(seriesID, update.LastUpdated.ToDateTime()).ConfigureAwait(false);

            return record;
        }
    }

    public class ChangeListApplier
    {
        public ChangeListApplier(IDbService dbService, ITvDbClient client, ApiResultRepository apiResultRepository)
        {
            this.DbService = dbService;
            this.Client = client;
            this.ApiResultRepository = apiResultRepository;
        }

        private ApiResultRepository ApiResultRepository { get; }

        private ITvDbClient Client { get; }
 

        private IDbService DbService { get; }

        public Task ApplyChange(ChangeListItem change)
        {
            switch (change.Type)
            {
                case UpdateRecordType.Episode :
                {
                    return this.ApplyEpisodeChange(change);
                }
                case UpdateRecordType.Show :
                {
                    return this.ApplyShowChange(change);
                }
                default :
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Applies an episode update. This method is for updates only.
        /// </summary>
        private async Task ApplyEpisodeChange(ChangeListItem change)
        {
            var myEpisode = await this.DbService.Episodes.FirstAsync(poco => poco.Thetvdbid == change.TheTvDbID).ConfigureAwait(false);

            var externalEpisode = await this.GetExternalEpisodeAsync(change.TheTvDbID).ConfigureAwait(false);

            MapToEpisode(myEpisode, externalEpisode);

            await this.DbService.Update(myEpisode).ConfigureAwait(false);

            await this.ApiResultRepository.SaveApiResult(externalEpisode, ApiResultType.Episode, change.TheTvDbID).ConfigureAwait(false);
        }

        private async Task ApplyShowChange(ChangeListItem change)
        {
            var myShow = await this.DbService.Shows.FirstOrDefaultAsync(poco => poco.Thetvdbid == change.TheTvDbID).ConfigureAwait(false)
                         ?? new ShowPoco
                         {
                             Thetvdbid = change.TheTvDbID
                         };

            var externalShow = await this.GetExternalShowAsync(myShow.Thetvdbid).ConfigureAwait(false);
            MapToShow(myShow, externalShow);

            myShow.NetworkID = await this.GetOrCreateNetwork(externalShow.Network).ConfigureAwait(false);
            myShow.ShowID = await this.DbService.Save(myShow).ConfigureAwait(false);

            await this.UpdateGenres(externalShow.Genre, myShow.ShowID).ConfigureAwait(false);
            await this.UpdateActors(myShow.Thetvdbid, myShow.ShowID).ConfigureAwait(false);
            await this.UpdateEpisodes(myShow.ShowID, change.EpisodeIDs).ConfigureAwait(false);

            await this.ApiResultRepository.SaveApiResult(externalShow, ApiResultType.Show, change.TheTvDbID).ConfigureAwait(false);
        }

        private async Task<EpisodeRecord> GetExternalEpisodeAsync(int updateId)
        {
            try
            {
                var response = await this.Client.Episodes.GetAsync(updateId).ConfigureAwait(false);

                return response?.Data;
            }
            catch (TvDbServerException ex)
            {
                if (ex.StatusCode == 404)
                {
                    return null;
                }

                throw;
            }
        }

        private async Task<Series> GetExternalShowAsync(int updateId)
        {
            try
            {
                var response = await this.Client.Series.GetAsync(updateId).ConfigureAwait(false);

                return response?.Data;
            }
            catch (TvDbServerException ex)
            {
                if (ex.StatusCode == 404)
                {
                    return null;
                }

                throw;
            }
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

                return await this.DbService.Insert(genre).ConfigureAwait(false);
            }

            return genre.GenreID;
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

                return await this.DbService.Insert(network).ConfigureAwait(false);
            }

            return network.NetworkID;
        }

        private static void MapToEpisode(EpisodePoco episode, EpisodeRecord data)
        {
            episode.EpisodeTitle = data.EpisodeName;
            episode.EpisodeDescription = data.Overview;

            if (!string.IsNullOrWhiteSpace(data.ImdbId))
            {
                episode.Imdbid = data.ImdbId;
            }

            episode.EpisodeNumber = data.AiredEpisodeNumber.Value;
            episode.SeasonNumber = data.AiredSeason.Value;
            episode.Thetvdbid = data.Id;

            if (!string.IsNullOrWhiteSpace(data.FirstAired))
            {
                episode.FirstAired = DateParser.ParseFirstAired(data.FirstAired);
            }

            episode.LastUpdated = data.LastUpdated.ToDateTime();
        }

        private static void MapToShow(ShowPoco show, Series data)
        {
            show.Thetvdbid = data.Id;
            show.ShowName = data.SeriesName;

            if (!string.IsNullOrWhiteSpace(data.Banner))
            {
                show.ShowBanner = data.Banner;
            }

            if (!string.IsNullOrWhiteSpace(data.ImdbId))
            {
                show.Imdbid = data.ImdbId;
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
                show.FirstAired = DateParser.ParseFirstAired(data.FirstAired);
            }

            if (!string.IsNullOrWhiteSpace(data.AirsTime))
            {
                show.AirTime = DateParser.ParseAirTime(data.AirsTime);
            }
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

            var myActors = await this.DbService.Actors.Where(poco => actorIds.Contains(poco.Thetvdbid)).ToListAsync().ConfigureAwait(false);

            foreach (var actor in actors)
            {
                var myActor = myActors.FirstOrDefault(poco => poco.Thetvdbid == actor.Id) ?? new ActorPoco();

                if (!string.IsNullOrWhiteSpace(actor.Image))
                {
                    myActor.ActorImage = actor.Image;
                }

                myActor.Thetvdbid = actor.Id;
                myActor.ActorName = actor.Name;
                myActor.LastUpdated = DateTime.Parse(actor.LastUpdated);

                myActor.ActorID = await this.DbService.Save(myActor).ConfigureAwait(false);

                var role = await this.DbService.Roles.FirstOrDefaultAsync(poco => poco.ShowID == showId && poco.ActorID == myActor.ActorID)
                                     .ConfigureAwait(false) ?? new RolePoco();

                role.ShowID = showId;
                role.ActorID = myActor.ActorID;
                role.RoleName = actor.Role;

                role.RoleID = await this.DbService.Save(role).ConfigureAwait(false);
            }
        }

        private async Task UpdateEpisodes(int showID, int[] episodeIDs)
        {
            var deletedEpisodeIDs = await this.DbService.Episodes
                                              .Where(poco => poco.ShowID == showID && !episodeIDs.Contains(poco.Thetvdbid))
                                              .Select(poco => poco.EpisodeID)
                                              .ToArrayAsync()
                                              .ConfigureAwait(false);

            await this.DbService.Delete<EpisodePoco>(deletedEpisodeIDs).ConfigureAwait(false);

            var nonDeletedEpisodeIDs = episodeIDs.Except(deletedEpisodeIDs).ToArray();

            var externalEpisodes = await this.Client.Episodes.GetFullEpisodesAsync(nonDeletedEpisodeIDs).ConfigureAwait(false);

            foreach (int theTvDbID in nonDeletedEpisodeIDs)
            {
                var episode = await this.DbService.Episodes.Where(poco => poco.Thetvdbid == theTvDbID)
                                        .FirstOrDefaultAsync()
                                        .ConfigureAwait(false) ?? new EpisodePoco
                {
                    ShowID = showID
                };

                var externalEpisode = externalEpisodes.First(record => record.Id == theTvDbID);

                MapToEpisode(episode, externalEpisode);

                await this.DbService.Save(episode).ConfigureAwait(false);

                await this.ApiResultRepository.SaveApiResult(externalEpisode, ApiResultType.Episode, externalEpisode.Id)
                          .ConfigureAwait(false);
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

            var existingGenreIds = await this.DbService.ShowsGenres.Where(poco => poco.ShowID == showId)
                                             .Select(poco => poco.GenreID)
                                             .ToListAsync()
                                             .ConfigureAwait(false);

            foreach (int genreId in genreIds.Except(existingGenreIds))
            {
                await this.DbService.Insert(new ShowGenrePoco
                          {
                              GenreID = genreId,
                              ShowID = showId
                          })
                          .ConfigureAwait(false);
            }
        }
    }

    public class ChangeListItem
    {
        public int[] EpisodeIDs { get; set; }

        public DateTime LastUpdated { get; set; }

        public int TheTvDbID { get; set; }

        public UpdateRecordType Type { get; set; }
    }

    public enum UpdateRecordType
    {
        Episode,

        Show
    }

    public class DataSynchronizer2
    {
        public DataSynchronizer2(
            ILog log,
            SettingsService settingsService,
            ErrorHandler errorHandler,
            ITvDbClient client)
        {
            this.Log = log;
            this.SettingsService = settingsService;
            this.ErrorHandler = errorHandler;
            this.Client = client;
        }

        private ITvDbClient Client { get; }
 
        private ErrorHandler ErrorHandler { get; }

        private ILog Log { get; }

        private SettingsService SettingsService { get; }

        public async Task PerformUpdate(IContainer container)
        {
            if (!bool.Parse(await this.SettingsService.GetSettingAsync(Setting.DisableDatabaseUpdate).ConfigureAwait(false)))
            {
                var lastUpdated = DateTime.Parse(await this.SettingsService.GetSettingAsync(Setting.LastDatabaseUpdate).ConfigureAwait(false))
                                          .ToUniversalTime();

                var changeListCompiler = container.GetInstance<ChangeListCompiler>();
                var failedChangeRepository = container.GetInstance<ApiChangeRepository>();

                var updates = await this.GetUpdates(lastUpdated, DateTime.UtcNow).ConfigureAwait(false);
                var changeList = await this.GetChangeList(updates, changeListCompiler).ConfigureAwait(false);

                var failedChangeList = await failedChangeRepository.GetFailedUpdates().ConfigureAwait(false);

                var fullChangeList = changeList.Concat(failedChangeList);

                int index = 1;

                foreach (var change in fullChangeList)
                {
                    await this.ApplyChange(change, lastUpdated, index, changeList.Count, container)
                              .ContinueWith(task => index++).ConfigureAwait(false);
                }

                Global.Log.Debug("Updater finished successfully.");
            }
            else
            {
                Global.Log.Debug("Updates disabled. Exiting...");
            }
        }
 
        private async Task ApplyChange(ChangeListItem change, DateTime lastUpdated, int index, int maxCount, IContainer masterContainer)
        {
            using (var container = masterContainer.CreateChildContainer())
            {
                try
                {
                    var dbService = container.GetInstance<IDbService>();
                    var settingsService = container.GetInstance<SettingsService>();
                    var applier = container.GetInstance<ChangeListApplier>();
                    var failedChangeRepository = container.GetInstance<ApiChangeRepository>();

                    await dbService.ExecuteInTransaction(async (transaction) =>
                    {
                        try
                        {
                            await applier.ApplyChange(change).ConfigureAwait(false);

                            await failedChangeRepository.RemoveFailedUpdate(change.TheTvDbID).ConfigureAwait(false);
                        }
                        catch (Exception e)
                        {
                            transaction.Rollback();

                            await failedChangeRepository.AddFailedApiChange(change).ConfigureAwait(false);

                            throw new DataSyncException(
                                $"[{index}/{maxCount}] Failed to apply a change. (ID={change.TheTvDbID},"
                                + $" Type={change.Type.ToString()}, EpisodeCount={change.EpisodeIDs.Length})", e);
                        }

                        DateTime newLastUpdated = new[]
                        {
                            lastUpdated,
                            change.LastUpdated
                        }.Max();

                        await settingsService.SetSettingAsync(Setting.LastDatabaseUpdate, newLastUpdated.ToString("O"))
                                            .ConfigureAwait(false);
                    }).ConfigureAwait(false);

                    this.Log.Debug($"[{index}/{maxCount}] Successfuly applied change (ID={change.TheTvDbID},"
                                   + $" Type={change.Type.ToString()}, EpisodeCount={change.EpisodeIDs.Length})");
                }
                catch (Exception e)
                {
                    await this.ErrorHandler.HandleErrorAsync(e).ConfigureAwait(false);
                }
            }
        }

        private async Task<List<ChangeListItem>> GetChangeList(Update[] updates, ChangeListCompiler changeListCompiler)
        {
            List<ChangeListItem> changeList;
            var changeListWatch = Stopwatch.StartNew();

            try
            {
                changeList = await changeListCompiler.GetChangeList(updates).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new DataSyncException("An error occured while compiling the change list.", e);
            }

            this.Log.Debug($"Change list was compiled successfuly in {changeListWatch.Elapsed:hh\\:mm\\:ss}.");

            int episodeCount = changeList.Count(item => item.Type == UpdateRecordType.Episode);
            int showCount = changeList.Count(item => item.Type    == UpdateRecordType.Show);
            this.Log.Debug($"{episodeCount} episodes. {showCount} shows.");

            return changeList;
        }

        private async Task<Update[]> GetUpdates(DateTime fromTime, DateTime toTime)
        {
            Update[] updates;
            var updatesWatch = Stopwatch.StartNew();

            try
            {
                var response = await this.Client.Updates.GetAccumulatedAsync(fromTime, toTime).ConfigureAwait(false);

                if (response.Data == null)
                {
                    updates = Array.Empty<Update>();
                }
                else
                {
                    updates = response.Data.OrderBy(update => update.LastUpdated).ToArray();
                }
            }
            catch (Exception e)
            {
                throw new DataSyncException("An error occured while getting the updates from the server.", e);
            }

            this.Log.Debug($"{updates.Length} updates were detected in {updatesWatch.Elapsed:hh\\:mm\\:ss}.");
            updatesWatch.Stop();
            return updates;
        }
    }

    public class ApiChangeRepository
    {
        public ApiChangeRepository(IDbService dbService)
        {
            this.DbService = dbService;
        }

        private IDbService DbService { get; }

        public async Task AddFailedApiChange(ChangeListItem change)
        {
            var apiChange = await this.DbService.ApiChanges.FirstOrDefaultAsync(p => p.ApiChangeThetvdbid == change.TheTvDbID)
                                      .ConfigureAwait(false) ?? new ApiChangePoco();

            var utcNow = DateTime.UtcNow;

            apiChange.ApiChangeLastFailedTime = utcNow;
            apiChange.ApiChangeBody = JsonConvert.SerializeObject(change);

            apiChange.ApiChangeFailCount++;

            if (((IPoco)apiChange).IsNew())
            {
                apiChange.ApiChangeDate = utcNow;
                apiChange.ApiChangeThetvdbid = change.TheTvDbID;
                apiChange.ApiChangeThetvdbLastUpdated = change.LastUpdated;
            }

            await this.DbService.Save(apiChange).ConfigureAwait(false);
        }

        public async Task<List<ChangeListItem>> GetFailedUpdates()
        {
            var failedUpdates = await this.DbService.ApiChanges.ToListAsync().ConfigureAwait(false);

            return failedUpdates
                   .Select(p => p.ApiChangeBody)
                   .Select(JsonConvert.DeserializeObject<ChangeListItem>)
                   .ToList();
        }

        public async Task RemoveFailedUpdate(int thetvdbid)
        {
            var poco = await this.DbService.ApiChanges.FirstOrDefaultAsync(p => p.ApiChangeThetvdbid == thetvdbid).ConfigureAwait(false);

            if (poco != null)
            {
                await this.DbService.Delete(poco).ConfigureAwait(false);
            }
        }
    }
}