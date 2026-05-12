using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stallstjarnornas.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class namechangeOpeningDaysToOperatingDay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sittings_OpeningDays_OpeningDaysId",
                table: "Sittings");

            migrationBuilder.RenameColumn(
                name: "OpeningDaysId",
                table: "Sittings",
                newName: "OperatingDayId");

            migrationBuilder.RenameIndex(
                name: "IX_Sittings_OpeningDaysId",
                table: "Sittings",
                newName: "IX_Sittings_OperatingDayId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sittings_OpeningDays_OperatingDayId",
                table: "Sittings",
                column: "OperatingDayId",
                principalTable: "OpeningDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sittings_OpeningDays_OperatingDayId",
                table: "Sittings");

            migrationBuilder.RenameColumn(
                name: "OperatingDayId",
                table: "Sittings",
                newName: "OpeningDaysId");

            migrationBuilder.RenameIndex(
                name: "IX_Sittings_OperatingDayId",
                table: "Sittings",
                newName: "IX_Sittings_OpeningDaysId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sittings_OpeningDays_OpeningDaysId",
                table: "Sittings",
                column: "OpeningDaysId",
                principalTable: "OpeningDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
