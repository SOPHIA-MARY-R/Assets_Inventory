﻿// <auto-generated />
using System;
using Fluid.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Fluid.Core.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Fluid.Shared.Entities.HardDiskInfo", b =>
                {
                    b.Property<string>("OemSerialNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte>("BusType")
                        .HasColumnType("tinyint");

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

                    b.HasKey("OemSerialNo");

                    b.HasIndex("MachineId");

                    b.ToTable("KeyboardMaster");
                });

            modelBuilder.Entity("Fluid.Shared.Entities.MachineInfo", b =>
                {
                    b.Property<string>("OemServiceTag")
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

                    b.Property<byte>("UseStatus")
                        .HasColumnType("tinyint");

                    b.Property<byte>("UseType")
                        .HasColumnType("tinyint");

                    b.HasKey("OemServiceTag");

                    b.ToTable("MachineMaster");
                });

            modelBuilder.Entity("Fluid.Shared.Entities.MotherboardInfo", b =>
                {
                    b.Property<string>("OemSerialNo")
                        .HasColumnType("nvarchar(450)");

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
#pragma warning restore 612, 618
        }
    }
}
