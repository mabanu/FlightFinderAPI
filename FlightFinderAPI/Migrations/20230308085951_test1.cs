using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightFinderAPI.Migrations
{
    public partial class test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RouteId = table.Column<string>(type: "TEXT", nullable: false),
                    FlightId = table.Column<string>(type: "TEXT", nullable: false),
                    Currency = table.Column<string>(type: "TEXT", nullable: false),
                    PriceToPay = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    BookingId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "BookingId", "Currency", "FlightId", "PriceToPay", "RouteId" },
                values: new object[] { new Guid("e910476f-3439-4728-a0d2-76bf92576740"), "SEK", "6e4b483d", 140.39f, "0e2f3647" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "BookingId", "Name", "Password", "UserName" },
                values: new object[] { new Guid("bf182d5d-8489-48e6-8e87-dd2d90a7fbb6"), new Guid("e910476f-3439-4728-a0d2-76bf92576740"), "maxi", "maxi", "maxi" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_BookingId",
                table: "Users",
                column: "BookingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Bookings");
        }
    }
}
