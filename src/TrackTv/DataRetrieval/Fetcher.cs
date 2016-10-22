namespace TrackTv.DataRetrieval
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Models;
    using TrackTv.Models.Joint;

    using TvDbSharper;
    using TvDbSharper.BaseSchemas;
    using TvDbSharper.Clients.Episodes.Json;
    using TvDbSharper.Clients.Series.Json;

    using Actor = TrackTv.Models.Actor;
    using ActorData = TvDbSharper.Clients.Series.Json.Actor;

    public class Fetcher
    {
        // ReSharper disable once StyleCop.SA1305
        public Fetcher(TrackTvDbContext context, ITvDbClient tvDbClient)
        {
            this.Context = context;
            this.TvDbClient = tvDbClient;

            this.Mapper = new ObjectMapper();
        }

        private TrackTvDbContext Context { get; }

        private ObjectMapper Mapper { get; }

        private ITvDbClient TvDbClient { get; }

        public async Task AddShowAsync(int seriesId)
        {
            var show = new Show();

            await this.PopulateShowAsync(show, seriesId);
            await this.PopulateActorsAsync(show, seriesId);
            await this.AddEpisodesAsync(show, seriesId);

            this.Context.Shows.Add(show);

            await this.Context.SaveChangesAsync();
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

        private async Task AddEpisodesAsync(Show show, int seriesId)
        {
            var basicEpisodes = await this.GetBasicEpisodesAsync(seriesId);
            var ids = basicEpisodes.Select(episode => episode.Id).ToArray();
            var records = await this.GetFullEpisodesAsync(ids);

            foreach (var record in records)
            {
                var episode = new Episode();

                this.Mapper.MapToEpisode(episode, record);

                show.Episodes.Add(episode);
            }
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

        private async Task<List<BasicEpisode>> GetBasicEpisodesAsync(int seriesId)
        {
            var tasks = new List<Task<TvDbResponse<BasicEpisode[]>>>();

            var firstResponse = await this.TvDbClient.Series.GetEpisodesAsync(seriesId, 1);

            for (int i = 2; i <= firstResponse.Links.Last; i++)
            {
                tasks.Add(this.TvDbClient.Series.GetEpisodesAsync(seriesId, i));
            }

            // ReSharper disable once CoVariantArrayConversion
            Task.WaitAll(tasks.ToArray());

            var basicEpisodes = new List<BasicEpisode>(firstResponse.Data);

            foreach (var task in tasks)
            {
                basicEpisodes.AddRange((await task).Data);
            }

            return basicEpisodes;
        }

        private async Task<List<EpisodeRecord>> GetFullEpisodesAsync(int[] ids)
        {
            var tasks = new List<Task<TvDbResponse<EpisodeRecord>>>();

            foreach (int id in ids)
            {
                tasks.Add(this.TvDbClient.Episodes.GetAsync(id));
            }

            // ReSharper disable once CoVariantArrayConversion
            Task.WaitAll(tasks.ToArray());

            var episodes = new List<EpisodeRecord>();

            foreach (var task in tasks)
            {
                episodes.Add((await task).Data);
            }

            return episodes;
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

                if (existingActorsByTvDbId.ContainsKey(data.Id))
                {
                    actor = existingActorsByTvDbId[data.Id];

                    UpdateActor(actor, data, DateTime.Parse(data.LastUpdated));
                }
                else
                {
                    actor = new Actor(data.Id, data.Name, DateTime.Parse(data.LastUpdated), data.Image);
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

            this.Mapper.MapToShow(show, response.Data);

            string networkName = response.Data.Network;

            var network = this.Context.Networks.FirstOrDefault(x => x.Name.ToLower() == networkName.ToLower());

            if (network == null)
            {
                network = new Network(networkName);
            }

            show.Network = network;

            await this.AddGenresAsync(show, response.Data.Genre);
        }
    }
}