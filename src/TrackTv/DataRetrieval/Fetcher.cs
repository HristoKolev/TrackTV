namespace TrackTv.DataRetrieval
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Models;
    using TrackTv.Models.Extensions;
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

            await this.AddAllEpisodes(show, seriesId);

            this.Context.Shows.Add(show);

            await this.Context.SaveChangesAsync();
        }

        public async Task UpdateShowAsync(int seriesId)
        {
            var show =
                await
                    this.Context.Shows.Include(x => x.ShowsGenres)
                        .Include(x => x.ShowsActors)
                        .Include(x => x.Network)
                        .Include(x => x.Episodes)
                        .FirstOrDefaultAsync(x => x.TvDbId == seriesId);

            await this.PopulateShowAsync(show, seriesId);
            await this.PopulateActorsAsync(show, seriesId);

            await this.AddNewEpisodesAsync(show, seriesId);

            await this.Context.SaveChangesAsync();
        }

        private async Task AddAllEpisodes(Show show, int seriesId)
        {
            var basicEpisodes = await this.TvDbClient.Series.GetBasicEpisodesAsync(seriesId);
            var ids = basicEpisodes.Select(x => x.Id).ToArray();

            await this.AddEpisodesAsync(show, ids);
        }

        private async Task AddEpisodesAsync(Show show, int[] ids)
        {
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
            var existingGenres = await this.Context.Genres.Where(genre => genreNames.Contains(genre.Name)).ToListAsync();
            var existingGenresByName = existingGenres.ToDictionary(genre => genre.Name, genre => genre);

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

                if (!show.IsPersisted() || !genre.IsPersisted())
                {
                    show.ShowsGenres.Add(new ShowsGenres(genre));
                }
                else
                {
                    if (!show.ShowsGenres.Any(x => (x.ShowId == show.Id) && (x.GenreId == genre.Id)))
                    {
                        show.ShowsGenres.Add(new ShowsGenres(genre));
                    }
                }
            }
        }

        private async Task AddNetwork(Show show, string networkName)
        {
            if (!show.HasNetwork() || (show.Network.Name != networkName))
            {
                var existingNetwork = await this.Context.Networks.FirstOrDefaultAsync(x => x.Name.ToLower() == networkName.ToLower());

                show.Network = existingNetwork ?? new Network(networkName);
            }
        }

        private async Task AddNewEpisodesAsync(Show show, int seriesId)
        {
            var existingEpisodeIds = show.Episodes.Select(x => x.TvDbId).ToList();
            var basicEpisodes = await this.TvDbClient.Series.GetBasicEpisodesAsync(seriesId);

            var newIds = basicEpisodes.Select(x => x.Id).ToList().Except(existingEpisodeIds).ToArray();

            await this.AddEpisodesAsync(show, newIds);
        }

        private async Task PopulateActorsAsync(Show show, int seriesId)
        {
            var response = await this.TvDbClient.Series.GetActorsAsync(seriesId);

            var actorsTvDbIds = response.Data.Select(actor => actor.Id).ToArray();
            var existingActors = await this.Context.Actors.Where(actor => actorsTvDbIds.Contains(actor.TvDbId)).ToListAsync();
            var existingActorsByTvDbId = existingActors.ToDictionary(actor => actor.TvDbId, actor => actor);

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

                if (!show.IsPersisted() || !actor.IsPersisted())
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

            await this.AddNetwork(show, networkName);

            await this.AddGenresAsync(show, response.Data.Genre);
        }
    }
}