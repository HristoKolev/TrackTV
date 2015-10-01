namespace TrackTV.Data
{
    using NetInfrastructure.Data.Repositories;

    using TrackTV.Models;

    public interface ITrackTVData
    {
        IRepository<Episode> Episodes { get; }

        IRepository<Genre> Genres { get; }

        IRepository<Network> Networks { get; }

        IRepository<Season> Seasons { get; }

        IRepository<Show> Shows { get; }

        IRepository<ApplicationUser, string> Users { get; }

        int SaveChanges();
    }
}