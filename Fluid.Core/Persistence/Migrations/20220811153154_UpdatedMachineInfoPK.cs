using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fluid.Core.Persistence.Migrations
{
    public partial class UpdatedMachineInfoPK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OemServiceTag",
                table: "MachineMaster",
                newName: "AssetTag");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AssetTag",
                table: "MachineMaster",
                newName: "OemServiceTag");
        }
    }
}
