namespace TrackTv.Data
{
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    using TrackTv.Models;
    using TrackTv.Models.Joint;

    public interface ICoreDataStore
    {
        DbSet<Actor> Actors { get; }

        ChangeTracker ChangeTracker { get; }

        DbSet<Episode> Episodes { get; }

        DbSet<Genre> Genres { get; }

        DbSet<Network> Networks { get; }

        DbSet<Show> Shows { get; }

        DbSet<ShowsGenres> ShowsGenres { get; }

        DbSet<ShowsUsers> ShowsUsers { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}