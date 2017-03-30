namespace TrackTv.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using TrackTv.Data.Models;

    public class TrackTvDbContext : IdentityDbContext<User>
    {
        public TrackTvDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Actor> Actors { get; set; }

        public DbSet<Episode> Episodes { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Network> Networks { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<Show> Shows { get; set; }

        public DbSet<ShowsActors> ShowsActors { get; set; }

        public DbSet<ShowsGenres> ShowsGenres { get; set; }

        public DbSet<ShowsProfiles> ShowsProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            ConfigureManyToManyRelationships(builder);

            ConfigureProperties(builder);
        }

        private static void ConfigureManyToManyRelationships(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasKey(role => new { role.UserId, role.RoleId });
            builder.Entity<IdentityUserToken<string>>().HasKey(token => new { token.LoginProvider, token.Name, token.UserId });
            builder.Entity<IdentityUserLogin<string>>().HasKey(login => new { login.LoginProvider, login.ProviderKey });

            builder.Entity<ShowsProfiles>().HasKey(t => new { t.ProfileId, t.ShowId });
            builder.Entity<ShowsActors>().HasKey(t => new { t.ShowId, t.ActorId });
            builder.Entity<ShowsGenres>().HasKey(t => new { t.ShowId, t.GenreId });
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

            builder.Entity<Profile>().Property(user => user.Username).IsRequired();
        }
    }
}