using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fluid.Core.Persistence.Migrations
{
    public partial class ModifiedColumnsForMonitorMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HDMIPortCount",
                table: "MonitorMaster",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasBuiltInSpeakers",
                table: "MonitorMaster",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte>(
                name: "PanelType",
                table: "MonitorMaster",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<decimal>(
                name: "RefreshRate",
                table: "MonitorMaster",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "VGAPortCount",
                table: "MonitorMaster",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HDMIPortCount",
                table: "MonitorMaster");

            migrationBuilder.DropColumn(
                name: "HasBuiltInSpeakers",
                table: "MonitorMaster");

            migrationBuilder.DropColumn(
                name: "PanelType",
                table: "MonitorMaster");

            migrationBuilder.DropColumn(
                name: "RefreshRate",
                table: "MonitorMaster");

            migrationBuilder.DropColumn(
                name: "VGAPortCount",
                table: "MonitorMaster");
        }
    }
}
