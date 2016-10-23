namespace TrackTv
{
    using Microsoft.EntityFrameworkCore;

    using TrackTv.Models;
    using TrackTv.Models.Joint;

    public class TrackTvDbContext : DbContext
    {
        public TrackTvDbContext(DbContextOptions options)
            : base(options)
        {
        }

        // public TrackTvDbContext()
        // {
        // }
        public DbSet<Actor> Actors { get; set; }

        public DbSet<Episode> Episodes { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Network> Networks { get; set; }

        public DbSet<Show> Shows { get; set; }

        public DbSet<User> Users { get; set; }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        // optionsBuilder.UseSqlServer(@"Server=.;Database=TrackTvDb;Trusted_Connection=True;MultipleActiveResultSets=True;");

        // base.OnConfiguring(optionsBuilder);
        // }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            this.ConfigureManyToManyRelationships(builder);

            this.ConfigureRequiredProperties(builder);
        }

        private void ConfigureManyToManyRelationships(ModelBuilder builder)
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

        private void ConfigureRequiredProperties(ModelBuilder builder)
        {
            builder.Entity<Actor>().Property(actor => actor.Name).IsRequired();
            builder.Entity<Genre>().Property(genre => genre.Name).IsRequired();
            builder.Entity<Network>().Property(network => network.Name).IsRequired();
            builder.Entity<Show>().Property(show => show.Name).IsRequired();
            builder.Entity<User>().Property(user => user.Username).IsRequired();
        }
    }
}