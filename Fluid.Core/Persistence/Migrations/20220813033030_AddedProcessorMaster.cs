using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fluid.Core.Persistence.Migrations
{
    public partial class AddedProcessorMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcessorMaster",
                columns: table => new
                {
                    ProcessorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    table.PrimaryKey("PK_ProcessorMaster", x => x.ProcessorId);
                    table.ForeignKey(
                        name: "FK_ProcessorMaster_MachineMaster_MachineId",
                        column: x => x.MachineId,
                        principalTable: "MachineMaster",
                        principalColumn: "OemServiceTag");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessorMaster_MachineId",
                table: "ProcessorMaster",
                column: "MachineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessorMaster");
        }
    }
}
