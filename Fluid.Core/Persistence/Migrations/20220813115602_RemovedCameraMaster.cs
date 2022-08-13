using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fluid.Core.Persistence.Migrations
{
    public partial class RemovedCameraMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MonitorInfo_MachineMaster_MachineId",
                table: "MonitorInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MonitorInfo",
                table: "MonitorInfo");

            migrationBuilder.RenameTable(
                name: "MonitorInfo",
                newName: "MonitorMaster");

            migrationBuilder.RenameIndex(
                name: "IX_MonitorInfo_MachineId",
                table: "MonitorMaster",
                newName: "IX_MonitorMaster_MachineId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MonitorMaster",
                table: "MonitorMaster",
                column: "OemSerialNo");

            migrationBuilder.AddForeignKey(
                name: "FK_MonitorMaster_MachineMaster_MachineId",
                table: "MonitorMaster",
                column: "MachineId",
                principalTable: "MachineMaster",
                principalColumn: "AssetTag");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MonitorMaster_MachineMaster_MachineId",
                table: "MonitorMaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MonitorMaster",
                table: "MonitorMaster");

            migrationBuilder.RenameTable(
                name: "MonitorMaster",
                newName: "MonitorInfo");

            migrationBuilder.RenameIndex(
                name: "IX_MonitorMaster_MachineId",
                table: "MonitorInfo",
                newName: "IX_MonitorInfo_MachineId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MonitorInfo",
                table: "MonitorInfo",
                column: "OemSerialNo");

            migrationBuilder.AddForeignKey(
                name: "FK_MonitorInfo_MachineMaster_MachineId",
                table: "MonitorInfo",
                column: "MachineId",
                principalTable: "MachineMaster",
                principalColumn: "AssetTag");
        }
    }
}
