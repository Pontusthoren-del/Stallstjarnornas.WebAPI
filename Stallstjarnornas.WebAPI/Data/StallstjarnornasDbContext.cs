using Microsoft.EntityFrameworkCore;
using Stallstjarnornas.Library.Models;
namespace Stallstjarnornas.WebAPI.Data
{
    public class StallstjarnornasDbContext : DbContext
    {
        public StallstjarnornasDbContext(DbContextOptions<StallstjarnornasDbContext> options) : base(options)
        {

        }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Guest> Guests { get; set; }

        public DbSet<MailLog> MailLogs { get; set; }

        public DbSet<OperatingDay> OpeningDays { get; set; }

        public DbSet<Sitting> Sittings { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<TableAssignment> TableAssignments { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Guest)
                .WithMany(g => g.Bookings)
                .HasForeignKey(b => b.GuestId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Sitting)
                .WithMany(s => s.Bookings)
                .HasForeignKey(b => b.SittingId);
            modelBuilder.Entity<TableAssignment>()
                .HasOne(ta => ta.Booking)
                .WithMany(b => b.TableAssignments)
                .HasForeignKey(ta => ta.BookingId);
            modelBuilder.Entity<TableAssignment>()
                .HasOne(ta => ta.Table)
                .WithMany(t => t.TableAssignments)
                .HasForeignKey(ta => ta.TableId);

            modelBuilder.Entity<Table>().HasData(
               Enumerable.Range(1, 25).Select(i => new Table { Id = i, Seats = 2 }).ToArray()
                );

            // OpeningDays
            modelBuilder.Entity<OperatingDay>().HasData(
                new OperatingDay { Id = 1, Day = DayOfWeek.Monday, Opens = new TimeOnly(17, 0), Closes = new TimeOnly(22, 0), IsClosed = false },
                new OperatingDay { Id = 2, Day = DayOfWeek.Tuesday, Opens = new TimeOnly(17, 0), Closes = new TimeOnly(22, 0), IsClosed = false },
                new OperatingDay { Id = 3, Day = DayOfWeek.Wednesday, Opens = new TimeOnly(17, 0), Closes = new TimeOnly(22, 0), IsClosed = false },
                new OperatingDay { Id = 4, Day = DayOfWeek.Thursday, Opens = new TimeOnly(17, 0), Closes = new TimeOnly(22, 0), IsClosed = false },
                new OperatingDay { Id = 5, Day = DayOfWeek.Friday, Opens = new TimeOnly(17, 0), Closes = new TimeOnly(22, 0), IsClosed = false },
                new OperatingDay { Id = 6, Day = DayOfWeek.Saturday, Opens = new TimeOnly(17, 0), Closes = new TimeOnly(22, 0), IsClosed = false },
                new OperatingDay { Id = 7, Day = DayOfWeek.Sunday, Opens = new TimeOnly(17, 0), Closes = new TimeOnly(22, 0), IsClosed = true }
            );

            // Sittings - 2 per dag (även söndag definierad men IsClosed = true på OpeningDays)
            modelBuilder.Entity<Sitting>().HasData(
                new Sitting { Id = 1, OperatingDayId = 1, StartTime = new TimeOnly(17, 0), EndTime = new TimeOnly(19, 0), MaxGuests = 50 },
                new Sitting { Id = 2, OperatingDayId = 1, StartTime = new TimeOnly(19, 0), EndTime = new TimeOnly(21, 0), MaxGuests = 50 },
                new Sitting { Id = 3, OperatingDayId = 2, StartTime = new TimeOnly(17, 0), EndTime = new TimeOnly(19, 0), MaxGuests = 50 },
                new Sitting { Id = 4, OperatingDayId = 2, StartTime = new TimeOnly(19, 0), EndTime = new TimeOnly(21, 0), MaxGuests = 50 },
                new Sitting { Id = 5, OperatingDayId = 3, StartTime = new TimeOnly(17, 0), EndTime = new TimeOnly(19, 0), MaxGuests = 50 },
                new Sitting { Id = 6, OperatingDayId = 3, StartTime = new TimeOnly(19, 0), EndTime = new TimeOnly(21, 0), MaxGuests = 50 },
                new Sitting { Id = 7, OperatingDayId = 4, StartTime = new TimeOnly(17, 0), EndTime = new TimeOnly(19, 0), MaxGuests = 50 },
                new Sitting { Id = 8, OperatingDayId = 4, StartTime = new TimeOnly(19, 0), EndTime = new TimeOnly(21, 0), MaxGuests = 50 },
                new Sitting { Id = 9, OperatingDayId = 5, StartTime = new TimeOnly(17, 0), EndTime = new TimeOnly(19, 0), MaxGuests = 50 },
                new Sitting { Id = 10, OperatingDayId = 5, StartTime = new TimeOnly(19, 0), EndTime = new TimeOnly(21, 0), MaxGuests = 50 },
                new Sitting { Id = 11, OperatingDayId = 6, StartTime = new TimeOnly(17, 0), EndTime = new TimeOnly(19, 0), MaxGuests = 50 },
                new Sitting { Id = 12, OperatingDayId = 6, StartTime = new TimeOnly(19, 0), EndTime = new TimeOnly(21, 0), MaxGuests = 50 },
                new Sitting { Id = 13, OperatingDayId = 7, StartTime = new TimeOnly(17, 0), EndTime = new TimeOnly(19, 0), MaxGuests = 50 },
                new Sitting { Id = 14, OperatingDayId = 7, StartTime = new TimeOnly(19, 0), EndTime = new TimeOnly(21, 0), MaxGuests = 50 }
            );

            // Guests - 30 gäster
            modelBuilder.Entity<Guest>().HasData(
                new Guest { Id = 1, Name = "Anna Lindqvist", Phone = "070-123 45 67", Email = "anna.lindqvist@gmail.com" },
                new Guest { Id = 2, Name = "Erik Johansson", Phone = "073-234 56 78", Email = "erik.johansson@gmail.com" },
                new Guest { Id = 3, Name = "Maria Svensson", Phone = "076-345 67 89", Email = "maria.svensson@hotmail.com" },
                new Guest { Id = 4, Name = "Oskar Bergström", Phone = "072-456 78 90", Email = "oskar.bergstrom@outlook.com" },
                new Guest { Id = 5, Name = "Lina Karlsson", Phone = "070-567 89 01", Email = "lina.karlsson@gmail.com" },
                new Guest { Id = 6, Name = "Johan Nilsson", Phone = "073-678 90 12", Email = "johan.nilsson@gmail.com" },
                new Guest { Id = 7, Name = "Sara Eriksson", Phone = "076-789 01 23", Email = "sara.eriksson@hotmail.com" },
                new Guest { Id = 8, Name = "Mikael Larsson", Phone = "072-890 12 34", Email = "mikael.larsson@outlook.com" },
                new Guest { Id = 9, Name = "Emma Olsson", Phone = "070-901 23 45", Email = "emma.olsson@gmail.com" },
                new Guest { Id = 10, Name = "Andreas Persson", Phone = "073-012 34 56", Email = "andreas.persson@gmail.com" },
                new Guest { Id = 11, Name = "Karin Andersson", Phone = "076-123 45 67", Email = "karin.andersson@hotmail.com" },
                new Guest { Id = 12, Name = "Peter Gustafsson", Phone = "072-234 56 78", Email = "peter.gustafsson@outlook.com" },
                new Guest { Id = 13, Name = "Sofia Magnusson", Phone = "070-345 67 89", Email = "sofia.magnusson@gmail.com" },
                new Guest { Id = 14, Name = "Magnus Lindström", Phone = "073-456 78 90", Email = "magnus.lindstrom@gmail.com" },
                new Guest { Id = 15, Name = "Hanna Jakobsson", Phone = "076-567 89 01", Email = "hanna.jakobsson@hotmail.com" },
                new Guest { Id = 16, Name = "Daniel Petersson", Phone = "072-678 90 12", Email = "daniel.petersson@outlook.com" },
                new Guest { Id = 17, Name = "Maja Henriksson", Phone = "070-789 01 23", Email = "maja.henriksson@gmail.com" },
                new Guest { Id = 18, Name = "Jonas Sandberg", Phone = "073-890 12 34", Email = "jonas.sandberg@gmail.com" },
                new Guest { Id = 19, Name = "Elin Sjöberg", Phone = "076-901 23 45", Email = "elin.sjoberg@hotmail.com" },
                new Guest { Id = 20, Name = "Viktor Lundgren", Phone = "072-012 34 56", Email = "viktor.lundgren@outlook.com" },
                new Guest { Id = 21, Name = "Therese Holm", Phone = "070-111 22 33", Email = "therese.holm@gmail.com" },
                new Guest { Id = 22, Name = "Rickard Björk", Phone = "073-222 33 44", Email = "rickard.bjork@hotmail.com" },
                new Guest { Id = 23, Name = "Camilla Strand", Phone = "076-333 44 55", Email = "camilla.strand@gmail.com" },
                new Guest { Id = 24, Name = "Fredrik Holm", Phone = "072-444 55 66", Email = "fredrik.holm@outlook.com" },
                new Guest { Id = 25, Name = "Isabella Nyström", Phone = "070-555 66 77", Email = "isabella.nystrom@gmail.com" },
                new Guest { Id = 26, Name = "Tobias Engström", Phone = "073-666 77 88", Email = "tobias.engstrom@gmail.com" },
                new Guest { Id = 27, Name = "Matilda Forsberg", Phone = "076-777 88 99", Email = "matilda.forsberg@hotmail.com" },
                new Guest { Id = 28, Name = "Simon Åberg", Phone = "072-888 99 00", Email = "simon.aberg@outlook.com" },
                new Guest { Id = 29, Name = "Johanna Blom", Phone = "070-999 00 11", Email = "johanna.blom@gmail.com" },
                new Guest { Id = 30, Name = "Alexander Vång", Phone = "073-000 11 22", Email = "alexander.vang@gmail.com" }
            );

            // Bookings - 40 bokningar med Message flyttad hit
            // Bookings - 80 bokningar spridda över olika datum, sittningar och statusar
            modelBuilder.Entity<Booking>().HasData(
                // Vecka 20 2026 (11-17 maj)
                new Booking { Id = 1, GuestId = 1, SittingId = 1, BookingDate = new DateTime(2026, 5, 11), NoOfGuests = 2, Status = "Confirmed", BookingNumber = 1001, CreatedDate = new DateTime(2026, 5, 1), Message = null },
                new Booking { Id = 2, GuestId = 2, SittingId = 1, BookingDate = new DateTime(2026, 5, 11), NoOfGuests = 4, Status = "Confirmed", BookingNumber = 1002, CreatedDate = new DateTime(2026, 5, 1), Message = "Allergisk mot gluten" },
                new Booking { Id = 3, GuestId = 3, SittingId = 1, BookingDate = new DateTime(2026, 5, 11), NoOfGuests = 6, Status = "Confirmed", BookingNumber = 1003, CreatedDate = new DateTime(2026, 5, 2), Message = null },
                new Booking { Id = 4, GuestId = 4, SittingId = 1, BookingDate = new DateTime(2026, 5, 11), NoOfGuests = 8, Status = "Confirmed", BookingNumber = 1004, CreatedDate = new DateTime(2026, 5, 2), Message = "Laktosintolerant" },
                new Booking { Id = 5, GuestId = 5, SittingId = 1, BookingDate = new DateTime(2026, 5, 11), NoOfGuests = 6, Status = "Confirmed", BookingNumber = 1005, CreatedDate = new DateTime(2026, 5, 3), Message = null },
                new Booking { Id = 6, GuestId = 6, SittingId = 1, BookingDate = new DateTime(2026, 5, 11), NoOfGuests = 8, Status = "Confirmed", BookingNumber = 1006, CreatedDate = new DateTime(2026, 5, 3), Message = "Nötallergi, var noga!" },
                new Booking { Id = 7, GuestId = 7, SittingId = 1, BookingDate = new DateTime(2026, 5, 11), NoOfGuests = 8, Status = "Confirmed", BookingNumber = 1007, CreatedDate = new DateTime(2026, 5, 4), Message = null },
                new Booking { Id = 8, GuestId = 8, SittingId = 1, BookingDate = new DateTime(2026, 5, 11), NoOfGuests = 8, Status = "Confirmed", BookingNumber = 1008, CreatedDate = new DateTime(2026, 5, 4), Message = "Allergisk mot skaldjur" },
                // Sittning 1 måndag 11 maj är nu FULLBOKAD (2+4+6+8+6+8+8+8 = 50)

                new Booking { Id = 9, GuestId = 9, SittingId = 2, BookingDate = new DateTime(2026, 5, 11), NoOfGuests = 4, Status = "Confirmed", BookingNumber = 1009, CreatedDate = new DateTime(2026, 5, 5), Message = null },
                new Booking { Id = 10, GuestId = 10, SittingId = 2, BookingDate = new DateTime(2026, 5, 11), NoOfGuests = 3, Status = "Confirmed", BookingNumber = 1010, CreatedDate = new DateTime(2026, 5, 5), Message = null },
                new Booking { Id = 11, GuestId = 11, SittingId = 2, BookingDate = new DateTime(2026, 5, 11), NoOfGuests = 7, Status = "Confirmed", BookingNumber = 1011, CreatedDate = new DateTime(2026, 5, 6), Message = "Glutenintolerant och laktosintolerant" },
                new Booking { Id = 12, GuestId = 12, SittingId = 2, BookingDate = new DateTime(2026, 5, 11), NoOfGuests = 2, Status = "Cancelled", BookingNumber = 1012, CreatedDate = new DateTime(2026, 5, 6), Message = null },

                new Booking { Id = 13, GuestId = 13, SittingId = 3, BookingDate = new DateTime(2026, 5, 12), NoOfGuests = 5, Status = "Confirmed", BookingNumber = 1013, CreatedDate = new DateTime(2026, 5, 7), Message = "Äggallergi" },
                new Booking { Id = 14, GuestId = 14, SittingId = 3, BookingDate = new DateTime(2026, 5, 12), NoOfGuests = 2, Status = "Pending", BookingNumber = 1014, CreatedDate = new DateTime(2026, 5, 7), Message = null },
                new Booking { Id = 15, GuestId = 15, SittingId = 3, BookingDate = new DateTime(2026, 5, 12), NoOfGuests = 4, Status = "Confirmed", BookingNumber = 1015, CreatedDate = new DateTime(2026, 5, 7), Message = null },
                new Booking { Id = 16, GuestId = 16, SittingId = 4, BookingDate = new DateTime(2026, 5, 12), NoOfGuests = 3, Status = "Confirmed", BookingNumber = 1016, CreatedDate = new DateTime(2026, 5, 8), Message = "Nötallergi" },
                new Booking { Id = 17, GuestId = 17, SittingId = 4, BookingDate = new DateTime(2026, 5, 12), NoOfGuests = 6, Status = "Confirmed", BookingNumber = 1017, CreatedDate = new DateTime(2026, 5, 8), Message = null },
                new Booking { Id = 18, GuestId = 18, SittingId = 4, BookingDate = new DateTime(2026, 5, 12), NoOfGuests = 2, Status = "Cancelled", BookingNumber = 1018, CreatedDate = new DateTime(2026, 5, 8), Message = "Sojaöverkänslighet" },

                new Booking { Id = 19, GuestId = 19, SittingId = 5, BookingDate = new DateTime(2026, 5, 13), NoOfGuests = 4, Status = "Confirmed", BookingNumber = 1019, CreatedDate = new DateTime(2026, 5, 9), Message = null },
                new Booking { Id = 20, GuestId = 20, SittingId = 5, BookingDate = new DateTime(2026, 5, 13), NoOfGuests = 8, Status = "Confirmed", BookingNumber = 1020, CreatedDate = new DateTime(2026, 5, 9), Message = null },
                new Booking { Id = 21, GuestId = 21, SittingId = 5, BookingDate = new DateTime(2026, 5, 13), NoOfGuests = 2, Status = "Confirmed", BookingNumber = 1021, CreatedDate = new DateTime(2026, 5, 9), Message = "Celiaki, strikt glutenfri mat krävs" },
                new Booking { Id = 22, GuestId = 22, SittingId = 6, BookingDate = new DateTime(2026, 5, 13), NoOfGuests = 3, Status = "Pending", BookingNumber = 1022, CreatedDate = new DateTime(2026, 5, 9), Message = null },
                new Booking { Id = 23, GuestId = 23, SittingId = 6, BookingDate = new DateTime(2026, 5, 13), NoOfGuests = 5, Status = "Confirmed", BookingNumber = 1023, CreatedDate = new DateTime(2026, 5, 10), Message = "Laktosintolerant" },
                new Booking { Id = 24, GuestId = 24, SittingId = 6, BookingDate = new DateTime(2026, 5, 13), NoOfGuests = 4, Status = "Confirmed", BookingNumber = 1024, CreatedDate = new DateTime(2026, 5, 10), Message = null },

                new Booking { Id = 25, GuestId = 25, SittingId = 7, BookingDate = new DateTime(2026, 5, 14), NoOfGuests = 2, Status = "Confirmed", BookingNumber = 1025, CreatedDate = new DateTime(2026, 5, 10), Message = null },
                new Booking { Id = 26, GuestId = 26, SittingId = 7, BookingDate = new DateTime(2026, 5, 14), NoOfGuests = 6, Status = "Cancelled", BookingNumber = 1026, CreatedDate = new DateTime(2026, 5, 10), Message = "Skaldjursallergi" },
                new Booking { Id = 27, GuestId = 27, SittingId = 7, BookingDate = new DateTime(2026, 5, 14), NoOfGuests = 3, Status = "Confirmed", BookingNumber = 1027, CreatedDate = new DateTime(2026, 5, 10), Message = null },
                new Booking { Id = 28, GuestId = 28, SittingId = 8, BookingDate = new DateTime(2026, 5, 14), NoOfGuests = 2, Status = "Confirmed", BookingNumber = 1028, CreatedDate = new DateTime(2026, 5, 10), Message = null },
                new Booking { Id = 29, GuestId = 29, SittingId = 8, BookingDate = new DateTime(2026, 5, 14), NoOfGuests = 4, Status = "Pending", BookingNumber = 1029, CreatedDate = new DateTime(2026, 5, 10), Message = "Ägg- och nötallergi" },
                new Booking { Id = 30, GuestId = 30, SittingId = 8, BookingDate = new DateTime(2026, 5, 14), NoOfGuests = 5, Status = "Confirmed", BookingNumber = 1030, CreatedDate = new DateTime(2026, 5, 10), Message = null },

                // Vecka 21 2026 (18-24 maj)
                new Booking { Id = 31, GuestId = 1, SittingId = 1, BookingDate = new DateTime(2026, 5, 18), NoOfGuests = 4, Status = "Confirmed", BookingNumber = 1031, CreatedDate = new DateTime(2026, 5, 11), Message = null },
                new Booking { Id = 32, GuestId = 2, SittingId = 1, BookingDate = new DateTime(2026, 5, 18), NoOfGuests = 6, Status = "Confirmed", BookingNumber = 1032, CreatedDate = new DateTime(2026, 5, 11), Message = "Glutenfri" },
                new Booking { Id = 33, GuestId = 3, SittingId = 2, BookingDate = new DateTime(2026, 5, 18), NoOfGuests = 2, Status = "Pending", BookingNumber = 1033, CreatedDate = new DateTime(2026, 5, 12), Message = null },
                new Booking { Id = 34, GuestId = 4, SittingId = 2, BookingDate = new DateTime(2026, 5, 18), NoOfGuests = 8, Status = "Confirmed", BookingNumber = 1034, CreatedDate = new DateTime(2026, 5, 12), Message = null },
                new Booking { Id = 35, GuestId = 5, SittingId = 3, BookingDate = new DateTime(2026, 5, 19), NoOfGuests = 3, Status = "Confirmed", BookingNumber = 1035, CreatedDate = new DateTime(2026, 5, 12), Message = null },
                new Booking { Id = 36, GuestId = 6, SittingId = 3, BookingDate = new DateTime(2026, 5, 19), NoOfGuests = 5, Status = "Cancelled", BookingNumber = 1036, CreatedDate = new DateTime(2026, 5, 13), Message = null },
                new Booking { Id = 37, GuestId = 7, SittingId = 4, BookingDate = new DateTime(2026, 5, 19), NoOfGuests = 2, Status = "Confirmed", BookingNumber = 1037, CreatedDate = new DateTime(2026, 5, 13), Message = null },
                new Booking { Id = 38, GuestId = 8, SittingId = 4, BookingDate = new DateTime(2026, 5, 19), NoOfGuests = 4, Status = "Confirmed", BookingNumber = 1038, CreatedDate = new DateTime(2026, 5, 13), Message = "Skaldjursallergi" },
                new Booking { Id = 39, GuestId = 9, SittingId = 9, BookingDate = new DateTime(2026, 5, 22), NoOfGuests = 6, Status = "Confirmed", BookingNumber = 1039, CreatedDate = new DateTime(2026, 5, 14), Message = null },
                new Booking { Id = 40, GuestId = 10, SittingId = 9, BookingDate = new DateTime(2026, 5, 22), NoOfGuests = 4, Status = "Confirmed", BookingNumber = 1040, CreatedDate = new DateTime(2026, 5, 14), Message = null },
                new Booking { Id = 41, GuestId = 11, SittingId = 10, BookingDate = new DateTime(2026, 5, 22), NoOfGuests = 8, Status = "Confirmed", BookingNumber = 1041, CreatedDate = new DateTime(2026, 5, 14), Message = "Vegansk" },
                new Booking { Id = 42, GuestId = 12, SittingId = 10, BookingDate = new DateTime(2026, 5, 22), NoOfGuests = 2, Status = "Pending", BookingNumber = 1042, CreatedDate = new DateTime(2026, 5, 15), Message = null },
                new Booking { Id = 43, GuestId = 13, SittingId = 11, BookingDate = new DateTime(2026, 5, 23), NoOfGuests = 5, Status = "Confirmed", BookingNumber = 1043, CreatedDate = new DateTime(2026, 5, 15), Message = null },
                new Booking { Id = 44, GuestId = 14, SittingId = 11, BookingDate = new DateTime(2026, 5, 23), NoOfGuests = 3, Status = "Confirmed", BookingNumber = 1044, CreatedDate = new DateTime(2026, 5, 15), Message = "Äggallergi" },
                new Booking { Id = 45, GuestId = 15, SittingId = 12, BookingDate = new DateTime(2026, 5, 23), NoOfGuests = 7, Status = "Confirmed", BookingNumber = 1045, CreatedDate = new DateTime(2026, 5, 15), Message = null },

                // Vecka 22 2026 (25-31 maj) - Fullbokad lördag kväll
                new Booking { Id = 46, GuestId = 16, SittingId = 11, BookingDate = new DateTime(2026, 5, 30), NoOfGuests = 8, Status = "Confirmed", BookingNumber = 1046, CreatedDate = new DateTime(2026, 5, 16), Message = null },
                new Booking { Id = 47, GuestId = 17, SittingId = 11, BookingDate = new DateTime(2026, 5, 30), NoOfGuests = 8, Status = "Confirmed", BookingNumber = 1047, CreatedDate = new DateTime(2026, 5, 16), Message = null },
                new Booking { Id = 48, GuestId = 18, SittingId = 11, BookingDate = new DateTime(2026, 5, 30), NoOfGuests = 8, Status = "Confirmed", BookingNumber = 1048, CreatedDate = new DateTime(2026, 5, 17), Message = "Sojaöverkänslighet" },
                new Booking { Id = 49, GuestId = 19, SittingId = 11, BookingDate = new DateTime(2026, 5, 30), NoOfGuests = 8, Status = "Confirmed", BookingNumber = 1049, CreatedDate = new DateTime(2026, 5, 17), Message = null },
                new Booking { Id = 50, GuestId = 20, SittingId = 11, BookingDate = new DateTime(2026, 5, 30), NoOfGuests = 8, Status = "Confirmed", BookingNumber = 1050, CreatedDate = new DateTime(2026, 5, 17), Message = null },
                new Booking { Id = 51, GuestId = 21, SittingId = 11, BookingDate = new DateTime(2026, 5, 30), NoOfGuests = 6, Status = "Confirmed", BookingNumber = 1051, CreatedDate = new DateTime(2026, 5, 17), Message = "Celiaki" },
                new Booking { Id = 52, GuestId = 22, SittingId = 11, BookingDate = new DateTime(2026, 5, 30), NoOfGuests = 4, Status = "Confirmed", BookingNumber = 1052, CreatedDate = new DateTime(2026, 5, 18), Message = null },
                // Sittning 11 lördag 30 maj är nu FULLBOKAD (8+8+8+8+8+6+4 = 50)

                new Booking { Id = 53, GuestId = 23, SittingId = 12, BookingDate = new DateTime(2026, 5, 30), NoOfGuests = 3, Status = "Confirmed", BookingNumber = 1053, CreatedDate = new DateTime(2026, 5, 18), Message = null },
                new Booking { Id = 54, GuestId = 24, SittingId = 12, BookingDate = new DateTime(2026, 5, 30), NoOfGuests = 5, Status = "Pending", BookingNumber = 1054, CreatedDate = new DateTime(2026, 5, 18), Message = "Laktosintolerant" },
                new Booking { Id = 55, GuestId = 25, SittingId = 9, BookingDate = new DateTime(2026, 5, 29), NoOfGuests = 2, Status = "Confirmed", BookingNumber = 1055, CreatedDate = new DateTime(2026, 5, 18), Message = null },
                new Booking { Id = 56, GuestId = 26, SittingId = 9, BookingDate = new DateTime(2026, 5, 29), NoOfGuests = 4, Status = "Cancelled", BookingNumber = 1056, CreatedDate = new DateTime(2026, 5, 19), Message = null },

                // Juni 2026
                new Booking { Id = 57, GuestId = 27, SittingId = 1, BookingDate = new DateTime(2026, 6, 1), NoOfGuests = 3, Status = "Confirmed", BookingNumber = 1057, CreatedDate = new DateTime(2026, 5, 20), Message = null },
                new Booking { Id = 58, GuestId = 28, SittingId = 1, BookingDate = new DateTime(2026, 6, 1), NoOfGuests = 5, Status = "Confirmed", BookingNumber = 1058, CreatedDate = new DateTime(2026, 5, 20), Message = "Nötallergi" },
                new Booking { Id = 59, GuestId = 29, SittingId = 2, BookingDate = new DateTime(2026, 6, 1), NoOfGuests = 2, Status = "Pending", BookingNumber = 1059, CreatedDate = new DateTime(2026, 5, 21), Message = null },
                new Booking { Id = 60, GuestId = 30, SittingId = 2, BookingDate = new DateTime(2026, 6, 1), NoOfGuests = 4, Status = "Confirmed", BookingNumber = 1060, CreatedDate = new DateTime(2026, 5, 21), Message = null },
                new Booking { Id = 61, GuestId = 1, SittingId = 5, BookingDate = new DateTime(2026, 6, 10), NoOfGuests = 6, Status = "Confirmed", BookingNumber = 1061, CreatedDate = new DateTime(2026, 5, 25), Message = null },
                new Booking { Id = 62, GuestId = 2, SittingId = 6, BookingDate = new DateTime(2026, 6, 10), NoOfGuests = 2, Status = "Confirmed", BookingNumber = 1062, CreatedDate = new DateTime(2026, 5, 25), Message = "Glutenfri" },
                new Booking { Id = 63, GuestId = 3, SittingId = 7, BookingDate = new DateTime(2026, 6, 15), NoOfGuests = 8, Status = "Confirmed", BookingNumber = 1063, CreatedDate = new DateTime(2026, 5, 28), Message = null },
                new Booking { Id = 64, GuestId = 4, SittingId = 8, BookingDate = new DateTime(2026, 6, 15), NoOfGuests = 4, Status = "Cancelled", BookingNumber = 1064, CreatedDate = new DateTime(2026, 5, 28), Message = null },
                new Booking { Id = 65, GuestId = 5, SittingId = 9, BookingDate = new DateTime(2026, 6, 20), NoOfGuests = 3, Status = "Confirmed", BookingNumber = 1065, CreatedDate = new DateTime(2026, 6, 1), Message = "Vegansk" },
                new Booking { Id = 66, GuestId = 6, SittingId = 10, BookingDate = new DateTime(2026, 6, 20), NoOfGuests = 5, Status = "Confirmed", BookingNumber = 1066, CreatedDate = new DateTime(2026, 6, 1), Message = null },

                // Juli 2026
                new Booking { Id = 67, GuestId = 7, SittingId = 11, BookingDate = new DateTime(2026, 7, 4), NoOfGuests = 4, Status = "Confirmed", BookingNumber = 1067, CreatedDate = new DateTime(2026, 6, 15), Message = null },
                new Booking { Id = 68, GuestId = 8, SittingId = 12, BookingDate = new DateTime(2026, 7, 4), NoOfGuests = 6, Status = "Confirmed", BookingNumber = 1068, CreatedDate = new DateTime(2026, 6, 15), Message = "Skaldjursallergi" },
                new Booking { Id = 69, GuestId = 9, SittingId = 1, BookingDate = new DateTime(2026, 7, 13), NoOfGuests = 2, Status = "Pending", BookingNumber = 1069, CreatedDate = new DateTime(2026, 6, 20), Message = null },
                new Booking { Id = 70, GuestId = 10, SittingId = 2, BookingDate = new DateTime(2026, 7, 13), NoOfGuests = 8, Status = "Confirmed", BookingNumber = 1070, CreatedDate = new DateTime(2026, 6, 20), Message = null },
                new Booking { Id = 71, GuestId = 11, SittingId = 3, BookingDate = new DateTime(2026, 7, 20), NoOfGuests = 5, Status = "Confirmed", BookingNumber = 1071, CreatedDate = new DateTime(2026, 6, 25), Message = "Glutenintolerant" },
                new Booking { Id = 72, GuestId = 12, SittingId = 4, BookingDate = new DateTime(2026, 7, 20), NoOfGuests = 3, Status = "Cancelled", BookingNumber = 1072, CreatedDate = new DateTime(2026, 6, 25), Message = null },

                // Höst 2026
                new Booking { Id = 73, GuestId = 13, SittingId = 5, BookingDate = new DateTime(2026, 9, 5), NoOfGuests = 4, Status = "Confirmed", BookingNumber = 1073, CreatedDate = new DateTime(2026, 8, 20), Message = null },
                new Booking { Id = 74, GuestId = 14, SittingId = 6, BookingDate = new DateTime(2026, 9, 5), NoOfGuests = 2, Status = "Confirmed", BookingNumber = 1074, CreatedDate = new DateTime(2026, 8, 20), Message = "Äggallergi" },
                new Booking { Id = 75, GuestId = 15, SittingId = 7, BookingDate = new DateTime(2026, 10, 10), NoOfGuests = 6, Status = "Confirmed", BookingNumber = 1075, CreatedDate = new DateTime(2026, 9, 25), Message = null },
                new Booking { Id = 76, GuestId = 16, SittingId = 8, BookingDate = new DateTime(2026, 10, 10), NoOfGuests = 4, Status = "Pending", BookingNumber = 1076, CreatedDate = new DateTime(2026, 9, 25), Message = null },
                new Booking { Id = 77, GuestId = 17, SittingId = 9, BookingDate = new DateTime(2026, 11, 20), NoOfGuests = 8, Status = "Confirmed", BookingNumber = 1077, CreatedDate = new DateTime(2026, 11, 1), Message = "Vegansk" },
                new Booking { Id = 78, GuestId = 18, SittingId = 10, BookingDate = new DateTime(2026, 12, 24), NoOfGuests = 6, Status = "Confirmed", BookingNumber = 1078, CreatedDate = new DateTime(2026, 12, 1), Message = "Julbord, extra speciellt!" },
                new Booking { Id = 79, GuestId = 19, SittingId = 11, BookingDate = new DateTime(2026, 12, 24), NoOfGuests = 4, Status = "Confirmed", BookingNumber = 1079, CreatedDate = new DateTime(2026, 12, 1), Message = null },
                new Booking { Id = 80, GuestId = 20, SittingId = 12, BookingDate = new DateTime(2026, 12, 31), NoOfGuests = 8, Status = "Confirmed", BookingNumber = 1080, CreatedDate = new DateTime(2026, 12, 15), Message = "Nyårsmiddag!" }
            );

            // MailLogs
            modelBuilder.Entity<MailLog>().HasData(
                new MailLog { Id = 1, BookingId = 1, SentTo = "anna.lindqvist@gmail.com", SentDate = new DateTime(2026, 5, 1, 10, 0, 0), Status = "Sent" },
                new MailLog { Id = 2, BookingId = 2, SentTo = "erik.johansson@gmail.com", SentDate = new DateTime(2026, 5, 1, 10, 5, 0), Status = "Sent" },
                new MailLog { Id = 3, BookingId = 3, SentTo = "maria.svensson@hotmail.com", SentDate = new DateTime(2026, 5, 2, 11, 0, 0), Status = "Sent" },
                new MailLog { Id = 4, BookingId = 4, SentTo = "oskar.bergstrom@outlook.com", SentDate = new DateTime(2026, 5, 2, 11, 5, 0), Status = "Sent" },
                new MailLog { Id = 5, BookingId = 5, SentTo = "lina.karlsson@gmail.com", SentDate = new DateTime(2026, 5, 3, 12, 0, 0), Status = "Sent" },
                new MailLog { Id = 6, BookingId = 6, SentTo = "johan.nilsson@gmail.com", SentDate = new DateTime(2026, 5, 3, 12, 5, 0), Status = "Failed" },
                new MailLog { Id = 7, BookingId = 7, SentTo = "sara.eriksson@hotmail.com", SentDate = new DateTime(2026, 5, 4, 13, 0, 0), Status = "Sent" },
                new MailLog { Id = 8, BookingId = 8, SentTo = "mikael.larsson@outlook.com", SentDate = new DateTime(2026, 5, 4, 13, 5, 0), Status = "Sent" },
                new MailLog { Id = 9, BookingId = 9, SentTo = "emma.olsson@gmail.com", SentDate = new DateTime(2026, 5, 5, 14, 0, 0), Status = "Sent" },
                new MailLog { Id = 10, BookingId = 10, SentTo = "andreas.persson@gmail.com", SentDate = new DateTime(2026, 5, 5, 14, 5, 0), Status = "Sent" },
                new MailLog { Id = 11, BookingId = 11, SentTo = "karin.andersson@hotmail.com", SentDate = new DateTime(2026, 5, 6, 15, 0, 0), Status = "Sent" },
                new MailLog { Id = 12, BookingId = 12, SentTo = "peter.gustafsson@outlook.com", SentDate = new DateTime(2026, 5, 6, 15, 5, 0), Status = "Failed" },
                new MailLog { Id = 13, BookingId = 13, SentTo = "sofia.magnusson@gmail.com", SentDate = new DateTime(2026, 5, 7, 16, 0, 0), Status = "Sent" },
                new MailLog { Id = 14, BookingId = 14, SentTo = "magnus.lindstrom@gmail.com", SentDate = new DateTime(2026, 5, 7, 16, 5, 0), Status = "Sent" },
                new MailLog { Id = 15, BookingId = 15, SentTo = "hanna.jakobsson@hotmail.com", SentDate = new DateTime(2026, 5, 7, 16, 10, 0), Status = "Pending" },
                new MailLog { Id = 16, BookingId = 16, SentTo = "daniel.petersson@outlook.com", SentDate = new DateTime(2026, 5, 8, 10, 0, 0), Status = "Sent" },
                new MailLog { Id = 17, BookingId = 17, SentTo = "maja.henriksson@gmail.com", SentDate = new DateTime(2026, 5, 8, 10, 5, 0), Status = "Sent" },
                new MailLog { Id = 18, BookingId = 18, SentTo = "jonas.sandberg@gmail.com", SentDate = new DateTime(2026, 5, 8, 10, 10, 0), Status = "Failed" },
                new MailLog { Id = 19, BookingId = 19, SentTo = "elin.sjoberg@hotmail.com", SentDate = new DateTime(2026, 5, 9, 11, 0, 0), Status = "Sent" },
                new MailLog { Id = 20, BookingId = 20, SentTo = "viktor.lundgren@outlook.com", SentDate = new DateTime(2026, 5, 9, 11, 5, 0), Status = "Sent" }
            );
        }
    }
}
