namespace TrackTv.WebServices.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    using TrackTv.Data;

    public class ApplicationDbContext : TrackTvDbContext
    {
        /// <summary>
        /// The name of the connection string in the application config file.
        /// </summary>
        private const string ConnectionName = "DefaultConnection";

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.SelectProvider(Global.AppConfig.GetConnectionString(ConnectionName));
            }
        }
    }

    public class User
    {
        public bool IsAdmin { get; set; }

        public string Password { get; set; }

        public int ProfileId { get; set; }

        public int UserId { get; set; }

        public string Username { get; set; }
    }
}