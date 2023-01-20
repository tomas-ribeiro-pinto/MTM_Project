﻿// <auto-generated />
using System;
using MTM_Holidays.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MTM_Holidays.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230120153201_NewDatabase")]
    partial class NewDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MTM_Holidays.Models.Address", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("County")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("PostCode")
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.Property<string>("Region")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Street")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Town")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("ID");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("MTM_Holidays.Models.CardPayment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityCode")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.HasKey("ID");

                    b.ToTable("CardPayments");
                });

            modelBuilder.Entity("MTM_Holidays.Models.Customer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("AddressID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.HasKey("ID");

                    b.HasIndex("AddressID");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("MTM_Holidays.Models.DiscountCode", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Discount")
                        .HasColumnType("float");

                    b.HasKey("ID");

                    b.ToTable("DiscountCodes");
                });

            modelBuilder.Entity("MTM_Holidays.Models.Holiday", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("AccommodationType")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("DestinationAddressID")
                        .HasColumnType("int");

                    b.Property<int>("OriginAddressID")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("ID");

                    b.HasIndex("DestinationAddressID");

                    b.HasIndex("OriginAddressID");

                    b.ToTable("Holidays");
                });

            modelBuilder.Entity("MTM_Holidays.Models.Order", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int?>("CardPaymentID")
                        .HasColumnType("int");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<int?>("DiscountCodeID")
                        .HasColumnType("int");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("CardPaymentID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("DiscountCodeID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("MTM_Holidays.Models.Order_Holiday", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("HolidayID")
                        .HasColumnType("int");

                    b.Property<int>("Night")
                        .HasColumnType("int");

                    b.Property<int>("OrderID")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("HolidayID");

                    b.HasIndex("OrderID");

                    b.ToTable("Order_Holidays");
                });

            modelBuilder.Entity("MTM_Holidays.Models.Picture", b =>
                {
                    b.Property<int>("PictureID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PictureID"), 1L, 1);

                    b.Property<int>("HolidayID")
                        .HasColumnType("int");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.HasKey("PictureID");

                    b.HasIndex("HolidayID");

                    b.ToTable("Pictures");
                });

            modelBuilder.Entity("MTM_Holidays.Models.Customer", b =>
                {
                    b.HasOne("MTM_Holidays.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("MTM_Holidays.Models.Holiday", b =>
                {
                    b.HasOne("MTM_Holidays.Models.Address", "DestinationAddress")
                        .WithMany()
                        .HasForeignKey("DestinationAddressID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MTM_Holidays.Models.Address", "OriginAddress")
                        .WithMany()
                        .HasForeignKey("OriginAddressID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DestinationAddress");

                    b.Navigation("OriginAddress");
                });

            modelBuilder.Entity("MTM_Holidays.Models.Order", b =>
                {
                    b.HasOne("MTM_Holidays.Models.CardPayment", "CardPayment")
                        .WithMany()
                        .HasForeignKey("CardPaymentID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MTM_Holidays.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MTM_Holidays.Models.DiscountCode", "DiscountCode")
                        .WithMany()
                        .HasForeignKey("DiscountCodeID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("CardPayment");

                    b.Navigation("Customer");

                    b.Navigation("DiscountCode");
                });

            modelBuilder.Entity("MTM_Holidays.Models.Order_Holiday", b =>
                {
                    b.HasOne("MTM_Holidays.Models.Holiday", "Holiday")
                        .WithMany()
                        .HasForeignKey("HolidayID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MTM_Holidays.Models.Order", "Order")
                        .WithMany("Order_Holidays")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Holiday");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("MTM_Holidays.Models.Picture", b =>
                {
                    b.HasOne("MTM_Holidays.Models.Holiday", "Holiday")
                        .WithMany("Pictures")
                        .HasForeignKey("HolidayID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Holiday");
                });

            modelBuilder.Entity("MTM_Holidays.Models.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("MTM_Holidays.Models.Holiday", b =>
                {
                    b.Navigation("Pictures");
                });

            modelBuilder.Entity("MTM_Holidays.Models.Order", b =>
                {
                    b.Navigation("Order_Holidays");
                });
#pragma warning restore 612, 618
        }
    }
}