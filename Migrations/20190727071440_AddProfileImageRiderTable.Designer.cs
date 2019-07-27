﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eatklik.Models;

namespace eatklik.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20190727071440_AddProfileImageRiderTable")]
    partial class AddProfileImageRiderTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("eatklik.Models.City", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImagePath");

                    b.Property<string>("Name");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("eatklik.Models.CouponCode", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<long>("Discount");

                    b.Property<decimal>("PctDiscount");

                    b.Property<int>("Status");

                    b.Property<DateTime>("ValidTill");

                    b.HasKey("Id");

                    b.ToTable("CouponCodes");
                });

            modelBuilder.Entity("eatklik.Models.Cuisine", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("ImagePath");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Cuisines");
                });

            modelBuilder.Entity("eatklik.Models.Customer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<long>("CityId");

                    b.Property<string>("Email");

                    b.Property<string>("ImagePath");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("eatklik.Models.Menu", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImagePath");

                    b.Property<string>("Name");

                    b.Property<long>("RestaurantId");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("eatklik.Models.MenuItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImagePath");

                    b.Property<long>("MenuId");

                    b.Property<string>("Name");

                    b.Property<long>("Price");

                    b.Property<int>("Size");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.ToTable("MenuItems");
                });

            modelBuilder.Entity("eatklik.Models.Promotion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CityId");

                    b.Property<string>("Content");

                    b.Property<string>("Name");

                    b.Property<int>("PromoType");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Promotions");
                });

            modelBuilder.Entity("eatklik.Models.Restaurant", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CoverImagePath");

                    b.Property<string>("Description");

                    b.Property<string>("LogoImagePath");

                    b.Property<long>("MaxOrderPrice");

                    b.Property<long>("MinOrderPrice");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("eatklik.Models.RestaurantContact", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("MobileNo");

                    b.Property<long>("RestaurantId");

                    b.Property<string>("URL");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("RestaurantContacts");
                });

            modelBuilder.Entity("eatklik.Models.RestaurantCuisine", b =>
                {
                    b.Property<long>("RestaurantId");

                    b.Property<long>("CuisineId");

                    b.HasKey("RestaurantId", "CuisineId");

                    b.HasIndex("CuisineId");

                    b.ToTable("RestaurantCuisine");
                });

            modelBuilder.Entity("eatklik.Models.RestaurantLocation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<long>("CityId");

                    b.Property<long>("Latitude");

                    b.Property<long>("Longitude");

                    b.Property<long>("RestaurantId");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("RestaurantLocations");
                });

            modelBuilder.Entity("eatklik.Models.RestaurantTiming", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EndTime");

                    b.Property<long>("RestaurantId");

                    b.Property<string>("StartTime");

                    b.Property<int>("WeekDay");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("RestaurantTimings");
                });

            modelBuilder.Entity("eatklik.Models.Review", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment");

                    b.Property<long>("CustomerId");

                    b.Property<decimal>("Rating");

                    b.Property<long>("RestaurantId");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("eatklik.Models.Rider", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CityId");

                    b.Property<string>("MobileNo");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("ProfileImage");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Riders");
                });

            modelBuilder.Entity("eatklik.Models.Setting", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("eatklik.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("MobileNumber");

                    b.Property<string>("Password");

                    b.Property<string>("ProfileImage");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("eatklik.Models.Customer", b =>
                {
                    b.HasOne("eatklik.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eatklik.Models.Menu", b =>
                {
                    b.HasOne("eatklik.Models.Restaurant", "Restaurant")
                        .WithMany("RestaurantMenus")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eatklik.Models.MenuItem", b =>
                {
                    b.HasOne("eatklik.Models.Menu", "Menu")
                        .WithMany("MenuItems")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eatklik.Models.Promotion", b =>
                {
                    b.HasOne("eatklik.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eatklik.Models.RestaurantContact", b =>
                {
                    b.HasOne("eatklik.Models.Restaurant", "Restaurant")
                        .WithMany("RestaurantContacts")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eatklik.Models.RestaurantCuisine", b =>
                {
                    b.HasOne("eatklik.Models.Cuisine", "Cuisine")
                        .WithMany("RestaurantCuisines")
                        .HasForeignKey("CuisineId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("eatklik.Models.Restaurant", "Restaurant")
                        .WithMany("RestaurantCuisines")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eatklik.Models.RestaurantLocation", b =>
                {
                    b.HasOne("eatklik.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("eatklik.Models.Restaurant", "Restaurant")
                        .WithMany("RestaurantLocations")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eatklik.Models.RestaurantTiming", b =>
                {
                    b.HasOne("eatklik.Models.Restaurant", "Restaurant")
                        .WithMany("RestaurantTimings")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eatklik.Models.Review", b =>
                {
                    b.HasOne("eatklik.Models.Customer", "Customer")
                        .WithMany("RestaurantReviews")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("eatklik.Models.Restaurant", "Restaurant")
                        .WithMany("CustomrReviews")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eatklik.Models.Rider", b =>
                {
                    b.HasOne("eatklik.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
