﻿// <auto-generated />
using System;
using HyggyBackend.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HyggyBackend.DAL.Migrations
{
    [DbContext(typeof(HyggyContext))]
    partial class HyggyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HyggyBackend.DAL.Entities.Address", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HouseNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Latitude")
                        .HasColumnType("float");

                    b.Property<double?>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Addresses");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            City = "Odessa",
                            HouseNumber = "23",
                            Latitude = 48.0,
                            Longitude = 38.0,
                            PostalCode = "6600",
                            State = "Odessa",
                            Street = "Shevchenko str."
                        });
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("DeliveryAddressId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ShopId")
                        .HasColumnType("bigint");

                    b.Property<long>("StatusId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("DeliveryAddressId");

                    b.HasIndex("ShopId");

                    b.HasIndex("StatusId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.OrderItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<long?>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<long?>("PriceHistoryId")
                        .HasColumnType("bigint");

                    b.Property<long?>("WareId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("PriceHistoryId");

                    b.HasIndex("WareId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.OrderStatus", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OrderStatuses");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.Proffession", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Proffessions");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.Shop", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("AddressId")
                        .HasColumnType("bigint");

                    b.Property<string>("PhotoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("StorageId")
                        .HasColumnType("bigint");

                    b.Property<string>("WorkHours")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique()
                        .HasFilter("[AddressId] IS NOT NULL");

                    b.HasIndex("StorageId");

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.Storage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("AddressId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ShopId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique()
                        .HasFilter("[AddressId] IS NOT NULL");

                    b.HasIndex("ShopId");

                    b.ToTable("Storages");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            AddressId = 1L
                        });
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator().HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.Ware", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("Article")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Discount")
                        .HasColumnType("real");

                    b.Property<bool>("IsDeliveryAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<long>("StatusId")
                        .HasColumnType("bigint");

                    b.Property<long>("WareCategory3Id")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.HasIndex("WareCategory3Id");

                    b.ToTable("Wares");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.WareCategory1", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("JSONStructureFilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WareCategories1");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.WareCategory2", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("JSONStructureFilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("WareCategory1Id")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("WareCategory1Id");

                    b.ToTable("WareCategories2");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.WareCategory3", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("JSONStructureFilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("WareCategory2Id")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("WareCategory2Id");

                    b.ToTable("WareCategories3");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.WareImage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("WareId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("WareId");

                    b.ToTable("WareImages");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.WareItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("Quantity")
                        .HasColumnType("bigint");

                    b.Property<long>("StorageId")
                        .HasColumnType("bigint");

                    b.Property<long>("WareId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("StorageId");

                    b.HasIndex("WareId");

                    b.ToTable("WareItems");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.WarePriceHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("EffectiveDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<long>("WareId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("WareId");

                    b.ToTable("WarePriceHistories");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.WareStatus", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WareStatuses");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "9b999352-e0c5-4800-89a7-a4b9f937b47f",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "033e8bdb-9fbb-4509-b279-cce52ddb5e4a",
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.Customer", b =>
                {
                    b.HasBaseType("HyggyBackend.DAL.Entities.User");

                    b.HasDiscriminator().HasValue("Customer");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.Employes.ShopEmployee", b =>
                {
                    b.HasBaseType("HyggyBackend.DAL.Entities.User");

                    b.Property<DateTime>("DateOfBirth")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("datetime2");

                    b.Property<long>("ShopId")
                        .HasColumnType("bigint");

                    b.HasIndex("ShopId");

                    b.HasDiscriminator().HasValue("ShopEmployee");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.Employes.StorageEmployee", b =>
                {
                    b.HasBaseType("HyggyBackend.DAL.Entities.User");

                    b.Property<DateTime>("DateOfBirth")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("datetime2");

                    b.Property<long>("StorageId")
                        .HasColumnType("bigint");

                    b.HasIndex("StorageId");

                    b.HasDiscriminator().HasValue("StorageEmployee");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.Order", b =>
                {
                    b.HasOne("HyggyBackend.DAL.Entities.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId");

                    b.HasOne("HyggyBackend.DAL.Entities.Address", "DeliveryAddress")
                        .WithMany("Orders")
                        .HasForeignKey("DeliveryAddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HyggyBackend.DAL.Entities.Shop", "Shop")
                        .WithMany("Orders")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("HyggyBackend.DAL.Entities.OrderStatus", "Status")
                        .WithMany("Orders")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("DeliveryAddress");

                    b.Navigation("Shop");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.OrderItem", b =>
                {
                    b.HasOne("HyggyBackend.DAL.Entities.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("HyggyBackend.DAL.Entities.WarePriceHistory", "PriceHistory")
                        .WithMany("OrderItems")
                        .HasForeignKey("PriceHistoryId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("HyggyBackend.DAL.Entities.Ware", "Ware")
                        .WithMany("OrderItems")
                        .HasForeignKey("WareId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Order");

                    b.Navigation("PriceHistory");

                    b.Navigation("Ware");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.Shop", b =>
                {
                    b.HasOne("HyggyBackend.DAL.Entities.Address", "Address")
                        .WithOne("Shop")
                        .HasForeignKey("HyggyBackend.DAL.Entities.Shop", "AddressId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("HyggyBackend.DAL.Entities.Storage", "Storage")
                        .WithMany()
                        .HasForeignKey("StorageId");

                    b.Navigation("Address");

                    b.Navigation("Storage");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.Storage", b =>
                {
                    b.HasOne("HyggyBackend.DAL.Entities.Address", "Address")
                        .WithOne("Storage")
                        .HasForeignKey("HyggyBackend.DAL.Entities.Storage", "AddressId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("HyggyBackend.DAL.Entities.Shop", "Shop")
                        .WithMany()
                        .HasForeignKey("ShopId");

                    b.Navigation("Address");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.Ware", b =>
                {
                    b.HasOne("HyggyBackend.DAL.Entities.WareStatus", "Status")
                        .WithMany("Wares")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HyggyBackend.DAL.Entities.WareCategory3", "WareCategory3")
                        .WithMany("Wares")
                        .HasForeignKey("WareCategory3Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");

                    b.Navigation("WareCategory3");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.WareCategory2", b =>
                {
                    b.HasOne("HyggyBackend.DAL.Entities.WareCategory1", "WareCategory1")
                        .WithMany("WaresCategory2")
                        .HasForeignKey("WareCategory1Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WareCategory1");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.WareCategory3", b =>
                {
                    b.HasOne("HyggyBackend.DAL.Entities.WareCategory2", "WareCategory2")
                        .WithMany("WaresCategory3")
                        .HasForeignKey("WareCategory2Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WareCategory2");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.WareImage", b =>
                {
                    b.HasOne("HyggyBackend.DAL.Entities.Ware", "Ware")
                        .WithMany("Images")
                        .HasForeignKey("WareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ware");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.WareItem", b =>
                {
                    b.HasOne("HyggyBackend.DAL.Entities.Storage", "Storage")
                        .WithMany("WareItems")
                        .HasForeignKey("StorageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HyggyBackend.DAL.Entities.Ware", "Ware")
                        .WithMany("WareItems")
                        .HasForeignKey("WareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Storage");

                    b.Navigation("Ware");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.WarePriceHistory", b =>
                {
                    b.HasOne("HyggyBackend.DAL.Entities.Ware", "Ware")
                        .WithMany("PriceHistories")
                        .HasForeignKey("WareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ware");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("HyggyBackend.DAL.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("HyggyBackend.DAL.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HyggyBackend.DAL.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("HyggyBackend.DAL.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.Employes.ShopEmployee", b =>
                {
                    b.HasOne("HyggyBackend.DAL.Entities.Shop", "Shop")
                        .WithMany("ShopEmployees")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.Employes.StorageEmployee", b =>
                {
                    b.HasOne("HyggyBackend.DAL.Entities.Storage", "Storage")
                        .WithMany("StorageEmployees")
                        .HasForeignKey("StorageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Storage");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.Address", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Shop");

                    b.Navigation("Storage");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.OrderStatus", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.Shop", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("ShopEmployees");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.Storage", b =>
                {
                    b.Navigation("StorageEmployees");

                    b.Navigation("WareItems");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.Ware", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("OrderItems");

                    b.Navigation("PriceHistories");

                    b.Navigation("WareItems");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.WareCategory1", b =>
                {
                    b.Navigation("WaresCategory2");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.WareCategory2", b =>
                {
                    b.Navigation("WaresCategory3");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.WareCategory3", b =>
                {
                    b.Navigation("Wares");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.WarePriceHistory", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.WareStatus", b =>
                {
                    b.Navigation("Wares");
                });

            modelBuilder.Entity("HyggyBackend.DAL.Entities.Customer", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
