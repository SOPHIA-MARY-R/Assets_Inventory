using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fluid.Core.Persistence.Migrations
{
    public partial class RenameToPurchaseItemMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurchaseMaster",
                columns: table => new
                {
                    InvoiceNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VendorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salesman = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoughtThrough = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TechnicianUserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseMaster", x => x.InvoiceNo);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseItemMaster",
                columns: table => new
                {
                    HSN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    NetRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PurchaseInfoId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseItemMaster", x => x.HSN);
                    table.ForeignKey(
                        name: "FK_PurchaseItemMaster_PurchaseMaster_PurchaseInfoId",
                        column: x => x.PurchaseInfoId,
                        principalTable: "PurchaseMaster",
                        principalColumn: "InvoiceNo");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItemMaster_PurchaseInfoId",
                table: "PurchaseItemMaster",
                column: "PurchaseInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseItemMaster");

            migrationBuilder.DropTable(
                name: "PurchaseMaster");
        }
    }
}
