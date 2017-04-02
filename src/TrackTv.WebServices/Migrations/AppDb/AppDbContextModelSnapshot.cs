using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TrackTv.WebServices.Infrastructure;
using TrackTv.Data.Models.Enums;

namespace TrackTv.WebServices.Migrations.AppDb
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TrackTv.Data.Models.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Image")
                        .HasMaxLength(255);

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("TheTvDbId");

                    b.HasKey("Id");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("TrackTv.Data.Models.Episode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<DateTime?>("FirstAired");

                    b.Property<string>("ImdbId")
                        .HasMaxLength(10);

                    b.Property<DateTime>("LastUpdated");

                    b.Property<int>("Number");

                    b.Property<int>("SeasonNumber");

                    b.Property<int>("ShowId");

                    b.Property<int>("TheTvDbId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("ShowId");

                    b.ToTable("Episodes");
                });

            modelBuilder.Entity("TrackTv.Data.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("TrackTv.Data.Models.Network", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.HasKey("Id");

                    b.ToTable("Networks");
                });

            modelBuilder.Entity("TrackTv.Data.Models.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("UserId");

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("TrackTv.Data.Models.Show", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AirDay");

                    b.Property<DateTime?>("AirTime");

                    b.Property<string>("Banner")
                        .HasMaxLength(255);

                    b.Property<string>("Description");

                    b.Property<DateTime?>("FirstAired");

                    b.Property<string>("ImdbId")
                        .HasMaxLength(10);

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("NetworkId");

                    b.Property<int>("Status");

                    b.Property<int>("TheTvDbId");

                    b.HasKey("Id");

                    b.HasIndex("NetworkId");

                    b.ToTable("Shows");
                });

            modelBuilder.Entity("TrackTv.Data.Models.ShowsActors", b =>
                {
                    b.Property<int>("ShowId");

                    b.Property<int>("ActorId");

                    b.Property<string>("Role")
                        .HasMaxLength(255);

                    b.HasKey("ShowId", "ActorId");

                    b.HasIndex("ActorId");

                    b.ToTable("ShowsActors");
                });

            modelBuilder.Entity("TrackTv.Data.Models.ShowsGenres", b =>
                {
                    b.Property<int>("ShowId");

                    b.Property<int>("GenreId");

                    b.HasKey("ShowId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("ShowsGenres");
                });

            modelBuilder.Entity("TrackTv.Data.Models.ShowsProfiles", b =>
                {
                    b.Property<int>("ProfileId");

                    b.Property<int>("ShowId");

                    b.HasKey("ProfileId", "ShowId");

                    b.HasIndex("ShowId");

                    b.ToTable("ShowsProfiles");
                });

            modelBuilder.Entity("TrackTv.Data.Models.Episode", b =>
                {
                    b.HasOne("TrackTv.Data.Models.Show", "Show")
                        .WithMany("Episodes")
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

            modelBuilder.Entity("TrackTv.Data.Models.ShowsActors", b =>
                {
                    b.HasOne("TrackTv.Data.Models.Actor", "Actor")
                        .WithMany("ShowsActors")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TrackTv.Data.Models.Show", "Show")
                        .WithMany("ShowsActors")
                        .HasForeignKey("ShowId")
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

            modelBuilder.Entity("TrackTv.Data.Models.ShowsProfiles", b =>
                {
                    b.HasOne("TrackTv.Data.Models.Profile", "Profile")
                        .WithMany("ShowsUsers")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TrackTv.Data.Models.Show", "Show")
                        .WithMany("ShowsUsers")
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
