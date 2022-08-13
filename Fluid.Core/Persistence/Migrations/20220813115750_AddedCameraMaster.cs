using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fluid.Core.Persistence.Migrations
{
    public partial class AddedCameraMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_CameraMaster_MachineId",
                table: "CameraMaster",
                column: "MachineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CameraMaster");
        }
    }
}
