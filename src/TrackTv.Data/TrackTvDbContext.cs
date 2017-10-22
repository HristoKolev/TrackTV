namespace TrackTv.Data
{
    using Microsoft.EntityFrameworkCore;

    using TrackTv.Data.Models;

    public class TrackTvDbContext : DbContext
    {
        public TrackTvDbContext(DbContextOptions<TrackTvDbContext> options)
            : base(options)
        {
        }

        public TrackTvDbContext()
        {
        }

        public DbSet<Actor> Actors { get; set; }

        public DbSet<Episode> Episodes { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Network> Networks { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Show> Shows { get; set; }

        public DbSet<ShowsGenres> ShowsGenres { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ShowsGenres>().HasKey(t => new
            {
                t.ShowId,
                t.GenreId
            });
        }
    }
}