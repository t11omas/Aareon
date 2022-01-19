﻿// <auto-generated />
using AareonTechnicalTest;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AareonTechnicalTest.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220118180719_SeedPeople")]
    partial class SeedPeople
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("AareonTechnicalTest.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Forename")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Surname")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Persons");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Forename = "System",
                            IsAdmin = true,
                            Surname = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Forename = "Test",
                            IsAdmin = false,
                            Surname = "User"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
