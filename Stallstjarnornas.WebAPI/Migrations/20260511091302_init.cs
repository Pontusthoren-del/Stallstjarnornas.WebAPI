using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Stallstjarnornas.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpeningDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<int>(type: "int", nullable: false),
                    Opens = table.Column<TimeOnly>(type: "time", nullable: false),
                    Closes = table.Column<TimeOnly>(type: "time", nullable: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpeningDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sittings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpeningDaysId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    MaxGuests = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sittings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sittings_OpeningDays_OpeningDaysId",
                        column: x => x.OpeningDaysId,
                        principalTable: "OpeningDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuestId = table.Column<int>(type: "int", nullable: false),
                    SittingId = table.Column<int>(type: "int", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoOfGuests = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookingNumber = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Guests_GuestId",
                        column: x => x.GuestId,
                        principalTable: "Guests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Sittings_SittingId",
                        column: x => x.SittingId,
                        principalTable: "Sittings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MailLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    SentTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MailLogs_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Guests",
                columns: new[] { "Id", "Email", "Message", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "anna.lindqvist@gmail.com", null, "Anna Lindqvist", "070-123 45 67" },
                    { 2, "erik.johansson@gmail.com", "Allergisk mot gluten", "Erik Johansson", "073-234 56 78" },
                    { 3, "maria.svensson@hotmail.com", null, "Maria Svensson", "076-345 67 89" },
                    { 4, "oskar.bergstrom@outlook.com", "Laktosintolerant", "Oskar Bergström", "072-456 78 90" },
                    { 5, "lina.karlsson@gmail.com", null, "Lina Karlsson", "070-567 89 01" },
                    { 6, "johan.nilsson@gmail.com", "Nötallergi, var noga!", "Johan Nilsson", "073-678 90 12" },
                    { 7, "sara.eriksson@hotmail.com", null, "Sara Eriksson", "076-789 01 23" },
                    { 8, "mikael.larsson@outlook.com", "Allergisk mot skaldjur", "Mikael Larsson", "072-890 12 34" },
                    { 9, "emma.olsson@gmail.com", null, "Emma Olsson", "070-901 23 45" },
                    { 10, "andreas.persson@gmail.com", null, "Andreas Persson", "073-012 34 56" },
                    { 11, "karin.andersson@hotmail.com", "Glutenintolerant och laktosintolerant", "Karin Andersson", "076-123 45 67" },
                    { 12, "peter.gustafsson@outlook.com", null, "Peter Gustafsson", "072-234 56 78" },
                    { 13, "sofia.magnusson@gmail.com", "Äggallergi", "Sofia Magnusson", "070-345 67 89" },
                    { 14, "magnus.lindstrom@gmail.com", null, "Magnus Lindström", "073-456 78 90" },
                    { 15, "hanna.jakobsson@hotmail.com", null, "Hanna Jakobsson", "076-567 89 01" },
                    { 16, "daniel.petersson@outlook.com", "Nötallergi", "Daniel Petersson", "072-678 90 12" },
                    { 17, "maja.henriksson@gmail.com", null, "Maja Henriksson", "070-789 01 23" },
                    { 18, "jonas.sandberg@gmail.com", "Sojaöverkänslighet", "Jonas Sandberg", "073-890 12 34" },
                    { 19, "elin.sjoberg@hotmail.com", null, "Elin Sjöberg", "076-901 23 45" },
                    { 20, "viktor.lundgren@outlook.com", null, "Viktor Lundgren", "072-012 34 56" },
                    { 21, "therese.holm@gmail.com", "Cöliaki, strikt glutenfri mat krävs", "Therese Holm", "070-111 22 33" },
                    { 22, "rickard.bjork@hotmail.com", null, "Rickard Björk", "073-222 33 44" },
                    { 23, "camilla.strand@gmail.com", "Laktosintolerant", "Camilla Strand", "076-333 44 55" },
                    { 24, "fredrik.holm@outlook.com", null, "Fredrik Holm", "072-444 55 66" },
                    { 25, "isabella.nystrom@gmail.com", null, "Isabella Nyström", "070-555 66 77" },
                    { 26, "tobias.engstrom@gmail.com", "Skaldjursallergi", "Tobias Engström", "073-666 77 88" },
                    { 27, "matilda.forsberg@hotmail.com", null, "Matilda Forsberg", "076-777 88 99" },
                    { 28, "simon.aberg@outlook.com", null, "Simon Åberg", "072-888 99 00" },
                    { 29, "johanna.blom@gmail.com", "Ägg- och nötallergi", "Johanna Blom", "070-999 00 11" },
                    { 30, "alexander.vang@gmail.com", null, "Alexander Vång", "073-000 11 22" }
                });

            migrationBuilder.InsertData(
                table: "OpeningDays",
                columns: new[] { "Id", "Closes", "Day", "IsClosed", "Opens" },
                values: new object[,]
                {
                    { 1, new TimeOnly(22, 0, 0), 1, false, new TimeOnly(17, 0, 0) },
                    { 2, new TimeOnly(22, 0, 0), 2, false, new TimeOnly(17, 0, 0) },
                    { 3, new TimeOnly(22, 0, 0), 3, false, new TimeOnly(17, 0, 0) },
                    { 4, new TimeOnly(22, 0, 0), 4, false, new TimeOnly(17, 0, 0) },
                    { 5, new TimeOnly(22, 0, 0), 5, false, new TimeOnly(17, 0, 0) },
                    { 6, new TimeOnly(22, 0, 0), 6, false, new TimeOnly(17, 0, 0) },
                    { 7, new TimeOnly(22, 0, 0), 0, true, new TimeOnly(17, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Sittings",
                columns: new[] { "Id", "EndTime", "MaxGuests", "OpeningDaysId", "StartTime" },
                values: new object[,]
                {
                    { 1, new TimeOnly(19, 0, 0), 50, 1, new TimeOnly(17, 0, 0) },
                    { 2, new TimeOnly(21, 0, 0), 50, 1, new TimeOnly(19, 0, 0) },
                    { 3, new TimeOnly(19, 0, 0), 50, 2, new TimeOnly(17, 0, 0) },
                    { 4, new TimeOnly(21, 0, 0), 50, 2, new TimeOnly(19, 0, 0) },
                    { 5, new TimeOnly(19, 0, 0), 50, 3, new TimeOnly(17, 0, 0) },
                    { 6, new TimeOnly(21, 0, 0), 50, 3, new TimeOnly(19, 0, 0) },
                    { 7, new TimeOnly(19, 0, 0), 50, 4, new TimeOnly(17, 0, 0) },
                    { 8, new TimeOnly(21, 0, 0), 50, 4, new TimeOnly(19, 0, 0) },
                    { 9, new TimeOnly(19, 0, 0), 50, 5, new TimeOnly(17, 0, 0) },
                    { 10, new TimeOnly(21, 0, 0), 50, 5, new TimeOnly(19, 0, 0) },
                    { 11, new TimeOnly(19, 0, 0), 50, 6, new TimeOnly(17, 0, 0) },
                    { 12, new TimeOnly(21, 0, 0), 50, 6, new TimeOnly(19, 0, 0) },
                    { 13, new TimeOnly(19, 0, 0), 50, 7, new TimeOnly(17, 0, 0) },
                    { 14, new TimeOnly(21, 0, 0), 50, 7, new TimeOnly(19, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "BookingDate", "BookingNumber", "CreatedDate", "GuestId", "NoOfGuests", "SittingId", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1001, new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, 1, "Confirmed" },
                    { 2, new DateTime(2026, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1002, new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 4, 2, "Confirmed" },
                    { 3, new DateTime(2026, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1003, new DateTime(2026, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, 3, "Pending" },
                    { 4, new DateTime(2026, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1004, new DateTime(2026, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 6, 4, "Confirmed" },
                    { 5, new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 1005, new DateTime(2026, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 2, 5, "Confirmed" },
                    { 6, new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 1006, new DateTime(2026, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 5, 6, "Cancelled" },
                    { 7, new DateTime(2026, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1007, new DateTime(2026, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 8, 7, "Confirmed" },
                    { 8, new DateTime(2026, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1008, new DateTime(2026, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 2, 8, "Pending" },
                    { 9, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1009, new DateTime(2026, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 4, 9, "Confirmed" },
                    { 10, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1010, new DateTime(2026, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 3, 10, "Confirmed" },
                    { 11, new DateTime(2026, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1011, new DateTime(2026, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, 7, 11, "Confirmed" },
                    { 12, new DateTime(2026, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1012, new DateTime(2026, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, 2, 12, "Cancelled" },
                    { 13, new DateTime(2026, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1013, new DateTime(2026, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, 5, 1, "Confirmed" },
                    { 14, new DateTime(2026, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1014, new DateTime(2026, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, 2, 2, "Pending" },
                    { 15, new DateTime(2026, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1015, new DateTime(2026, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, 4, 3, "Confirmed" },
                    { 16, new DateTime(2026, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1016, new DateTime(2026, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 16, 3, 4, "Confirmed" },
                    { 17, new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 1017, new DateTime(2026, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 17, 6, 5, "Confirmed" },
                    { 18, new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 1018, new DateTime(2026, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, 2, 6, "Cancelled" },
                    { 19, new DateTime(2026, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1019, new DateTime(2026, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 19, 4, 7, "Confirmed" },
                    { 20, new DateTime(2026, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1020, new DateTime(2026, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, 10, 8, "Confirmed" },
                    { 21, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1021, new DateTime(2026, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 21, 2, 9, "Confirmed" },
                    { 22, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1022, new DateTime(2026, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 22, 3, 10, "Pending" },
                    { 23, new DateTime(2026, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1023, new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 23, 5, 11, "Confirmed" },
                    { 24, new DateTime(2026, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1024, new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 24, 4, 12, "Confirmed" },
                    { 25, new DateTime(2026, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1025, new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 25, 2, 1, "Confirmed" },
                    { 26, new DateTime(2026, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1026, new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 26, 6, 2, "Cancelled" },
                    { 27, new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 1027, new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 27, 3, 5, "Confirmed" },
                    { 28, new DateTime(2026, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 1028, new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 28, 2, 6, "Confirmed" },
                    { 29, new DateTime(2026, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1029, new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 29, 4, 7, "Pending" },
                    { 30, new DateTime(2026, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1030, new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, 5, 8, "Confirmed" },
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

            migrationBuilder.InsertData(
                table: "MailLogs",
                columns: new[] { "Id", "BookingId", "SentDate", "SentTo", "Status" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 5, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), "anna.lindqvist@gmail.com", "Sent" },
                    { 2, 2, new DateTime(2026, 5, 1, 10, 5, 0, 0, DateTimeKind.Unspecified), "erik.johansson@gmail.com", "Sent" },
                    { 3, 3, new DateTime(2026, 5, 2, 11, 0, 0, 0, DateTimeKind.Unspecified), "maria.svensson@hotmail.com", "Sent" },
                    { 4, 4, new DateTime(2026, 5, 2, 11, 5, 0, 0, DateTimeKind.Unspecified), "oskar.bergstrom@outlook.com", "Sent" },
                    { 5, 5, new DateTime(2026, 5, 3, 12, 0, 0, 0, DateTimeKind.Unspecified), "lina.karlsson@gmail.com", "Sent" },
                    { 6, 6, new DateTime(2026, 5, 3, 12, 5, 0, 0, DateTimeKind.Unspecified), "johan.nilsson@gmail.com", "Failed" },
                    { 7, 7, new DateTime(2026, 5, 4, 13, 0, 0, 0, DateTimeKind.Unspecified), "sara.eriksson@hotmail.com", "Sent" },
                    { 8, 8, new DateTime(2026, 5, 4, 13, 5, 0, 0, DateTimeKind.Unspecified), "mikael.larsson@outlook.com", "Sent" },
                    { 9, 9, new DateTime(2026, 5, 5, 14, 0, 0, 0, DateTimeKind.Unspecified), "emma.olsson@gmail.com", "Sent" },
                    { 10, 10, new DateTime(2026, 5, 5, 14, 5, 0, 0, DateTimeKind.Unspecified), "andreas.persson@gmail.com", "Sent" },
                    { 11, 11, new DateTime(2026, 5, 6, 15, 0, 0, 0, DateTimeKind.Unspecified), "karin.andersson@hotmail.com", "Sent" },
                    { 12, 12, new DateTime(2026, 5, 6, 15, 5, 0, 0, DateTimeKind.Unspecified), "peter.gustafsson@outlook.com", "Failed" },
                    { 13, 13, new DateTime(2026, 5, 7, 16, 0, 0, 0, DateTimeKind.Unspecified), "sofia.magnusson@gmail.com", "Sent" },
                    { 14, 14, new DateTime(2026, 5, 7, 16, 5, 0, 0, DateTimeKind.Unspecified), "magnus.lindstrom@gmail.com", "Sent" },
                    { 15, 15, new DateTime(2026, 5, 7, 16, 10, 0, 0, DateTimeKind.Unspecified), "hanna.jakobsson@hotmail.com", "Pending" },
                    { 16, 16, new DateTime(2026, 5, 8, 10, 0, 0, 0, DateTimeKind.Unspecified), "daniel.petersson@outlook.com", "Sent" },
                    { 17, 17, new DateTime(2026, 5, 8, 10, 5, 0, 0, DateTimeKind.Unspecified), "maja.henriksson@gmail.com", "Sent" },
                    { 18, 18, new DateTime(2026, 5, 8, 10, 10, 0, 0, DateTimeKind.Unspecified), "jonas.sandberg@gmail.com", "Failed" },
                    { 19, 19, new DateTime(2026, 5, 9, 11, 0, 0, 0, DateTimeKind.Unspecified), "elin.sjoberg@hotmail.com", "Sent" },
                    { 20, 20, new DateTime(2026, 5, 9, 11, 5, 0, 0, DateTimeKind.Unspecified), "viktor.lundgren@outlook.com", "Sent" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_GuestId",
                table: "Bookings",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_SittingId",
                table: "Bookings",
                column: "SittingId");

            migrationBuilder.CreateIndex(
                name: "IX_MailLogs_BookingId",
                table: "MailLogs",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Sittings_OpeningDaysId",
                table: "Sittings",
                column: "OpeningDaysId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MailLogs");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.DropTable(
                name: "Sittings");

            migrationBuilder.DropTable(
                name: "OpeningDays");
        }
    }
}
