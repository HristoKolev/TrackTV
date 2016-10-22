namespace TrackTv.DataRetrieval
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Models;
    using TrackTv.Models.Joint;

    using TvDbSharper;

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

        private async Task AddEpisodesAsync(Show show, int seriesId)
        {
            var basicEpisodes = await this.TvDbClient.Series.GetBasicEpisodesAsync(seriesId);
            var ids = basicEpisodes.Select(episode => episode.Id).ToArray();
            var records = await this.TvDbClient.Episodes.GetFullEpisodesAsync(ids);

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

                    this.Mapper.UpdateActor(actor, data);
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
                        this.Mapper.UpdateShowActorRelationship(relationship, data);
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