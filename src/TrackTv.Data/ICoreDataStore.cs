namespace TrackTV.Data
{
    using Microsoft.EntityFrameworkCore;

    using TrackTv.Models;
    using TrackTv.Models.Joint;

    public interface ICoreDataStore
    {
        DbSet<Actor> Actors { get; }

        DbSet<Episode> Episodes { get; }

        DbSet<Genre> Genres { get; }

        DbSet<Network> Networks { get; }

        DbSet<Show> Shows { get; }

        DbSet<ShowsGenres> ShowsGenres { get; }
    }
}