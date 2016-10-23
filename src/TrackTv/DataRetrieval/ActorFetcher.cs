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

    using ActorData = TvDbSharper.Clients.Series.Json.Actor;

    public class ActorFetcher
    {
        public ActorFetcher(TrackTvDbContext context, ITvDbClient client)
        {
            this.Context = context;
            this.Client = client;
        }

        private ITvDbClient Client { get; }

        private TrackTvDbContext Context { get; }

        public async Task PopulateActorsAsync(Show show, int seriesId)
        {
            var response = await this.Client.Series.GetActorsAsync(seriesId);

            var actorsTvDbIds = response.Data.Select(actor => actor.Id).ToArray();
            var existingActors = await this.Context.Actors.Where(actor => actorsTvDbIds.Contains(actor.TvDbId)).ToListAsync();
            var existingActorsByTvDbId = existingActors.ToDictionary(actor => actor.TvDbId, actor => actor);

            foreach (var data in response.Data)
            {
                Actor actor;

                if (existingActorsByTvDbId.ContainsKey(data.Id))
                {
                    actor = existingActorsByTvDbId[data.Id];

                    UpdateActor(actor, data);
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
                        UpdateShowActorRelationship(relationship, data);
                    }
                }
            }
        }

        private static void UpdateActor(Actor actor, ActorData data)
        {
            var lastUpdated = DateTime.Parse(data.LastUpdated);

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
    }
}