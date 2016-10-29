namespace TrackTV.Data
{
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Models;

    public interface ICoreDataContext
    {
        DbSet<Actor> Actors { get; }

        DbSet<Episode> Episodes { get; }

        DbSet<Genre> Genres { get; }

        DbSet<Network> Networks { get; }

        DbSet<Show> Shows { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}