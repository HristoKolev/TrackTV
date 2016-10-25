using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TrackTV.Data;

namespace TrackTV.Data.Migrations
{
    [DbContext(typeof(TrackTvDbContext))]
    partial class TrackTvDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TrackTv.Models.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Image")
                        .HasAnnotation("MaxLength", 255);

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.Property<int>("TheTvDbId");

                    b.HasKey("Id");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("TrackTv.Models.Episode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<DateTime?>("FirstAired");

                    b.Property<string>("ImdbId")
                        .HasAnnotation("MaxLength", 10);

                    b.Property<DateTime>("LastUpdated");

                    b.Property<int>("Number");

                    b.Property<int>("SeasonNumber");

                    b.Property<int>("ShowId");

                    b.Property<int>("TheTvDbId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.HasKey("Id");

                    b.HasIndex("ShowId");

                    b.ToTable("Episodes");
                });

            modelBuilder.Entity("TrackTv.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("TrackTv.Models.Joint.ShowsActors", b =>
                {
                    b.Property<int>("ShowId");

                    b.Property<int>("ActorId");

                    b.Property<string>("Role")
                        .HasAnnotation("MaxLength", 255);

                    b.HasKey("ShowId", "ActorId");

                    b.HasIndex("ActorId");

                    b.HasIndex("ShowId");

                    b.ToTable("ShowsActors");
                });

            modelBuilder.Entity("TrackTv.Models.Joint.ShowsGenres", b =>
                {
                    b.Property<int>("ShowId");

                    b.Property<int>("GenreId");

                    b.HasKey("ShowId", "GenreId");

                    b.HasIndex("GenreId");

                    b.HasIndex("ShowId");

                    b.ToTable("ShowsGenres");
                });

            modelBuilder.Entity("TrackTv.Models.Joint.ShowsUsers", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("ShowId");

                    b.HasKey("UserId", "ShowId");

                    b.HasIndex("ShowId");

                    b.HasIndex("UserId");

                    b.ToTable("ShowsUsers");
                });

            modelBuilder.Entity("TrackTv.Models.Network", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 40);

                    b.HasKey("Id");

                    b.ToTable("Networks");
                });

            modelBuilder.Entity("TrackTv.Models.Show", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AirDay");

                    b.Property<DateTime?>("AirTime");

                    b.Property<string>("Banner")
                        .HasAnnotation("MaxLength", 255);

                    b.Property<string>("Description");

                    b.Property<DateTime?>("FirstAired");

                    b.Property<string>("ImdbId")
                        .HasAnnotation("MaxLength", 10);

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.Property<int>("NetworkId");

                    b.Property<int>("Status");

                    b.Property<int>("TheTvDbId");

                    b.HasKey("Id");

                    b.HasIndex("NetworkId");

                    b.ToTable("Shows");
                });

            modelBuilder.Entity("TrackTv.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TrackTv.Models.Episode", b =>
                {
                    b.HasOne("TrackTv.Models.Show", "Show")
                        .WithMany("Episodes")
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TrackTv.Models.Joint.ShowsActors", b =>
                {
                    b.HasOne("TrackTv.Models.Actor", "Actor")
                        .WithMany("ShowsActors")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TrackTv.Models.Show", "Show")
                        .WithMany("ShowsActors")
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TrackTv.Models.Joint.ShowsGenres", b =>
                {
                    b.HasOne("TrackTv.Models.Genre", "Genre")
                        .WithMany("ShowsGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TrackTv.Models.Show", "Show")
                        .WithMany("ShowsGenres")
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TrackTv.Models.Joint.ShowsUsers", b =>
                {
                    b.HasOne("TrackTv.Models.Show", "Show")
                        .WithMany("ShowsUsers")
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TrackTv.Models.User", "User")
                        .WithMany("ShowsUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TrackTv.Models.Show", b =>
                {
                    b.HasOne("TrackTv.Models.Network", "Network")
                        .WithMany("Shows")
                        .HasForeignKey("NetworkId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
