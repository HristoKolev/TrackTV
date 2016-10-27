using Microsoft.EntityFrameworkCore;
using TrackTv.Models;

namespace TrackTV.Data
{
    public interface ICoreDataContext
    {
        DbSet<Actor> Actors { get; set; }
        DbSet<Episode> Episodes { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Network> Networks { get; set; }
        DbSet<Show> Shows { get; set; }
    }
}