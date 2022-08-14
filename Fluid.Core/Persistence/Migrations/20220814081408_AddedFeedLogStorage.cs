using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fluid.Core.Persistence.Migrations
{
    public partial class AddedFeedLogStorage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    LogDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedLogStorage", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeedLogStorage");
        }
    }
}
