﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Net14Web.DbStuff.RealEstate;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Net14Web.MigrationsRealEstate
{
    [DbContext(typeof(WebDbRealEstateContext))]
    [Migration("20240129111222_ApartamentApartmentOwnerLink")]
    partial class ApartamentApartmentOwnerLink
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Net14Web.DbStuff.RealEstate.Models.Apartament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("ApartmentOwnerId")
                        .HasColumnType("integer");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentOwnerId");

                    b.ToTable("Apartaments");
                });

            modelBuilder.Entity("Net14Web.DbStuff.RealEstate.Models.ApartmentOwner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<string>("HaveBuilding")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("KindOfActivity")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ApartmentOwners");
                });

            modelBuilder.Entity("Net14Web.DbStuff.RealEstate.Models.Apartament", b =>
                {
                    b.HasOne("Net14Web.DbStuff.RealEstate.Models.ApartmentOwner", "ApartmentOwner")
                        .WithMany("Apartaments")
                        .HasForeignKey("ApartmentOwnerId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("ApartmentOwner");
                });

            modelBuilder.Entity("Net14Web.DbStuff.RealEstate.Models.ApartmentOwner", b =>
                {
                    b.Navigation("Apartaments");
                });
#pragma warning restore 612, 618
        }
    }
}
