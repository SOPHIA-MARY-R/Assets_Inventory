using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fluid.Core.Persistence.Migrations
{
    public partial class AddedMonitorMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MonitorMaster",
                columns: table => new
                {
                    OemSerialNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
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

            migrationBuilder.CreateIndex(
                name: "IX_MonitorMaster_MachineId",
                table: "MonitorMaster",
                column: "MachineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonitorMaster");
        }
    }
}
