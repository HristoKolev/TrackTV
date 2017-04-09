namespace TrackTv.WebServices.Infrastructure
{
    using System;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    using TrackTv.Data;

    public class ApplicationDbContext : TrackTvDbContext
    {
        public ApplicationDbContext()
        {
            Console.WriteLine();
        }

        /// <summary>
        /// The name of the connection string in the application config file.
        /// </summary>
        private const string ConnectionName = "DefaultConnection";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Global.AppConfig.GetConnectionString(ConnectionName));
            }
        }
    }
}