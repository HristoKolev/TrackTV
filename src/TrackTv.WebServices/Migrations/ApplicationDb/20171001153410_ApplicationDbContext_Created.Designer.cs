using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TrackTv.WebServices.Infrastructure;
using TrackTv.Data.Models.Enums;

namespace TrackTv.WebServices.Migrations.ApplicationDb
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20171001153410_ApplicationDbContext_Created")]
    partial class ApplicationDbContext_Created
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TrackTv.Data.Models.Actor", b =>
                {
                    b.Property<int>("ActorId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActorImage")
                        .HasMaxLength(255);

                    b.Property<string>("ActorName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("LastUpdated");

                    b.Property<int>("TheTvDbId");

                    b.HasKey("ActorId");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("TrackTv.Data.Models.Episode", b =>
                {
                    b.Property<int>("EpisodeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EpisodeDescription");

                    b.Property<int>("EpisodeNumber");

                    b.Property<string>("EpisodeTitle")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("FirstAired");

                    b.Property<string>("ImdbId")
                        .HasMaxLength(10);

                    b.Property<DateTime>("LastUpdated");

                    b.Property<int>("SeasonNumber");

                    b.Property<int>("ShowId");

                    b.Property<int>("TheTvDbId");

                    b.HasKey("EpisodeId");

                    b.HasIndex("ShowId");

                    b.ToTable("Episodes");
                });

            modelBuilder.Entity("TrackTv.Data.Models.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GenreName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("GenreId");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("TrackTv.Data.Models.Network", b =>
                {
                    b.Property<int>("NetworkId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("NetworkName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("NetworkId");

                    b.ToTable("Networks");
                });

            modelBuilder.Entity("TrackTv.Data.Models.Profile", b =>
                {
                    b.Property<int>("ProfileId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("ProfileId");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("TrackTv.Data.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActorId");

                    b.Property<string>("RoleName")
                        .HasMaxLength(255);

                    b.Property<int>("ShowId");

                    b.HasKey("RoleId");

                    b.HasIndex("ActorId");

                    b.HasIndex("ShowId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("TrackTv.Data.Models.Show", b =>
                {
                    b.Property<int>("ShowId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AirDay");

                    b.Property<DateTime?>("AirTime");

                    b.Property<DateTime?>("FirstAired");

                    b.Property<string>("ImdbId")
                        .HasMaxLength(10);

                    b.Property<DateTime>("LastUpdated");

                    b.Property<int>("NetworkId");

                    b.Property<string>("ShowBanner")
                        .HasMaxLength(255);

                    b.Property<string>("ShowDescription");

                    b.Property<string>("ShowName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("ShowStatus");

                    b.Property<int>("TheTvDbId");

                    b.HasKey("ShowId");

                    b.HasIndex("NetworkId");

                    b.ToTable("Shows");
                });

            modelBuilder.Entity("TrackTv.Data.Models.ShowsGenres", b =>
                {
                    b.Property<int>("ShowId");

                    b.Property<int>("GenreId");

                    b.HasKey("ShowId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("ShowsGenres");
                });

            modelBuilder.Entity("TrackTv.Data.Models.Subscription", b =>
                {
                    b.Property<int>("SubscriptionId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ProfileId");

                    b.Property<int>("ShowId");

                    b.HasKey("SubscriptionId");

                    b.HasIndex("ProfileId");

                    b.HasIndex("ShowId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("TrackTv.Data.Models.Episode", b =>
                {
                    b.HasOne("TrackTv.Data.Models.Show", "Show")
                        .WithMany("Episodes")
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TrackTv.Data.Models.Role", b =>
                {
                    b.HasOne("TrackTv.Data.Models.Actor", "Actor")
                        .WithMany("Roles")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TrackTv.Data.Models.Show", "Show")
                        .WithMany("Roles")
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TrackTv.Data.Models.Show", b =>
                {
                    b.HasOne("TrackTv.Data.Models.Network", "Network")
                        .WithMany("Shows")
                        .HasForeignKey("NetworkId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TrackTv.Data.Models.ShowsGenres", b =>
                {
                    b.HasOne("TrackTv.Data.Models.Genre", "Genre")
                        .WithMany("ShowsGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TrackTv.Data.Models.Show", "Show")
                        .WithMany("ShowsGenres")
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TrackTv.Data.Models.Subscription", b =>
                {
                    b.HasOne("TrackTv.Data.Models.Profile", "Profile")
                        .WithMany("Subscriptions")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TrackTv.Data.Models.Show", "Show")
                        .WithMany("Subscriptions")
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
