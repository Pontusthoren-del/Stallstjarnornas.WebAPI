using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Stallstjarnornas.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class moredata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                column: "SittingId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BookingDate", "NoOfGuests", "SittingId", "Status" },
                values: new object[] { new DateTime(2026, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 1, "Confirmed" });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "BookingDate", "NoOfGuests", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 1 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "BookingDate", "NoOfGuests", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 1 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "BookingDate", "NoOfGuests", "SittingId", "Status" },
                values: new object[] { new DateTime(2026, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 1, "Confirmed" });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "BookingDate", "NoOfGuests", "SittingId", "Status" },
                values: new object[] { new DateTime(2026, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 1, "Confirmed" });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "BookingDate", "NoOfGuests", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 5 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 7 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 7 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 7 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 8 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 29,
                column: "SittingId",
                value: 8);

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "BookingDate", "BookingNumber", "CreatedDate", "GuestId", "Message", "NoOfGuests", "SittingId", "Status" },
                values: new object[,]
                {
                    { 31, new DateTime(2026, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1031, new DateTime(2026, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, 4, 1, "Confirmed" },
                    { 32, new DateTime(2026, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1032, new DateTime(2026, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Glutenfri", 6, 1, "Confirmed" },
                    { 33, new DateTime(2026, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1033, new DateTime(2026, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null, 2, 2, "Pending" },
                    { 34, new DateTime(2026, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1034, new DateTime(2026, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, null, 8, 2, "Confirmed" },
                    { 35, new DateTime(2026, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 1035, new DateTime(2026, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, null, 3, 3, "Confirmed" },
                    { 36, new DateTime(2026, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 1036, new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, null, 5, 3, "Cancelled" },
                    { 37, new DateTime(2026, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 1037, new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, null, 2, 4, "Confirmed" },
                    { 38, new DateTime(2026, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 1038, new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "Skaldjursallergi", 4, 4, "Confirmed" },
                    { 39, new DateTime(2026, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1039, new DateTime(2026, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, null, 6, 9, "Confirmed" },
                    { 40, new DateTime(2026, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1040, new DateTime(2026, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, null, 4, 9, "Confirmed" },
                    { 41, new DateTime(2026, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1041, new DateTime(2026, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, "Vegansk", 8, 10, "Confirmed" },
                    { 42, new DateTime(2026, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1042, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, null, 2, 10, "Pending" },
                    { 43, new DateTime(2026, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1043, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, null, 5, 11, "Confirmed" },
                    { 44, new DateTime(2026, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1044, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, "Äggallergi", 3, 11, "Confirmed" },
                    { 45, new DateTime(2026, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1045, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, null, 7, 12, "Confirmed" },
                    { 46, new DateTime(2026, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1046, new DateTime(2026, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, null, 8, 11, "Confirmed" },
                    { 47, new DateTime(2026, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1047, new DateTime(2026, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 17, null, 8, 11, "Confirmed" },
                    { 48, new DateTime(2026, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1048, new DateTime(2026, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, "Sojaöverkänslighet", 8, 11, "Confirmed" },
                    { 49, new DateTime(2026, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1049, new DateTime(2026, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, null, 8, 11, "Confirmed" },
                    { 50, new DateTime(2026, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1050, new DateTime(2026, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, null, 8, 11, "Confirmed" },
                    { 51, new DateTime(2026, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1051, new DateTime(2026, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 21, "Celiaki", 6, 11, "Confirmed" },
                    { 52, new DateTime(2026, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1052, new DateTime(2026, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 22, null, 4, 11, "Confirmed" },
                    { 53, new DateTime(2026, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1053, new DateTime(2026, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 23, null, 3, 12, "Confirmed" },
                    { 54, new DateTime(2026, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1054, new DateTime(2026, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 24, "Laktosintolerant", 5, 12, "Pending" },
                    { 55, new DateTime(2026, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 1055, new DateTime(2026, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 25, null, 2, 9, "Confirmed" },
                    { 56, new DateTime(2026, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 1056, new DateTime(2026, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 26, null, 4, 9, "Cancelled" },
                    { 57, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1057, new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 27, null, 3, 1, "Confirmed" },
                    { 58, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1058, new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 28, "Nötallergi", 5, 1, "Confirmed" },
                    { 59, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1059, new DateTime(2026, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 29, null, 2, 2, "Pending" },
                    { 60, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1060, new DateTime(2026, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, null, 4, 2, "Confirmed" },
                    { 61, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1061, new DateTime(2026, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, 6, 5, "Confirmed" },
                    { 62, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1062, new DateTime(2026, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Glutenfri", 2, 6, "Confirmed" },
                    { 63, new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1063, new DateTime(2026, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null, 8, 7, "Confirmed" },
                    { 64, new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1064, new DateTime(2026, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, null, 4, 8, "Cancelled" },
                    { 65, new DateTime(2026, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1065, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Vegansk", 3, 9, "Confirmed" },
                    { 66, new DateTime(2026, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1066, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, null, 5, 10, "Confirmed" },
                    { 67, new DateTime(2026, 7, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1067, new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, null, 4, 11, "Confirmed" },
                    { 68, new DateTime(2026, 7, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1068, new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "Skaldjursallergi", 6, 12, "Confirmed" },
                    { 69, new DateTime(2026, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 1069, new DateTime(2026, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, null, 2, 1, "Pending" },
                    { 70, new DateTime(2026, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 1070, new DateTime(2026, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, null, 8, 2, "Confirmed" },
                    { 71, new DateTime(2026, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1071, new DateTime(2026, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, "Glutenintolerant", 5, 3, "Confirmed" },
                    { 72, new DateTime(2026, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1072, new DateTime(2026, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, null, 3, 4, "Cancelled" },
                    { 73, new DateTime(2026, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1073, new DateTime(2026, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, null, 4, 5, "Confirmed" },
                    { 74, new DateTime(2026, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1074, new DateTime(2026, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, "Äggallergi", 2, 6, "Confirmed" },
                    { 75, new DateTime(2026, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1075, new DateTime(2026, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, null, 6, 7, "Confirmed" },
                    { 76, new DateTime(2026, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1076, new DateTime(2026, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, null, 4, 8, "Pending" },
                    { 77, new DateTime(2026, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1077, new DateTime(2026, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 17, "Vegansk", 8, 9, "Confirmed" },
                    { 78, new DateTime(2026, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1078, new DateTime(2026, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, "Julbord, extra speciellt!", 6, 10, "Confirmed" },
                    { 79, new DateTime(2026, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1079, new DateTime(2026, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, null, 4, 11, "Confirmed" },
                    { 80, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1080, new DateTime(2026, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, "Nyårsmiddag!", 8, 12, "Confirmed" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                column: "SittingId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BookingDate", "NoOfGuests", "SittingId", "Status" },
                values: new object[] { new DateTime(2026, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, "Pending" });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "BookingDate", "NoOfGuests", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 4 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "BookingDate", "NoOfGuests", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 5 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "BookingDate", "NoOfGuests", "SittingId", "Status" },
                values: new object[] { new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 6, "Cancelled" });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 7 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "BookingDate", "NoOfGuests", "SittingId", "Status" },
                values: new object[] { new DateTime(2026, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 8, "Pending" });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 9 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 10 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 11 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 12 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 7 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "BookingDate", "NoOfGuests", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 8 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 9 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 10 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 11 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 12 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "BookingDate", "SittingId" },
                values: new object[] { new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 29,
                column: "SittingId",
                value: 7);
        }
    }
}
