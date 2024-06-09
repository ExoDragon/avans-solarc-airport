using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZoneManagementApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lease",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CurrentVersion = table.Column<int>(type: "int", nullable: false)
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
                    CurrentVersion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zone", x => x.Id);
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
                    LeaseAggregateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ZoneAggregateId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_Lease_LeaseAggregateId",
                        column: x => x.LeaseAggregateId,
                        principalTable: "Lease",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Event_Zone_ZoneAggregateId",
                        column: x => x.ZoneAggregateId,
                        principalTable: "Zone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_LeaseAggregateId",
                table: "Event",
                column: "LeaseAggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_ZoneAggregateId",
                table: "Event",
                column: "ZoneAggregateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Lease");

            migrationBuilder.DropTable(
                name: "Zone");
        }
    }
}
