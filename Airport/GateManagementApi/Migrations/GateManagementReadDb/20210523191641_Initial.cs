using Microsoft.EntityFrameworkCore.Migrations;

namespace GateManagementApi.Migrations.GateManagementReadDb
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GateId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GateCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "Gate");

            migrationBuilder.DropTable(
                name: "Ticket");
        }
    }
}
