namespace TrackTv
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Models;
    using TrackTv.Models.Enums;
    using TrackTv.Models.Joint;

    using TvDbSharper;
    using TvDbSharper.Clients.Series.Json;
    using TvDbSharper.Clients.Updates;

    using Actor = TrackTv.Models.Actor;
    using ActorData = TvDbSharper.Clients.Series.Json.Actor;

    public class Fetcher
    {
        // ReSharper disable once StyleCop.SA1305
        public Fetcher(TrackTvDbContext context, ITvDbClient tvDbClient)
        {
            this.Context = context;
            this.TvDbClient = tvDbClient;
        }

        private TrackTvDbContext Context { get; }

        private ITvDbClient TvDbClient { get; }

        public async Task AddShowAsync(string imdbId)
        {
            int seriesId = await this.SearchShowAsync(imdbId);

            var show = new Show();

            await this.PopulateShowAsync(show, seriesId);
            await this.PopulateActorsAsync(show, seriesId);

            this.Context.Shows.Add(show);

            await this.Context.SaveChangesAsync();
        }

        private static void MapToShow(Series data, Show show)
        {
            show.TvDbId = data.Id;
            show.Name = data.SeriesName;
            show.Banner = data.Banner;
            show.ImdbId = data.ImdbId;
            show.Description = data.Overview;

            long? lastUpdated = data.LastUpdated;
            show.LastUpdated = lastUpdated.ToDateTime();

            AirDay airDay;
            Enum.TryParse(data.AirsDayOfWeek, out airDay);
            show.AirDay = airDay;

            ShowStatus status;
            Enum.TryParse(data.Status, out status);
            show.Status = status;

            if (!string.IsNullOrWhiteSpace(data.FirstAired))
            {
                show.FirstAired = DateTime.ParseExact(data.FirstAired, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrWhiteSpace(data.AirsTime))
            {
                show.AirTime = DateTime.ParseExact("2000-01-01 " + data.AirsTime, "yyyy-MM-dd hh:mm tt", CultureInfo.InvariantCulture);
            }
        }

        private static DateTime ParseDate(string value)
        {
            return DateTime.ParseExact(value, "yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture);
        }

        private static void UpdateActor(Actor actor, ActorData data, DateTime lastUpdated)
        {
            if (lastUpdated > actor.LastUpdated)
            {
                actor.LastUpdated = lastUpdated;
                actor.Name = data.Name;
                actor.Image = data.Image;
            }
        }

        private static void UpdateShowActorRelationship(ShowsActors showsActors, ActorData data)
        {
            showsActors.Role = data.Role;
        }

        private async Task AddGenresAsync(Show show, string[] genreNames)
        {
            var existingGenresByName = this.Context.Genres.Where(genre => genreNames.Contains(genre.Name))
                                           .ToDictionary(genre => genre.Name, genre => genre);

            foreach (string genreName in genreNames)
            {
                Genre genre;

                if (existingGenresByName.ContainsKey(genreName))
                {
                    genre = existingGenresByName[genreName];
                }
                else
                {
                    genre = new Genre(genreName);
                }

                if ((show.Id == default(int)) || (genre.Id == default(int)))
                {
                    show.ShowsGenres.Add(new ShowsGenres(genre));
                }
                else
                {
                    if (!await this.Context.ShowsGenres.AnyAsync(x => (x.ShowId == show.Id) && (x.GenreId == genre.Id)))
                    {
                        show.ShowsGenres.Add(new ShowsGenres(genre));
                    }
                }
            }
        }

        private async Task PopulateActorsAsync(Show show, int seriesId)
        {
            var response = await this.TvDbClient.Series.GetActorsAsync(seriesId);

            var actorsTvDbIds = response.Data.Select(actor => actor.Id).ToArray();

            var existingActorsByTvDbId =
                this.Context.Actors.Where(actor => actorsTvDbIds.Contains(actor.TvDbId)).ToDictionary(actor => actor.TvDbId, actor => actor);

            foreach (var data in response.Data)
            {
                Actor actor;

                var lastUpdated = ParseDate(data.LastUpdated);

                if (existingActorsByTvDbId.ContainsKey(data.Id))
                {
                    actor = existingActorsByTvDbId[data.Id];

                    UpdateActor(actor, data, lastUpdated);
                }
                else
                {
                    actor = new Actor(data.Id, data.Name, lastUpdated, data.Image);
                }

                if ((show.Id == default(int)) || (actor.Id == default(int)))
                {
                    show.ShowsActors.Add(new ShowsActors(actor, data.Role));
                }
                else
                {
                    var relationship = show.ShowsActors.FirstOrDefault(x => x.ActorId == actor.Id);

                    if (relationship == null)
                    {
                        show.ShowsActors.Add(new ShowsActors(actor, data.Role));
                    }
                    else
                    {
                        UpdateShowActorRelationship(relationship, data);
                    }
                }
            }
        }

        private async Task PopulateShowAsync(Show show, int seriesId)
        {
            var response = await this.TvDbClient.Series.GetAsync(seriesId);

            MapToShow(response.Data, show);

            string networkName = response.Data.Network;

            var network = this.Context.Networks.FirstOrDefault(x => x.Name.ToLower() == networkName.ToLower());

            if (network == null)
            {
                network = new Network(networkName);
            }

            show.Network = network;

            await this.AddGenresAsync(show, response.Data.Genre);
        }

        private async Task<int> SearchShowAsync(string imdbId)
        {
            var response = await this.TvDbClient.Search.SearchSeriesByImdbIdAsync(imdbId);

            var series = response.Data.Single();

            return series.Id;
        }
    }
}