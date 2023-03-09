﻿// <auto-generated />
using System;
using FlightFinderAPI.Services.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FlightFinderAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.14");

            modelBuilder.Entity("FlightFinderAPI.Domain.Booking", b =>
                {
                    b.Property<Guid>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("TEXT");

                    b.Property<string>("FlightId")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("TEXT");

                    b.Property<float>("PriceToPay")
                        .HasColumnType("REAL");

                    b.Property<string>("RouteId")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("BookingId");

                    b.HasIndex("UserId");

                    b.ToTable("Bookings");

                    b.HasData(
                        new
                        {
                            BookingId = new Guid("e910476f-3439-4728-a0d2-76bf92576740"),
                            BookingDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Currency = "SEK",
                            FlightId = "6e4b483d",
                            PriceToPay = 140.39f,
                            RouteId = "0e2f3647",
                            UserId = new Guid("bf182d5d-8489-48e6-8e87-dd2d90a7fbb6")
                        });
                });

            modelBuilder.Entity("FlightFinderAPI.Domain.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("TEXT");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("bf182d5d-8489-48e6-8e87-dd2d90a7fbb6"),
                            Name = "maxi",
                            Password = "maxi",
                            UserEmail = "maxi@maxi.com"
                        });
                });

            modelBuilder.Entity("FlightFinderAPI.Domain.Booking", b =>
                {
                    b.HasOne("FlightFinderAPI.Domain.User", "User")
                        .WithMany("BookedFlights")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FlightFinderAPI.Domain.User", b =>
                {
                    b.Navigation("BookedFlights");
                });
#pragma warning restore 612, 618
        }
    }
}
