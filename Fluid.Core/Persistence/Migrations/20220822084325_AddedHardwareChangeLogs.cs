using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fluid.Core.Persistence.Migrations
{
    public partial class AddedHardwareChangeLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "UpdateChangeOnClient",
                table: "MachineMaster",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
                    NewConfigJsonRaw = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareChangeLogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HardwareChangeLogs");

            migrationBuilder.DropColumn(
                name: "UpdateChangeOnClient",
                table: "MachineMaster");
        }
    }
}
