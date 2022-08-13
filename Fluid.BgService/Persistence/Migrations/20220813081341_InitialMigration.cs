using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fluid.BgService.Persistence.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MachineMasters",
                columns: table => new
                {
                    AssetTag = table.Column<string>(type: "TEXT", nullable: false),
                    OemSerialNo = table.Column<string>(type: "TEXT", nullable: true),
                    MachineName = table.Column<string>(type: "TEXT", nullable: true),
                    Model = table.Column<string>(type: "TEXT", nullable: true),
                    Manufacturer = table.Column<string>(type: "TEXT", nullable: true),
                    MachineType = table.Column<byte>(type: "INTEGER", nullable: false),
                    UseType = table.Column<byte>(type: "INTEGER", nullable: false),
                    UseStatus = table.Column<byte>(type: "INTEGER", nullable: false),
                    AssignedPersonName = table.Column<string>(type: "TEXT", nullable: true),
                    AssetLocation = table.Column<string>(type: "TEXT", nullable: true),
                    AssetBranch = table.Column<string>(type: "TEXT", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    InitializationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineMasters", x => x.AssetTag);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MachineMasters");
        }
    }
}
