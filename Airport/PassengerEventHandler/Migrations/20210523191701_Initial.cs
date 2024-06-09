using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PassengerEventHandler.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Passenger",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phonenumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passenger", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlightId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Airline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartureDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FlightFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlightTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GateCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeatNr = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassengerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassengerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassengerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassengerPhonenumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Passenger");

            migrationBuilder.DropTable(
                name: "Ticket");
        }
    }
}
