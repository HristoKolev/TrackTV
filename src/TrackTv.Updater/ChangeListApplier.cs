namespace TrackTv.Updater
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using log4net;

    using LinqToDB;

    using TrackTv.Data;
    using TrackTv.Services;
    using TrackTv.Services.Data;
    using TrackTv.Updater.Infrastructure;

    using TvDbSharper;
    using TvDbSharper.Dto;

    public class ChangeListApplier
    {
        public ChangeListApplier(
            IDbService dbService,
            ITvDbClient client,
            ApiResultRepository apiResultRepository,
            ILog log,
            SettingsService settingsService)
        {
            this.DbService = dbService;
            this.Client = client;
            this.ApiResultRepository = apiResultRepository;
            this.Log = log;
            this.SettingsService = settingsService;
        }

        private ApiResultRepository ApiResultRepository { get; }

        private ITvDbClient Client { get; }

        private IDbService DbService { get; }

        private ILog Log { get; }

        private SettingsService SettingsService { get; }

        public Task ApplyChange(ApiChangePoco change)
        {
            switch (change.ApiChangeType)
            {
                case (int)ApiChangeType.Episode :
                {
                    return this.ApplyEpisodeChange(change);
                }
                case (int)ApiChangeType.Show :
                {
                    return this.ApplyShowChange(change);
                }
                default :
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
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

        /// <summary>
        /// Applies an episode update. This method is for updates only.
        /// </summary>
        private async Task ApplyEpisodeChange(ApiChangePoco change)
        {
            var myEpisode = await this.DbService.Episodes.FirstAsync(poco => poco.Thetvdbid == change.ApiChangeThetvdbid)
                                      .ConfigureAwait(false);

            var externalEpisode = await this.Client.Episodes.GetExternalEpisodeAsync(change.ApiChangeThetvdbid).ConfigureAwait(false);

            MapToEpisode(myEpisode, externalEpisode);

            await this.DbService.Update(myEpisode).ConfigureAwait(false);

            await this.ApiResultRepository.SaveApiResult(externalEpisode, ApiChangeType.Episode, change.ApiChangeThetvdbid)
                      .ConfigureAwait(false);
        }

        private async Task ApplyShowChange(ApiChangePoco change)
        {
            var myShow = await this.DbService.Shows.FirstOrDefaultAsync(poco => poco.Thetvdbid == change.ApiChangeThetvdbid)
                                   .ConfigureAwait(false) ?? new ShowPoco
            {
                Thetvdbid = change.ApiChangeThetvdbid
            };

            var externalShow = await this.Client.Series.GetExternalShowAsync(myShow.Thetvdbid).ConfigureAwait(false);
            MapToShow(myShow, externalShow);

            myShow.NetworkID = await this.GetOrCreateNetwork(externalShow.Network).ConfigureAwait(false);
            myShow.ShowID = await this.DbService.Save(myShow).ConfigureAwait(false);

            await this.UpdateGenres(externalShow.Genre, myShow.ShowID).ConfigureAwait(false);
            await this.UpdateActors(myShow.Thetvdbid, myShow.ShowID).ConfigureAwait(false);

            await this.UpdateEpisodes(myShow.ShowID, myShow.Thetvdbid).ConfigureAwait(false);

            await this.ApiResultRepository.SaveApiResult(externalShow, ApiChangeType.Show, change.ApiChangeThetvdbid).ConfigureAwait(false);
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
            if (string.IsNullOrWhiteSpace(networkName))
            {
                // The default 'Unknown' network.
                return 1;
            }

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
                role.RoleName = string.IsNullOrWhiteSpace(actor.Role) ? null : actor.Role;

                role.RoleID = await this.DbService.Save(role).ConfigureAwait(false);
            }
        }

        private async Task UpdateEpisodes(int showID, int thetvdbid)
        {
            // External episodes
            var externalEpisodes = (await this.Client.Series.GetBasicEpisodesAsync(thetvdbid).ConfigureAwait(false))
                                        .Select(e => new
                                        {
                                            TheTvDbID = e.Id,
                                            LastUpdated = e.LastUpdated.ToDateTime()
                                        }).ToArray();
            
            // My episodes 
            var myEpisodes = await this.DbService.Episodes.Where(poco => poco.ShowID == showID)
                                       .Select(poco => new
                                       {
                                           TheTvDbID = poco.Thetvdbid,
                                           LastUpdated = poco.LastUpdated,
                                           EpisodeID = poco.EpisodeID
                                       })
                                       .ToArrayAsync()
                                       .ConfigureAwait(false);

            // Delete
            if (myEpisodes.Any())
            {
                var externalEpisodeIDs = externalEpisodes.Select(x => x.TheTvDbID).ToArray();

                var myDeletedEpisodeIDs = myEpisodes.Where(x => !externalEpisodeIDs.Contains(x.TheTvDbID)).Select(a => a.EpisodeID).ToArray();

                await this.DbService.Delete<EpisodePoco>(myDeletedEpisodeIDs).ConfigureAwait(false);
            }

            // Add/Update
            var idsForUpdate = externalEpisodes.Where(externalEpisode =>
            {
                var myEpisode = myEpisodes.FirstOrDefault(x => x.TheTvDbID == externalEpisode.TheTvDbID);

                if (myEpisode == null)
                {
                    return true;
                }

                return myEpisode.LastUpdated < externalEpisode.LastUpdated;
            }).Select(a => a.TheTvDbID).ToArray();

            this.Log.Debug($"{idsForUpdate.Length} episodes for update. Total: {externalEpisodes.Length}");

            int episodeChunkSize = int.Parse(await this.SettingsService.GetSettingAsync(Setting.UpdateEpisodeChunkSize).ConfigureAwait(false));

            int chunk = 1;

            foreach (var episodeIDsChunk in idsForUpdate.Split(episodeChunkSize))
            {
                if (idsForUpdate.Length > episodeChunkSize)
                {
                    this.Log.Debug($"Episode chunk {chunk++} of {Math.Ceiling(idsForUpdate.Length / (decimal)episodeChunkSize)}");
                }

                var fullExternalEpisodes = await this.Client.Episodes.GetFullEpisodesAsync(episodeIDsChunk).ConfigureAwait(false);

                foreach (var externalEpisode in fullExternalEpisodes)
                {
                    var episode = await this.DbService.Episodes.Where(poco => poco.Thetvdbid == externalEpisode.Id)
                                            .FirstOrDefaultAsync()
                                            .ConfigureAwait(false) ?? new EpisodePoco
                    {
                        ShowID = showID
                    };

                    MapToEpisode(episode, externalEpisode);

                    await this.DbService.Save(episode).ConfigureAwait(false);

                    await this.ApiResultRepository.SaveApiResult(externalEpisode, ApiChangeType.Episode, externalEpisode.Id)
                              .ConfigureAwait(false);
                }
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