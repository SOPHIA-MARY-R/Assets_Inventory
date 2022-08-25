﻿// <auto-generated />
using System;
using Fluid.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Fluid.Core.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220825112045_AddedPurchaseMaster")]
    partial class AddedPurchaseMaster
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Fluid.Shared.Entities.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

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

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityStamp")
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

                    b.ToTable("Technicians", "dbo");
                });

            modelBuilder.Entity("Fluid.Shared.Entities.CameraInfo", b =>
                {
                    b.Property<string>("OemSerialNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasBuiltInMic")
                        .HasColumnType("bit");

                    b.Property<bool>("IsWireLess")
                        .HasColumnType("bit");

                    b.Property<string>("MachineId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MegaPixels")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<byte>("Resolution")
                        .HasColumnType("tinyint");

                    b.Property<byte>("UseStatus")
                        .HasColumnType("tinyint");

                    b.HasKey("OemSerialNo");

                    b.HasIndex("MachineId");

                    b.ToTable("CameraMaster");
                });

            modelBuilder.Entity("Fluid.Shared.Entities.FeedLog", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AssetBranch")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AssetLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AssetTag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AssignedPersonName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AttendingTechnicianId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JsonRaw")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("LogAttendStatus")
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("LogDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("MachineName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("MachineType")
                        .HasColumnType("tinyint");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OemSerialNo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FeedLogStorage");
                });

            modelBuilder.Entity("Fluid.Shared.Entities.GraphicsCardInfo", b =>
                {
                    b.Property<string>("OemSerialNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MachineId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<byte>("UseStatus")
                        .HasColumnType("tinyint");

                    b.HasKey("OemSerialNo");

                    b.HasIndex("MachineId");

                    b.ToTable("GraphicsCardMaster");
                });

            modelBuilder.Entity("Fluid.Shared.Entities.HardDiskInfo", b =>
                {
                    b.Property<string>("OemSerialNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte>("BusType")
                        .HasColumnType("tinyint");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("HealthCondition")
                        .HasColumnType("tinyint");

                    b.Property<string>("MachineId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("MediaType")
                        .HasColumnType("tinyint");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.Property<byte>("UseStatus")
                        .HasColumnType("tinyint");

                    b.HasKey("OemSerialNo");

                    b.HasIndex("MachineId");

                    b.ToTable("HardDiskMaster");
                });

            modelBuilder.Entity("Fluid.Shared.Entities.HardwareChangeLog", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AssetBranch")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AssetLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AssetTag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AssignedPersonName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ChangeDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("MachineName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("MachineType")
                        .HasColumnType("tinyint");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NewConfigJsonRaw")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OemSerialNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OldAssetBranch")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OldAssetLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OldAssignedPersonName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OldConfigJsonRaw")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OldMachineName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("HardwareChangeLogs");
                });

            modelBuilder.Entity("Fluid.Shared.Entities.KeyboardInfo", b =>
                {
                    b.Property<string>("OemSerialNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsWireless")
                        .HasColumnType("bit");

                    b.Property<string>("MachineId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<byte>("UseStatus")
                        .HasColumnType("tinyint");

                    b.HasKey("OemSerialNo");

                    b.HasIndex("MachineId");

                    b.ToTable("KeyboardMaster");
                });

            modelBuilder.Entity("Fluid.Shared.Entities.MachineInfo", b =>
                {
                    b.Property<string>("AssetTag")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AssetBranch")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AssetLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AssignedPersonName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("InitializationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("MachineName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("MachineType")
                        .HasColumnType("tinyint");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OemSerialNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("UpdateChangeOnClient")
                        .HasColumnType("bit");

                    b.Property<byte>("UseStatus")
                        .HasColumnType("tinyint");

                    b.Property<byte>("UseType")
                        .HasColumnType("tinyint");

                    b.HasKey("AssetTag");

                    b.ToTable("MachineMaster");
                });

            modelBuilder.Entity("Fluid.Shared.Entities.MonitorInfo", b =>
                {
                    b.Property<string>("OemSerialNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HDMIPortCount")
                        .HasColumnType("int");

                    b.Property<bool>("HasBuiltInSpeakers")
                        .HasColumnType("bit");

                    b.Property<string>("MachineId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("PanelType")
                        .HasColumnType("tinyint");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("RefreshRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<byte>("UseStatus")
                        .HasColumnType("tinyint");

                    b.Property<int>("VGAPortCount")
                        .HasColumnType("int");

                    b.Property<string>("Version")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OemSerialNo");

                    b.HasIndex("MachineId");

                    b.ToTable("MonitorMaster");
                });

            modelBuilder.Entity("Fluid.Shared.Entities.MotherboardInfo", b =>
                {
                    b.Property<string>("OemSerialNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MachineId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<byte>("UseStatus")
                        .HasColumnType("tinyint");

                    b.HasKey("OemSerialNo");

                    b.HasIndex("MachineId");

                    b.ToTable("MotherboardMaster");
                });

            modelBuilder.Entity("Fluid.Shared.Entities.MouseInfo", b =>
                {
                    b.Property<string>("OemSerialNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsWireless")
                        .HasColumnType("bit");

                    b.Property<string>("MachineId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<byte>("UseStatus")
                        .HasColumnType("tinyint");

                    b.HasKey("OemSerialNo");

                    b.HasIndex("MachineId");

                    b.ToTable("MouseMaster");
                });

            modelBuilder.Entity("Fluid.Shared.Entities.PhysicalMemoryInfo", b =>
                {
                    b.Property<string>("OemSerialNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("FormFactor")
                        .HasColumnType("tinyint");

                    b.Property<string>("MachineId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("MemoryType")
                        .HasColumnType("tinyint");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("Speed")
                        .HasColumnType("float");

                    b.Property<byte>("UseStatus")
                        .HasColumnType("tinyint");

                    b.HasKey("OemSerialNo");

                    b.HasIndex("MachineId");

                    b.ToTable("PhysicalMemoryMaster");
                });

            modelBuilder.Entity("Fluid.Shared.Entities.ProcessorInfo", b =>
                {
                    b.Property<string>("ProcessorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte>("Architecture")
                        .HasColumnType("tinyint");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Family")
                        .HasColumnType("int");

                    b.Property<string>("MachineId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxClockSpeed")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfCores")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfLogicalProcessors")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ThreadCount")
                        .HasColumnType("int");

                    b.Property<byte>("UseStatus")
                        .HasColumnType("tinyint");

                    b.HasKey("ProcessorId");

                    b.HasIndex("MachineId");

                    b.ToTable("ProcessorMaster");
                });

            modelBuilder.Entity("Fluid.Shared.Entities.PurchaseInfo", b =>
                {
                    b.Property<string>("InvoiceNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BoughtThrough")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Salesman")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TechnicianUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VendorName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InvoiceNo");

                    b.ToTable("PurchaseMaster");
                });

            modelBuilder.Entity("Fluid.Shared.Entities.PurchaseItem", b =>
                {
                    b.Property<string>("HSN")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ItemName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("NetRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PurchaseInfoId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HSN");

                    b.HasIndex("PurchaseInfoId");

                    b.ToTable("PurchaseItem");
                });

            modelBuilder.Entity("Fluid.Shared.Entities.SpeakerInfo", b =>
                {
                    b.Property<string>("OemSerialNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("InputPorts")
                        .HasColumnType("tinyint");

                    b.Property<bool>("IsBlueTooth")
                        .HasColumnType("bit");

                    b.Property<string>("MachineId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<byte>("UseStatus")
                        .HasColumnType("tinyint");

                    b.HasKey("OemSerialNo");

                    b.HasIndex("MachineId");

                    b.ToTable("SpeakerMaster");
                });

            modelBuilder.Entity("Fluid.Shared.Entities.CameraInfo", b =>
                {
                    b.HasOne("Fluid.Shared.Entities.MachineInfo", "Machine")
                        .WithMany()
                        .HasForeignKey("MachineId");

                    b.Navigation("Machine");
                });

            modelBuilder.Entity("Fluid.Shared.Entities.GraphicsCardInfo", b =>
                {
                    b.HasOne("Fluid.Shared.Entities.MachineInfo", "Machine")
                        .WithMany()
                        .HasForeignKey("MachineId");

                    b.Navigation("Machine");
                });

            modelBuilder.Entity("Fluid.Shared.Entities.HardDiskInfo", b =>
                {
                    b.HasOne("Fluid.Shared.Entities.MachineInfo", "Machine")
                        .WithMany()
                        .HasForeignKey("MachineId");

                    b.Navigation("Machine");
                });

            modelBuilder.Entity("Fluid.Shared.Entities.KeyboardInfo", b =>
                {
                    b.HasOne("Fluid.Shared.Entities.MachineInfo", "Machine")
                        .WithMany()
                        .HasForeignKey("MachineId");

                    b.Navigation("Machine");
                });

            modelBuilder.Entity("Fluid.Shared.Entities.MonitorInfo", b =>
                {
                    b.HasOne("Fluid.Shared.Entities.MachineInfo", "Machine")
                        .WithMany()
                        .HasForeignKey("MachineId");

                    b.Navigation("Machine");
                });

            modelBuilder.Entity("Fluid.Shared.Entities.MotherboardInfo", b =>
                {
                    b.HasOne("Fluid.Shared.Entities.MachineInfo", "Machine")
                        .WithMany()
                        .HasForeignKey("MachineId");

                    b.Navigation("Machine");
                });

            modelBuilder.Entity("Fluid.Shared.Entities.MouseInfo", b =>
                {
                    b.HasOne("Fluid.Shared.Entities.MachineInfo", "Machine")
                        .WithMany()
                        .HasForeignKey("MachineId");

                    b.Navigation("Machine");
                });

            modelBuilder.Entity("Fluid.Shared.Entities.PhysicalMemoryInfo", b =>
                {
                    b.HasOne("Fluid.Shared.Entities.MachineInfo", "Machine")
                        .WithMany()
                        .HasForeignKey("MachineId");

                    b.Navigation("Machine");
                });

            modelBuilder.Entity("Fluid.Shared.Entities.ProcessorInfo", b =>
                {
                    b.HasOne("Fluid.Shared.Entities.MachineInfo", "Machine")
                        .WithMany()
                        .HasForeignKey("MachineId");

                    b.Navigation("Machine");
                });

            modelBuilder.Entity("Fluid.Shared.Entities.PurchaseItem", b =>
                {
                    b.HasOne("Fluid.Shared.Entities.PurchaseInfo", "PurchaseInfo")
                        .WithMany("PurchaseItems")
                        .HasForeignKey("PurchaseInfoId");

                    b.Navigation("PurchaseInfo");
                });

            modelBuilder.Entity("Fluid.Shared.Entities.SpeakerInfo", b =>
                {
                    b.HasOne("Fluid.Shared.Entities.MachineInfo", "Machine")
                        .WithMany()
                        .HasForeignKey("MachineId");

                    b.Navigation("Machine");
                });

            modelBuilder.Entity("Fluid.Shared.Entities.PurchaseInfo", b =>
                {
                    b.Navigation("PurchaseItems");
                });
#pragma warning restore 612, 618
        }
    }
}
