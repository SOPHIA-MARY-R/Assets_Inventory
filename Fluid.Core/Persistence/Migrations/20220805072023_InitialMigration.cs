using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fluid.Core.Persistence.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MachineMaster",
                columns: table => new
                {
                    OemServiceTag = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OemSerialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MachineName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MachineType = table.Column<byte>(type: "tinyint", nullable: false),
                    UseType = table.Column<byte>(type: "tinyint", nullable: false),
                    UseStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    AssignedPersonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssetLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssetBranch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InitializationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineMaster", x => x.OemServiceTag);
                });

            migrationBuilder.CreateTable(
                name: "HardDiskMaster",
                columns: table => new
                {
                    OemSerialNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UseStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    MediaType = table.Column<byte>(type: "tinyint", nullable: false),
                    BusType = table.Column<byte>(type: "tinyint", nullable: false),
                    HealthCondition = table.Column<byte>(type: "tinyint", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    MachineId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardDiskMaster", x => x.OemSerialNo);
                    table.ForeignKey(
                        name: "FK_HardDiskMaster_MachineMaster_MachineId",
                        column: x => x.MachineId,
                        principalTable: "MachineMaster",
                        principalColumn: "OemServiceTag");
                });

            migrationBuilder.CreateTable(
                name: "KeyboardMaster",
                columns: table => new
                {
                    OemSerialNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsWireless = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MachineId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyboardMaster", x => x.OemSerialNo);
                    table.ForeignKey(
                        name: "FK_KeyboardMaster_MachineMaster_MachineId",
                        column: x => x.MachineId,
                        principalTable: "MachineMaster",
                        principalColumn: "OemServiceTag");
                });

            migrationBuilder.CreateTable(
                name: "MotherboardMaster",
                columns: table => new
                {
                    OemSerialNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UseStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    MachineId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotherboardMaster", x => x.OemSerialNo);
                    table.ForeignKey(
                        name: "FK_MotherboardMaster_MachineMaster_MachineId",
                        column: x => x.MachineId,
                        principalTable: "MachineMaster",
                        principalColumn: "OemServiceTag");
                });

            migrationBuilder.CreateTable(
                name: "MouseMaster",
                columns: table => new
                {
                    OemSerialNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsWireless = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MachineId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MouseMaster", x => x.OemSerialNo);
                    table.ForeignKey(
                        name: "FK_MouseMaster_MachineMaster_MachineId",
                        column: x => x.MachineId,
                        principalTable: "MachineMaster",
                        principalColumn: "OemServiceTag");
                });

            migrationBuilder.CreateTable(
                name: "PhysicalMemoryMaster",
                columns: table => new
                {
                    OemSerialNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UseStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Speed = table.Column<double>(type: "float", nullable: false),
                    MemoryType = table.Column<byte>(type: "tinyint", nullable: false),
                    FormFactor = table.Column<byte>(type: "tinyint", nullable: false),
                    MachineId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalMemoryMaster", x => x.OemSerialNo);
                    table.ForeignKey(
                        name: "FK_PhysicalMemoryMaster_MachineMaster_MachineId",
                        column: x => x.MachineId,
                        principalTable: "MachineMaster",
                        principalColumn: "OemServiceTag");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HardDiskMaster_MachineId",
                table: "HardDiskMaster",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_KeyboardMaster_MachineId",
                table: "KeyboardMaster",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_MotherboardMaster_MachineId",
                table: "MotherboardMaster",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_MouseMaster_MachineId",
                table: "MouseMaster",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalMemoryMaster_MachineId",
                table: "PhysicalMemoryMaster",
                column: "MachineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HardDiskMaster");

            migrationBuilder.DropTable(
                name: "KeyboardMaster");

            migrationBuilder.DropTable(
                name: "MotherboardMaster");

            migrationBuilder.DropTable(
                name: "MouseMaster");

            migrationBuilder.DropTable(
                name: "PhysicalMemoryMaster");

            migrationBuilder.DropTable(
                name: "MachineMaster");
        }
    }
}
