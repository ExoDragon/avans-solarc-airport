using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightEventHandler.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flight",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Plane = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Airline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartureDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GateCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlightFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlightTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Seats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flight", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flight");
        }
    }
}
