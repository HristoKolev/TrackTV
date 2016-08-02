namespace TrackTV.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using TrackTV.Models;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : this("DefaultConnection")
        {
        }

        public ApplicationDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString, false)
        {
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