﻿// <auto-generated />
using Estimation.DataAccess;
using Estimation.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace Estimation.DataAccess.Migrations
{
    [DbContext(typeof(MaterialDbContext))]
    [Migration("20180216071105_MaterialInitialDb")]
    partial class MaterialInitialDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("Estimation.DataAccess.Models.MainMaterialDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<int>("MaterialType");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("MainMaterials");
                });

            modelBuilder.Entity("Estimation.DataAccess.Models.MaterialDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<decimal>("Fittings");

                    b.Property<decimal>("ListPrice");

                    b.Property<int>("MainMaterialId");

                    b.Property<decimal>("Manpower");

                    b.Property<int>("MaterialType");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<decimal>("NetPrice");

                    b.Property<decimal>("OfferPrice");

                    b.Property<decimal>("Painting");

                    b.Property<string>("Remark");

                    b.Property<decimal>("Supporting");

                    b.HasKey("Id");

                    b.HasIndex("MainMaterialId");

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("Estimation.DataAccess.Models.MaterialDb", b =>
                {
                    b.HasOne("Estimation.DataAccess.Models.MainMaterialDb", "MainMaterial")
                        .WithMany("SubMaterials")
                        .HasForeignKey("MainMaterialId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
