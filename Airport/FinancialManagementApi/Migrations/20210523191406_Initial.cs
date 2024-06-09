using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancialManagementApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CurrentVersion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MessageType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventData = table.Column<string>(type: "text", nullable: true),
                    InvoiceAggregateId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_Invoice_InvoiceAggregateId",
                        column: x => x.InvoiceAggregateId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_InvoiceAggregateId",
                table: "Event",
                column: "InvoiceAggregateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Invoice");
        }
    }
}
