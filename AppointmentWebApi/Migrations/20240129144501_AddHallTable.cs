using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddHallTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Halls",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FloorNumber = table.Column<int>(type: "int", nullable: false),
                    BuildingId = table.Column<long>(type: "bigint", nullable: false),
                    NoOfChairs = table.Column<int>(type: "int", nullable: false),
                    HasAirConditioner = table.Column<bool>(type: "bit", nullable: false),
                    HasSoundSystem = table.Column<bool>(type: "bit", nullable: false),
                    HasScreen = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Halls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Halls_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Halls_BuildingId",
                table: "Halls",
                column: "BuildingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Halls");
        }
    }
}
