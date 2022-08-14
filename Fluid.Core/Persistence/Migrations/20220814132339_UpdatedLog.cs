using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fluid.Core.Persistence.Migrations
{
    public partial class UpdatedLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AttendingTechnicianId",
                table: "FeedLogStorage",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "LogAttendStatus",
                table: "FeedLogStorage",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttendingTechnicianId",
                table: "FeedLogStorage");

            migrationBuilder.DropColumn(
                name: "LogAttendStatus",
                table: "FeedLogStorage");
        }
    }
}
