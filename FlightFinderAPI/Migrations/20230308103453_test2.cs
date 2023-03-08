using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightFinderAPI.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Bookings_BookingId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_BookingId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Bookings",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "BookingId",
                keyValue: new Guid("e910476f-3439-4728-a0d2-76bf92576740"),
                column: "UserId",
                value: new Guid("bf182d5d-8489-48e6-8e87-dd2d90a7fbb6"));

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Users_UserId",
                table: "Bookings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Users_UserId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Bookings");

            migrationBuilder.AddColumn<Guid>(
                name: "BookingId",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("bf182d5d-8489-48e6-8e87-dd2d90a7fbb6"),
                column: "BookingId",
                value: new Guid("e910476f-3439-4728-a0d2-76bf92576740"));

            migrationBuilder.CreateIndex(
                name: "IX_Users_BookingId",
                table: "Users",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Bookings_BookingId",
                table: "Users",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
