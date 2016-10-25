namespace TrackTV.Data
{
    using System;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Models;
    using TrackTv.Models.Joint;

    public class TrackTvDbContext : DbContext
    {
        public TrackTvDbContext(DbContextOptions options)
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

        public DbSet<Show> Shows { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.;Database=TrackTvDb;Trusted_Connection=True;MultipleActiveResultSets=True;");
                Console.WriteLine("Using hardcoded configuration!!!");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            ConfigureManyToManyRelationships(builder);

            ConfigureProperties(builder);
        }

        private static void ConfigureProperties(ModelBuilder builder)
        {
            const int ImdbIdSize = 10;

            builder.Entity<Show>().Property(t => t.Name).HasMaxLength(byte.MaxValue).IsRequired();
            builder.Entity<Show>().Property(t => t.Banner).HasMaxLength(byte.MaxValue);
            builder.Entity<Show>().Property(t => t.ImdbId).HasMaxLength(ImdbIdSize);

            builder.Entity<Episode>().Property(t => t.Title).HasMaxLength(byte.MaxValue).IsRequired();
            builder.Entity<Episode>().Property(t => t.ImdbId).HasMaxLength(ImdbIdSize);

            builder.Entity<Genre>().Property(t => t.Name).HasMaxLength(100).IsRequired();

            builder.Entity<Network>().Property(t => t.Name).HasMaxLength(40).IsRequired();

            builder.Entity<Actor>().Property(t => t.Name).HasMaxLength(byte.MaxValue).IsRequired();
            builder.Entity<Actor>().Property(t => t.Image).HasMaxLength(byte.MaxValue);

            builder.Entity<ShowsActors>().Property(t => t.Role).HasMaxLength(byte.MaxValue);

            builder.Entity<User>().Property(user => user.Username).IsRequired();
        }

        private static void ConfigureManyToManyRelationships(ModelBuilder builder)
        {
            // ShowsUsers
            builder.Entity<ShowsUsers>().HasKey(t => new
                   {
                       t.UserId,
                       t.ShowId
                   });
            builder.Entity<ShowsUsers>().HasOne(t => t.User).WithMany(user => user.ShowsUsers).HasForeignKey(t => t.UserId);
            builder.Entity<ShowsUsers>().HasOne(t => t.Show).WithMany(show => show.ShowsUsers).HasForeignKey(t => t.ShowId);

            // ShowsActors
            builder.Entity<ShowsActors>().HasKey(t => new
                   {
                       t.ShowId,
                       t.ActorId
                   });
            builder.Entity<ShowsActors>().HasOne(t => t.Show).WithMany(show => show.ShowsActors).HasForeignKey(t => t.ShowId);
            builder.Entity<ShowsActors>().HasOne(t => t.Actor).WithMany(actor => actor.ShowsActors).HasForeignKey(t => t.ActorId);

            // ShowsGenres
            builder.Entity<ShowsGenres>().HasKey(t => new
                   {
                       t.ShowId,
                       t.GenreId
                   });
            builder.Entity<ShowsGenres>().HasOne(t => t.Show).WithMany(show => show.ShowsGenres).HasForeignKey(t => t.ShowId);
            builder.Entity<ShowsGenres>().HasOne(t => t.Genre).WithMany(genre => genre.ShowsGenres).HasForeignKey(t => t.GenreId);
        }
    }
}