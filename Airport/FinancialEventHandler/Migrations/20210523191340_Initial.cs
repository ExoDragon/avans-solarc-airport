using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancialEventHandler.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZoneName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZoneRentalPrice = table.Column<float>(type: "real", nullable: false),
                    BillingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lease",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZoneName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lease", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zone",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zone", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "Lease");

            migrationBuilder.DropTable(
                name: "Zone");
        }
    }
}
