namespace TrackTv.Updater
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using log4net;

    using LinqToDB;

    using TrackTv.Data;
    using TrackTv.Services.Data;

    using TvDbSharper;
    using TvDbSharper.Dto;

    public class ChangeListApplier
    {
        private const int ChunkSize = 200;
        
        public ChangeListApplier(IDbService dbService, ITvDbClient client, ApiResultRepository apiResultRepository, ILog log)
        {
            this.DbService = dbService;
            this.Client = client;
            this.ApiResultRepository = apiResultRepository;
            this.Log = log;
        }

        private ApiResultRepository ApiResultRepository { get; }

        private ILog Log { get; }

        private ITvDbClient Client { get; }
 
        private IDbService DbService { get; }

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

        /// <summary>
        /// Applies an episode update. This method is for updates only.
        /// </summary>
        private async Task ApplyEpisodeChange(ApiChangePoco change)
        {
            var myEpisode = await this.DbService.Episodes.FirstAsync(poco => poco.Thetvdbid == change.ApiChangeThetvdbid).ConfigureAwait(false);

            var externalEpisode = await this.GetExternalEpisodeAsync(change.ApiChangeThetvdbid).ConfigureAwait(false);

            MapToEpisode(myEpisode, externalEpisode);

            await this.DbService.Update(myEpisode).ConfigureAwait(false);

            await this.ApiResultRepository.SaveApiResult(externalEpisode, ApiResultType.Episode, change.ApiChangeThetvdbid).ConfigureAwait(false);
        }
        
        private async Task ApplyShowChange(ApiChangePoco change)
        {
            var myShow = await this.DbService.Shows.FirstOrDefaultAsync(poco => poco.Thetvdbid == change.ApiChangeThetvdbid).ConfigureAwait(false)
                         ?? new ShowPoco
                         {
                             Thetvdbid = change.ApiChangeThetvdbid
                         };

            var externalShow = await this.GetExternalShowAsync(myShow.Thetvdbid).ConfigureAwait(false);
            MapToShow(myShow, externalShow);

            myShow.NetworkID = await this.GetOrCreateNetwork(externalShow.Network).ConfigureAwait(false);
            myShow.ShowID = await this.DbService.Save(myShow).ConfigureAwait(false);

            await this.UpdateGenres(externalShow.Genre, myShow.ShowID).ConfigureAwait(false);
            await this.UpdateActors(myShow.Thetvdbid, myShow.ShowID).ConfigureAwait(false);

            await this.UpdateEpisodes(myShow.ShowID, myShow.Thetvdbid).ConfigureAwait(false);

            await this.ApiResultRepository.SaveApiResult(externalShow, ApiResultType.Show, change.ApiChangeThetvdbid).ConfigureAwait(false);
        }

        private async Task<EpisodeRecord> GetExternalEpisodeAsync(int updateId)
        {
            if (updateId == 0)
            {
                return null;
            }

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
            if (updateId == 0)
            {
                return null;
            }

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
                role.RoleName = string.IsNullOrWhiteSpace(actor.Role) ? null : actor.Role;

                role.RoleID = await this.DbService.Save(role).ConfigureAwait(false);
            }
        }

        private async Task UpdateEpisodes(int showID, int thetvdbid)
        {
            int[] episodeIDs = (await this.Client.Series.GetBasicEpisodesAsync(thetvdbid).ConfigureAwait(false)).Select(e => e.Id).ToArray();

            var deletedEpisodeIDs = await this.DbService.Episodes
                                              .Where(poco => poco.ShowID == showID && !episodeIDs.Contains(poco.Thetvdbid))
                                              .Select(poco => poco.EpisodeID)
                                              .ToArrayAsync()
                                              .ConfigureAwait(false);

            await this.DbService.Delete<EpisodePoco>(deletedEpisodeIDs).ConfigureAwait(false);

            var nonDeletedEpisodeIDs = episodeIDs.Except(deletedEpisodeIDs).ToArray();

            int chunk = 1;

            foreach (var task in this.Client.Episodes.GetFullEpisodeIterator(nonDeletedEpisodeIDs, ChunkSize))
            {
                var externalEpisodes = await task.ConfigureAwait(false);

                if (nonDeletedEpisodeIDs.Length > ChunkSize)
                {
                    this.Log.Debug($"Episode chunk {chunk++} of ~({nonDeletedEpisodeIDs.Length / ChunkSize})");
                }
 
                foreach (var externalEpisode in externalEpisodes)
                {
                    var episode = await this.DbService.Episodes.Where(poco => poco.Thetvdbid == externalEpisode.Id)
                                            .FirstOrDefaultAsync()
                                            .ConfigureAwait(false) ?? new EpisodePoco
                    {
                        ShowID = showID
                    };

                    MapToEpisode(episode, externalEpisode);

                    await this.DbService.Save(episode).ConfigureAwait(false);

                    await this.ApiResultRepository.SaveApiResult(externalEpisode, ApiResultType.Episode, externalEpisode.Id)
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