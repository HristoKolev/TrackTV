using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TrackTv;

namespace TrackTv.Migrations
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

                    b.Property<string>("Image");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("TvDbId");

                    b.HasKey("Id");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("TrackTv.Models.Episode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<DateTime?>("FirstAired");

                    b.Property<string>("ImdbId");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<int>("Number");

                    b.Property<int>("SeasonNumber");

                    b.Property<int>("ShowId");

                    b.Property<string>("Title");

                    b.Property<int>("TvDbId");

                    b.HasKey("Id");

                    b.HasIndex("ShowId");

                    b.ToTable("Episodes");
                });

            modelBuilder.Entity("TrackTv.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("TrackTv.Models.Joint.ShowsActors", b =>
                {
                    b.Property<int>("ShowId");

                    b.Property<int>("ActorId");

                    b.Property<string>("Role");

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
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Networks");
                });

            modelBuilder.Entity("TrackTv.Models.Show", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AirDay");

                    b.Property<DateTime?>("AirTime");

                    b.Property<string>("Banner");

                    b.Property<string>("Description");

                    b.Property<DateTime?>("FirstAired");

                    b.Property<string>("ImdbId");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("NetworkId");

                    b.Property<int>("Status");

                    b.Property<int>("TvDbId");

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
