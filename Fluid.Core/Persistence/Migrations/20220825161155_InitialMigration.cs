using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fluid.Core.Persistence.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "FeedLogStorage",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssetTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OemSerialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MachineName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MachineType = table.Column<byte>(type: "tinyint", nullable: false),
                    AssignedPersonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssetLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssetBranch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JsonRaw = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogAttendStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    AttendingTechnicianId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedLogStorage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HardwareChangeLogs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChangeDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssetTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OemSerialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldMachineName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldAssignedPersonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldAssetLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldAssetBranch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MachineName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedPersonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssetLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssetBranch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MachineType = table.Column<byte>(type: "tinyint", nullable: false),
                    OldConfigJsonRaw = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewConfigJsonRaw = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareChangeLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MachineMaster",
                columns: table => new
                {
                    AssetTag = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpdateChangeOnClient = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineMaster", x => x.AssetTag);
                });

            migrationBuilder.CreateTable(
                name: "Technicians",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technicians", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CameraMaster",
                columns: table => new
                {
                    OemSerialNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MegaPixels = table.Column<int>(type: "int", nullable: false),
                    Resolution = table.Column<byte>(type: "tinyint", nullable: false),
                    HasBuiltInMic = table.Column<bool>(type: "bit", nullable: false),
                    IsWireLess = table.Column<bool>(type: "bit", nullable: false),
                    UseStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    MachineId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CameraMaster", x => x.OemSerialNo);
                    table.ForeignKey(
                        name: "FK_CameraMaster_MachineMaster_MachineId",
                        column: x => x.MachineId,
                        principalTable: "MachineMaster",
                        principalColumn: "AssetTag");
                });

            migrationBuilder.CreateTable(
                name: "GraphicsCardMaster",
                columns: table => new
                {
                    OemSerialNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HardwareChange = table.Column<int>(type: "int", nullable: false),
                    UseStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    MachineId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GraphicsCardMaster", x => x.OemSerialNo);
                    table.ForeignKey(
                        name: "FK_GraphicsCardMaster_MachineMaster_MachineId",
                        column: x => x.MachineId,
                        principalTable: "MachineMaster",
                        principalColumn: "AssetTag");
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
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                        principalColumn: "AssetTag");
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
                    UseStatus = table.Column<byte>(type: "tinyint", nullable: false),
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
                        principalColumn: "AssetTag");
                });

            migrationBuilder.CreateTable(
                name: "MonitorMaster",
                columns: table => new
                {
                    OemSerialNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PanelType = table.Column<byte>(type: "tinyint", nullable: false),
                    RefreshRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HasBuiltInSpeakers = table.Column<bool>(type: "bit", nullable: false),
                    HDMIPortCount = table.Column<int>(type: "int", nullable: false),
                    VGAPortCount = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UseStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    MachineId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitorMaster", x => x.OemSerialNo);
                    table.ForeignKey(
                        name: "FK_MonitorMaster_MachineMaster_MachineId",
                        column: x => x.MachineId,
                        principalTable: "MachineMaster",
                        principalColumn: "AssetTag");
                });

            migrationBuilder.CreateTable(
                name: "MotherboardMaster",
                columns: table => new
                {
                    OemSerialNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                        principalColumn: "AssetTag");
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
                    UseStatus = table.Column<byte>(type: "tinyint", nullable: false),
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
                        principalColumn: "AssetTag");
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
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalMemoryMaster", x => x.OemSerialNo);
                    table.ForeignKey(
                        name: "FK_PhysicalMemoryMaster_MachineMaster_MachineId",
                        column: x => x.MachineId,
                        principalTable: "MachineMaster",
                        principalColumn: "AssetTag");
                });

            migrationBuilder.CreateTable(
                name: "ProcessorMaster",
                columns: table => new
                {
                    OemSerialNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProcessorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Architecture = table.Column<byte>(type: "tinyint", nullable: false),
                    Family = table.Column<int>(type: "int", nullable: false),
                    NumberOfCores = table.Column<int>(type: "int", nullable: false),
                    NumberOfLogicalProcessors = table.Column<int>(type: "int", nullable: false),
                    ThreadCount = table.Column<int>(type: "int", nullable: false),
                    MaxClockSpeed = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UseStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    MachineId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessorMaster", x => x.OemSerialNo);
                    table.ForeignKey(
                        name: "FK_ProcessorMaster_MachineMaster_MachineId",
                        column: x => x.MachineId,
                        principalTable: "MachineMaster",
                        principalColumn: "AssetTag");
                });

            migrationBuilder.CreateTable(
                name: "SpeakerMaster",
                columns: table => new
                {
                    OemSerialNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InputPorts = table.Column<byte>(type: "tinyint", nullable: false),
                    IsBlueTooth = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UseStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    MachineId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeakerMaster", x => x.OemSerialNo);
                    table.ForeignKey(
                        name: "FK_SpeakerMaster_MachineMaster_MachineId",
                        column: x => x.MachineId,
                        principalTable: "MachineMaster",
                        principalColumn: "AssetTag");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CameraMaster_MachineId",
                table: "CameraMaster",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_GraphicsCardMaster_MachineId",
                table: "GraphicsCardMaster",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_HardDiskMaster_MachineId",
                table: "HardDiskMaster",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_KeyboardMaster_MachineId",
                table: "KeyboardMaster",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_MonitorMaster_MachineId",
                table: "MonitorMaster",
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

            migrationBuilder.CreateIndex(
                name: "IX_ProcessorMaster_MachineId",
                table: "ProcessorMaster",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_SpeakerMaster_MachineId",
                table: "SpeakerMaster",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "dbo",
                table: "Technicians",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "dbo",
                table: "Technicians",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CameraMaster");

            migrationBuilder.DropTable(
                name: "FeedLogStorage");

            migrationBuilder.DropTable(
                name: "GraphicsCardMaster");

            migrationBuilder.DropTable(
                name: "HardDiskMaster");

            migrationBuilder.DropTable(
                name: "HardwareChangeLogs");

            migrationBuilder.DropTable(
                name: "KeyboardMaster");

            migrationBuilder.DropTable(
                name: "MonitorMaster");

            migrationBuilder.DropTable(
                name: "MotherboardMaster");

            migrationBuilder.DropTable(
                name: "MouseMaster");

            migrationBuilder.DropTable(
                name: "PhysicalMemoryMaster");

            migrationBuilder.DropTable(
                name: "ProcessorMaster");

            migrationBuilder.DropTable(
                name: "SpeakerMaster");

            migrationBuilder.DropTable(
                name: "Technicians",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "MachineMaster");
        }
    }
}
