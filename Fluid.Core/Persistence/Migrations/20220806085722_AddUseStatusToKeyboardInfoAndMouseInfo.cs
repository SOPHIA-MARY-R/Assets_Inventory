using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fluid.Core.Persistence.Migrations
{
    public partial class AddUseStatusToKeyboardInfoAndMouseInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "UseStatus",
                table: "MouseMaster",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "UseStatus",
                table: "KeyboardMaster",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UseStatus",
                table: "MouseMaster");

            migrationBuilder.DropColumn(
                name: "UseStatus",
                table: "KeyboardMaster");
        }
    }
}
