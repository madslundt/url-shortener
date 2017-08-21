﻿// <auto-generated />
using dataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace datamodel.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("datamodel.Models.Url", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("RedirectUrl");

                    b.Property<string>("ShortId");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("RedirectUrl")
                        .IsUnique();

                    b.HasIndex("ShortId")
                        .IsUnique();

                    b.ToTable("Urls");
                });

            modelBuilder.Entity("datamodel.Models.UrlError", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("ErrorMessage");

                    b.Property<int>("StatusCode");

                    b.HasKey("Id");

                    b.ToTable("UrlErrors");
                });

            modelBuilder.Entity("datamodel.Models.UrlHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Redirected")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("UrlId");

                    b.HasKey("Id");

                    b.HasIndex("UrlId");

                    b.ToTable("UrlRedirects");
                });

            modelBuilder.Entity("datamodel.Models.UrlHistory", b =>
                {
                    b.HasOne("datamodel.Models.Url", "Url")
                        .WithMany("UrlHistories")
                        .HasForeignKey("UrlId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
