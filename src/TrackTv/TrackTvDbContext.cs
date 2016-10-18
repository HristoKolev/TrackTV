namespace TrackTv
{
    using Microsoft.EntityFrameworkCore;

    using TrackTv.Models;
    using TrackTv.Models.Joint;

    public class TrackTvDbContext : DbContext
    {
        public DbSet<Actor> Actors { get; set; }

        public DbSet<Episode> Episodes { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Network> Networks { get; set; }

        public DbSet<Show> Shows { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Server=.;Database=TrackTvDb;Trusted_Connection=True;MultipleActiveResultSets=True;");

            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ShowsUsers
            modelBuilder.Entity<ShowsUsers>().HasKey(t => new
                        {
                            t.UserId,
                            t.ShowId
                        });
            modelBuilder.Entity<ShowsUsers>().HasOne(t => t.User).WithMany(user => user.ShowsUsers).HasForeignKey(t => t.UserId);
            modelBuilder.Entity<ShowsUsers>().HasOne(t => t.Show).WithMany(show => show.ShowsUsers).HasForeignKey(t => t.ShowId);

            // ShowsActors
            modelBuilder.Entity<ShowsActors>().HasKey(t => new
                        {
                            t.ShowId,
                            t.ActorId
                        });
            modelBuilder.Entity<ShowsActors>().HasOne(t => t.Show).WithMany(show => show.ShowsActors).HasForeignKey(t => t.ShowId);
            modelBuilder.Entity<ShowsActors>().HasOne(t => t.Actor).WithMany(actor => actor.ShowsActors).HasForeignKey(t => t.ActorId);

            // ShowsGenres
            modelBuilder.Entity<ShowsGenres>().HasKey(t => new
                        {
                            t.ShowId,
                            t.GenreId
                        });
            modelBuilder.Entity<ShowsGenres>().HasOne(t => t.Show).WithMany(show => show.ShowsGenres).HasForeignKey(t => t.ShowId);
            modelBuilder.Entity<ShowsGenres>().HasOne(t => t.Genre).WithMany(genre => genre.ShowsGenres).HasForeignKey(t => t.GenreId);
        }
    }
}