// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using MusicfyAPI.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;

namespace MusicfyAPI.Migrations
{
    [DbContext(typeof(MusicDbContext))]
    [Migration("20180131132516_AddedUserTable")]
    partial class AddedUserTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("MusicfyAPI.Data.Entities.Album", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ArtistId");

                    b.Property<string>("Barcode");

                    b.Property<string>("Country");

                    b.Property<bool>("IsSingle");

                    b.Property<string>("Name");

                    b.Property<int>("ReleaseYear");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("MusicfyAPI.Data.Entities.Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Country");

                    b.Property<bool>("IsGroup");

                    b.Property<string>("Name");

                    b.Property<string>("RealName");

                    b.Property<int>("StartedYear");

                    b.HasKey("Id");

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("MusicfyAPI.Data.Entities.Featuring", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ArtistId");

                    b.Property<int>("SongId");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("SongId");

                    b.ToTable("Featurings");
                });

            modelBuilder.Entity("MusicfyAPI.Data.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("MusicfyAPI.Data.Entities.Lyric", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Language");

                    b.Property<string>("LyricText");

                    b.Property<int>("SongId");

                    b.HasKey("Id");

                    b.HasIndex("SongId");

                    b.ToTable("Lyrics");
                });

            modelBuilder.Entity("MusicfyAPI.Data.Entities.Song", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AlbumId");

                    b.Property<int>("ArtistId");

                    b.Property<int>("GenreId");

                    b.Property<bool>("IsFeaturing");

                    b.Property<string>("Language");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("ArtistId");

                    b.HasIndex("GenreId");

                    b.ToTable("Songs");
                });

            modelBuilder.Entity("MusicfyAPI.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MusicfyAPI.Data.Entities.Album", b =>
                {
                    b.HasOne("MusicfyAPI.Data.Entities.Artist", "Artist")
                        .WithMany("Albums")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MusicfyAPI.Data.Entities.Featuring", b =>
                {
                    b.HasOne("MusicfyAPI.Data.Entities.Artist", "Artist")
                        .WithMany("Featurings")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MusicfyAPI.Data.Entities.Song", "Song")
                        .WithMany("Featurings")
                        .HasForeignKey("SongId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MusicfyAPI.Data.Entities.Lyric", b =>
                {
                    b.HasOne("MusicfyAPI.Data.Entities.Song", "Song")
                        .WithMany("Lyrics")
                        .HasForeignKey("SongId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MusicfyAPI.Data.Entities.Song", b =>
                {
                    b.HasOne("MusicfyAPI.Data.Entities.Album", "Album")
                        .WithMany("Songs")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MusicfyAPI.Data.Entities.Artist", "Artist")
                        .WithMany("Songs")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MusicfyAPI.Data.Entities.Genre", "Genre")
                        .WithMany("Songs")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
