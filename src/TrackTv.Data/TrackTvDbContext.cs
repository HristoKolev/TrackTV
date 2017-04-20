﻿namespace TrackTv.Data
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
            ConfigureManyToManyRelationships(builder);

            ConfigureProperties(builder);
        }

        private static void ConfigureManyToManyRelationships(ModelBuilder builder)
        {
            builder.Entity<ShowsGenres>()
                   .HasKey(t => new
                   {
                       t.ShowId,
                       t.GenreId
                   });
        }

        private static void ConfigureProperties(ModelBuilder builder)
        {
            const int ImdbIdSize = 10;

            builder.Entity<Show>().Property(t => t.Name).HasMaxLength(byte.MaxValue).IsRequired();
            builder.Entity<Show>().Property(t => t.Banner).HasMaxLength(byte.MaxValue);
            builder.Entity<Show>().Property(t => t.ImdbId).HasMaxLength(ImdbIdSize);

            builder.Entity<Episode>().Property(t => t.Title).HasMaxLength(byte.MaxValue);
            builder.Entity<Episode>().Property(t => t.ImdbId).HasMaxLength(ImdbIdSize);

            builder.Entity<Genre>().Property(t => t.Name).HasMaxLength(byte.MaxValue).IsRequired();

            builder.Entity<Network>().Property(t => t.Name).HasMaxLength(byte.MaxValue).IsRequired();

            builder.Entity<Actor>().Property(t => t.Name).HasMaxLength(byte.MaxValue).IsRequired();
            builder.Entity<Actor>().Property(t => t.Image).HasMaxLength(byte.MaxValue);

            builder.Entity<Role>().Property(t => t.RoleName).HasMaxLength(byte.MaxValue);

            builder.Entity<Profile>().Property(user => user.Username).IsRequired();
        }
    }
}