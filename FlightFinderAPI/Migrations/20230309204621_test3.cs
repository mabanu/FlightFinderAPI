using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightFinderAPI.Migrations
{
    public partial class test3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Users",
                newName: "UserEmail");

            migrationBuilder.AddColumn<DateTime>(
                name: "BookingDate",
                table: "Bookings",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("bf182d5d-8489-48e6-8e87-dd2d90a7fbb6"),
                column: "UserEmail",
                value: "maxi@maxi.com");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingDate",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "Users",
                newName: "UserName");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("bf182d5d-8489-48e6-8e87-dd2d90a7fbb6"),
                column: "UserName",
                value: "maxi");
        }
    }
}
