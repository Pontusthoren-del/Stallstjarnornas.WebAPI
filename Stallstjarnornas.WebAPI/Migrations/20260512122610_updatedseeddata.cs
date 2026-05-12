using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Stallstjarnornas.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatedseeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DropColumn(
                name: "Message",
                table: "Guests");

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Message",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                column: "Message",
                value: "Allergisk mot gluten");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 3,
                column: "Message",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 4,
                column: "Message",
                value: "Laktosintolerant");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 5,
                column: "Message",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 6,
                column: "Message",
                value: "Nötallergi, var noga!");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 7,
                column: "Message",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 8,
                column: "Message",
                value: "Allergisk mot skaldjur");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 9,
                column: "Message",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 10,
                column: "Message",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 11,
                column: "Message",
                value: "Glutenintolerant och laktosintolerant");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 12,
                column: "Message",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 13,
                column: "Message",
                value: "Äggallergi");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 14,
                column: "Message",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 15,
                column: "Message",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 16,
                column: "Message",
                value: "Nötallergi");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 17,
                column: "Message",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 18,
                column: "Message",
                value: "Sojaöverkänslighet");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 19,
                column: "Message",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 20,
                column: "Message",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 21,
                column: "Message",
                value: "Celiaki, strikt glutenfri mat krävs");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 22,
                column: "Message",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 23,
                column: "Message",
                value: "Laktosintolerant");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 24,
                column: "Message",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 25,
                column: "Message",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 26,
                column: "Message",
                value: "Skaldjursallergi");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 27,
                column: "Message",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 28,
                column: "Message",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 29,
                column: "Message",
                value: "Ägg- och nötallergi");

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 30,
                column: "Message",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "Bookings");

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Guests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "BookingDate", "BookingNumber", "CreatedDate", "GuestId", "NoOfGuests", "SittingId", "Status" },
                values: new object[,]
                {
                    { 31, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1031, new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, 9, "Confirmed" },
                    { 32, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1032, new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, 10, "Confirmed" },
                    { 33, new DateTime(2026, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1033, new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 4, 11, "Cancelled" },
                    { 34, new DateTime(2026, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1034, new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 2, 12, "Confirmed" },
                    { 35, new DateTime(2026, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1035, new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 6, 3, "Confirmed" },
                    { 36, new DateTime(2026, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1036, new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, 3, 4, "Pending" },
                    { 37, new DateTime(2026, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1037, new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, 2, 7, "Confirmed" },
                    { 38, new DateTime(2026, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1038, new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, 5, 8, "Confirmed" },
                    { 39, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1039, new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 17, 4, 9, "Confirmed" },
                    { 40, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1040, new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, 3, 10, "Cancelled" }
                });

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 1,
                column: "Message",
                value: null);

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 2,
                column: "Message",
                value: "Allergisk mot gluten");

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 3,
                column: "Message",
                value: null);

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 4,
                column: "Message",
                value: "Laktosintolerant");

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 5,
                column: "Message",
                value: null);

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 6,
                column: "Message",
                value: "Nötallergi, var noga!");

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 7,
                column: "Message",
                value: null);

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 8,
                column: "Message",
                value: "Allergisk mot skaldjur");

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 9,
                column: "Message",
                value: null);

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 10,
                column: "Message",
                value: null);

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 11,
                column: "Message",
                value: "Glutenintolerant och laktosintolerant");

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 12,
                column: "Message",
                value: null);

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 13,
                column: "Message",
                value: "Äggallergi");

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 14,
                column: "Message",
                value: null);

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 15,
                column: "Message",
                value: null);

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 16,
                column: "Message",
                value: "Nötallergi");

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 17,
                column: "Message",
                value: null);

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 18,
                column: "Message",
                value: "Sojaöverkänslighet");

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 19,
                column: "Message",
                value: null);

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 20,
                column: "Message",
                value: null);

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 21,
                column: "Message",
                value: "Cöliaki, strikt glutenfri mat krävs");

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 22,
                column: "Message",
                value: null);

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 23,
                column: "Message",
                value: "Laktosintolerant");

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 24,
                column: "Message",
                value: null);

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 25,
                column: "Message",
                value: null);

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 26,
                column: "Message",
                value: "Skaldjursallergi");

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 27,
                column: "Message",
                value: null);

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 28,
                column: "Message",
                value: null);

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 29,
                column: "Message",
                value: "Ägg- och nötallergi");

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "Id",
                keyValue: 30,
                column: "Message",
                value: null);
        }
    }
}
