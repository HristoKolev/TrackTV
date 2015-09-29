namespace TrackTV.Data.Contracts
{
    using TrackTV.Data.Common.Repositories.Contracts;
    using TrackTV.Models;

    public interface ITrackTVData
    {
        IRepository<Episode, int> Episodes { get; }

        IRepository<Genre, int> Genres { get; }

        IRepository<Network, int> Networks { get; }

        IRepository<Season, int> Seasons { get; }

        IRepository<Show, int> Shows { get; }

        IRepository<ApplicationUser, string> Users { get; }

        int SaveChanges();
    }
}