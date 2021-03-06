// <auto-generated />
using System;
using DesafioCoodesh.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DesafioCoodesh.Migrations
{
    [DbContext(typeof(ArticleContext))]
    [Migration("20220205144817_InitialMigrate")]
    partial class InitialMigrate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DesafioCoodesh.Models.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Featured")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("longtext");

                    b.Property<string>("NewsSite")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("PublishedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("Sumary")
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.Property<string>("Url")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("DesafioCoodesh.Models.Event", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(95)");

                    b.Property<int?>("ArticleId")
                        .HasColumnType("int");

                    b.Property<string>("Provider")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("DesafioCoodesh.Models.Launch", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(95)");

                    b.Property<int?>("ArticleId")
                        .HasColumnType("int");

                    b.Property<string>("Provider")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.ToTable("Launches");
                });

            modelBuilder.Entity("DesafioCoodesh.Models.Event", b =>
                {
                    b.HasOne("DesafioCoodesh.Models.Article", null)
                        .WithMany("Events")
                        .HasForeignKey("ArticleId");
                });

            modelBuilder.Entity("DesafioCoodesh.Models.Launch", b =>
                {
                    b.HasOne("DesafioCoodesh.Models.Article", null)
                        .WithMany("Launches")
                        .HasForeignKey("ArticleId");
                });

            modelBuilder.Entity("DesafioCoodesh.Models.Article", b =>
                {
                    b.Navigation("Events");

                    b.Navigation("Launches");
                });
#pragma warning restore 612, 618
        }
    }
}
