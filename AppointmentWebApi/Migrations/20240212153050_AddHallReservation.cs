using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddHallReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReservationDays",
                table: "Halls",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReservationFromTime",
                table: "Halls",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ReservationToTime",
                table: "Halls",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationDays",
                table: "Halls");

            migrationBuilder.DropColumn(
                name: "ReservationFromTime",
                table: "Halls");

            migrationBuilder.DropColumn(
                name: "ReservationToTime",
                table: "Halls");
        }
    }
}
