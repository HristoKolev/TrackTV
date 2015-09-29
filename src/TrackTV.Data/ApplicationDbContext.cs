namespace TrackTV.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using TrackTV.Data.Migrations;
    using TrackTV.Models;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public IDbSet<Episode> Episodes { get; set; }

        public IDbSet<Genre> Genres { get; set; }

        public IDbSet<Network> Networks { get; set; }

        public IDbSet<Show> Shows { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}