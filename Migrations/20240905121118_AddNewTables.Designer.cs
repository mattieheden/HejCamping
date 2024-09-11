﻿// <auto-generated />
using System;
using HejCamping.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HejCamping.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240905121118_AddNewTables")]
    partial class AddNewTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("HejCamping.Domain.Booking", b =>
                {
                    b.Property<string>("OrderNumber")
                        .HasColumnType("TEXT");

                    b.Property<int>("CabinNr")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsCancelled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("TEXT");

                    b.Property<float>("TotalPrice")
                        .HasColumnType("REAL");

                    b.HasKey("OrderNumber");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("HejCamping.Domain.Review", b =>
                {
                    b.Property<string>("OrderNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ReviewDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ReviewText")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("OrderNumber");

                    b.ToTable("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}