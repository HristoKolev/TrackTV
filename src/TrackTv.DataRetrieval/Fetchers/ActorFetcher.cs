namespace TrackTV.DataRetrieval.Fetchers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TrackTV.Data.Repositories.Contracts;
    using TrackTV.DataRetrieval.Fetchers.Contracts;

    using TrackTv.Models;
    using TrackTv.Models.Joint;

    using TvDbSharper.Clients.Series;

    using ActorData = TvDbSharper.Clients.Series.Json.Actor;

    public class ActorFetcher : IActorFetcher
    {
        public ActorFetcher(IActorsRepository actorsRepository, ISeriesClient client)
        {
            this.ActorsRepository = actorsRepository;
            this.Client = client;
        }

        private IActorsRepository ActorsRepository { get; }

        private ISeriesClient Client { get; }

        public async Task PopulateActorsAsync(Show show)
        {
            var response = await this.Client.GetActorsAsync(show.TheTvDbId);

            var ids = response.Data.Select(actor => actor.Id).ToArray();

            var actors = await this.ActorsRepository.GetActorsByTheTvDbIdsAsync(ids);

            foreach (var data in response.Data)
            {
                var actor = GetOrCreateActor(actors, data);

                if (!show.HasActor(actor))
                {
                    show.ShowsActors.Add(new ShowsActors(actor, data.Role));
                }
                else
                {
                    var relationship = show.ShowsActors.FirstOrDefault(x => x.ActorId == actor.Id);

                    UpdateShowActorRelationship(relationship, data);
                }
            }
        }

        private static Actor GetOrCreateActor(IEnumerable<Actor> actors, ActorData data)
        {
            var actor = actors.FirstOrDefault(x => x.TheTvDbId == data.Id);

            if (actor != null)
            {
                UpdateActor(actor, data);
            }

            return actor ?? new Actor(data.Id, data.Name, DateTime.Parse(data.LastUpdated), data.Image);
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