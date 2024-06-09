using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GateManagementApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gate",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CurrentVersion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CurrentVersion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
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
                    GateAggregateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TicketAggregateId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_Gate_GateAggregateId",
                        column: x => x.GateAggregateId,
                        principalTable: "Gate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Event_Ticket_TicketAggregateId",
                        column: x => x.TicketAggregateId,
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_GateAggregateId",
                table: "Event",
                column: "GateAggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_TicketAggregateId",
                table: "Event",
                column: "TicketAggregateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Gate");

            migrationBuilder.DropTable(
                name: "Ticket");
        }
    }
}
