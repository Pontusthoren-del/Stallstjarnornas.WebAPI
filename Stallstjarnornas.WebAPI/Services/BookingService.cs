using Microsoft.EntityFrameworkCore;
using Stallstjarnornas.WebAPI.Data;
using Stallstjarnornas.WebAPI.DTOs.Booking;
using Stallstjarnornas.WebAPI.Interfaces;
using Stallstjarnornas.Library.Models;
using Stallstjarnornas.WebAPI.Exceptions;

namespace Stallstjarnornas.WebAPI.Services
{
    public class BookingService : IBookingService
    {
        private readonly StallstjarnornasDbContext _ctx;
        private readonly IGuestService _guestService;
        private readonly IMailLogService _mailService;
        public BookingService(StallstjarnornasDbContext ctx, IGuestService guestService, IMailLogService mailService)
        {
            _ctx = ctx;
            _guestService = guestService;
            _mailService = mailService;
        }
        public async Task<String> CancelBookingAsync(int bookingNumber)
        {
            var booking = await _ctx.Bookings
                .Include(b => b.Guest)
                .FirstOrDefaultAsync(b => b.BookingNumber == bookingNumber);
            if (booking == null)
            {
                throw new NotFoundException("Bokning hittades inte.");
            }
            if (booking.Status == "Cancelled")
            {
                throw new ConflictException("Bokningen är redan avbokad.");
            }
            booking.Status = "Cancelled";
            await _ctx.SaveChangesAsync();
            return booking.Guest.Name;
        }
        public async Task<BookingResponseDto> CreateBookingAsync(CreateBookingDto dto)
        {
            var guest = await _guestService.GetGuestEntityByEmailAsync(dto.Email);
            if (guest == null)
            {
                guest = new Guest
                {
                    Name = dto.Name,
                    Phone = dto.Phone,
                    Email = dto.Email
                };
                _ctx.Guests.Add(guest);
            }
            if (dto.BookingDate < DateOnly.FromDateTime(DateTime.Today))
            {
                throw new ValidationException("Bokningsdatumet kan inte vara i det förflutna.");
            }
            var sittingInfo = await _ctx.Sittings
                .Where(s => s.Id == dto.SittingId)
                .Select(s => new
                {
                    s.Id,
                    s.StartTime,
                    s.EndTime,
                    s.MaxGuests,
                    BookedGuests = s.Bookings
                        .Where(b => b.BookingDate == dto.BookingDate.ToDateTime(TimeOnly.MinValue)
                        && b.Status != "Cancelled")
                        .Sum(b => b.NoOfGuests)
                })
                .FirstOrDefaultAsync();
            if (sittingInfo == null)
            {
                throw new NotFoundException("Sittningen finns inte.");
            }
            if (sittingInfo.BookedGuests + dto.NumberOfGuests > sittingInfo.MaxGuests)
            {
                throw new ValidationException("Sittningen är fullbokad.");
            }
            var maxBookingNumber = await _ctx.Bookings
                .MaxAsync(b => (int?)b.BookingNumber) ?? 1000;

            var booking = new Booking
            {
                Guest = guest,
                SittingId = dto.SittingId,
                BookingDate = dto.BookingDate.ToDateTime(TimeOnly.MinValue),
                NoOfGuests = dto.NumberOfGuests,
                Status = "Pending",
                BookingNumber = maxBookingNumber + 1,
                CreatedDate = DateTime.Now,
                Message = dto.Message
            };
            _ctx.Bookings.Add(booking);
            await _ctx.SaveChangesAsync();
            await _mailService.LogMailAsync(guest.Email, booking.Id);
            return new BookingResponseDto(
                BookingNumber: booking.BookingNumber,
                GuestName: guest.Name,
                GuestEmail: guest.Email,
                GuestPhone: guest.Phone,
                BookingDate: dto.BookingDate,
                SittingStartTime: sittingInfo.StartTime,
                SittingEndTime: sittingInfo.EndTime,
                NumberOfGuests: booking.NoOfGuests,
                Status: booking.Status,
                Message: booking.Message,
                CreatedDate: booking.CreatedDate,
                IsPlaced: false
            );
        }
        
        /// Filtrerar bokningar baserat på valfria sökkriterier.
        /// Alla parametrar är optional — skickas ingen parameter returneras alla bokningar.
        /// 
        public async Task<IEnumerable<BookingResponseDto>> FilterBookingsAsync(
            string? status, DateOnly? date, int? sittingId, int? week, int? month, int? year, bool? isPlaced, string? guestName, int? bookingNumber)
        {
            // Startar en query mot databasen utan att faktiskt hämta data än (deferred execution)
            var query = _ctx.Bookings.AsQueryable();

            // Filtrerar på exakt datum om det angetts
            if (date.HasValue)
            {
                query = query.Where(b => b.BookingDate == date.Value.ToDateTime(TimeOnly.MinValue));
            }

            // Filtrerar på vilket sittning-pass bokningen tillhör
            if (sittingId.HasValue)
            {
                query = query.Where(b => b.SittingId == sittingId.Value);
            }

            // Case-insensitivt namnfilter — matchar delsträngar (t.ex. "anna" hittar "Anna Svensson")
            if (!string.IsNullOrEmpty(guestName))
            {
                query = query.Where(b => b.Guest.Name.ToLower().Contains(guestName.ToLower()));
            }

            // Filtrerar på ett specifikt bokningsnummer
            if (bookingNumber.HasValue)
            {
                query = query.Where(b => b.BookingNumber == bookingNumber.Value);
            }

            // Filtrerar på bokningsstatus (t.ex. "Active", "Cancelled", "Confirmed")
            if (status != null)
            {
                query = query.Where(b => b.Status == status);
            }

            // Månad kräver alltid ett år för att vara meningsfullt
            if (month.HasValue && year.HasValue)
            {
                query = query.Where(b => b.BookingDate.Month == month.Value && b.BookingDate.Year == year.Value);
            }
            // År utan vecka hanteras här — år + vecka hanteras senare i minnet (se nedan)
            else if (year.HasValue && !week.HasValue)
            {
                query = query.Where(b => b.BookingDate.Year == year.Value);
            }

            // Kör SQL-frågan mot databasen här — resterande filtrering sker i minnet
            // eftersom ISOWeek inte stöds direkt av EF Core / SQL Server
            var result = await query
                .Select(b => new
                {
                    b.BookingNumber,
                    GuestName = b.Guest.Name,
                    GuestEmail = b.Guest.Email,
                    GuestPhone = b.Guest.Phone,
                    BookingDate = DateOnly.FromDateTime(b.BookingDate),
                    b.Sitting.StartTime,
                    b.Sitting.EndTime,
                    b.NoOfGuests,
                    b.Status,
                    b.Message,
                    b.CreatedDate,
                    // Kontrollerar om bordsplacering finns via TableAssignments-relationen
                    IsPlaced = b.TableAssignments.Any()
                })
                .ToListAsync();

            // Veckofiltrering måste ske i minnet (in-memory) eftersom
            // ISOWeek.GetWeekOfYear() inte kan översättas till SQL av EF Core
            if (week.HasValue && year.HasValue)
            {
                result = result.Where(b =>
                    System.Globalization.ISOWeek.GetWeekOfYear(
                        b.BookingDate.ToDateTime(TimeOnly.MinValue)) == week.Value &&
                    System.Globalization.ISOWeek.GetYear(
                        b.BookingDate.ToDateTime(TimeOnly.MinValue)) == year.Value)
                    .ToList();
            }
            else if (week.HasValue)
            {
                result = result.Where(b =>
                    System.Globalization.ISOWeek.GetWeekOfYear(
                        b.BookingDate.ToDateTime(TimeOnly.MinValue)) == week.Value)
                    .ToList();
            }

            // isPlaced filtreras också i minnet eftersom det är ett beräknat värde
            if (isPlaced.HasValue)
            {
                result = result.Where(b => b.IsPlaced == isPlaced.Value).ToList();
            }

            // Mappar det anonyma objektet till en DTO innan retur
            return result.Select(b => new BookingResponseDto(
                BookingNumber: b.BookingNumber,
                GuestName: b.GuestName,
                GuestEmail: b.GuestEmail,
                GuestPhone: b.GuestPhone,
                BookingDate: b.BookingDate,
                SittingStartTime: b.StartTime,
                SittingEndTime: b.EndTime,
                NumberOfGuests: b.NoOfGuests,
                Status: b.Status,
                Message: b.Message,
                CreatedDate: b.CreatedDate,
                IsPlaced: b.IsPlaced
            ));
        }
        public async Task<BookingResponseDto> GetBookingByNumberAsync(int bookingNumber)
        {
            var booking = await _ctx.Bookings
                .Where(b => b.BookingNumber == bookingNumber)
                .Select(b => new
                {
                    b.BookingNumber,
                    GuestName = b.Guest.Name,
                    GuestEmail = b.Guest.Email,
                    GuestPhone = b.Guest.Phone,
                    BookingDate = DateOnly.FromDateTime(b.BookingDate),
                    b.Sitting.StartTime,
                    b.Sitting.EndTime,
                    b.NoOfGuests,
                    b.Status,
                    b.Message,
                    b.CreatedDate,
                    IsPlaced = b.TableAssignments.Any()
                })
                .FirstOrDefaultAsync();
            if (booking == null)
            {
                throw new NotFoundException("Bokningen hittades inte.");
            }
            return new BookingResponseDto(
                BookingNumber: booking.BookingNumber,
                GuestName: booking.GuestName,
                GuestEmail: booking.GuestEmail,
                GuestPhone: booking.GuestPhone,
                BookingDate: booking.BookingDate,
                SittingStartTime: booking.StartTime,
                SittingEndTime: booking.EndTime,
                NumberOfGuests: booking.NoOfGuests,
                Status: booking.Status,
                Message: booking.Message,
                CreatedDate: booking.CreatedDate,
                IsPlaced: booking.IsPlaced
            );
        }
        public async Task<BookingResponseDto> UpdateBookingAsync(int bookingNumber, UpdateBookingDto dto)
        {
            var booking = await _ctx.Bookings
                .FirstOrDefaultAsync(b => b.BookingNumber == bookingNumber);
            //Fel om bokningen inte hittades
            if (booking == null)
            {
                throw new NotFoundException("Bokningen hittades inte.");
            }
            //Fel om man bokar i det förflutna
            if (dto.BookingDate.HasValue && dto.BookingDate.Value < DateOnly.FromDateTime(DateTime.Today))
            {
                throw new ValidationException("Bokningsdatumet kan inte vara i det förflutna.");
            }
            //körs bara om man försöker byta sittning eller datum
            if (dto.SittingId.HasValue || dto.BookingDate.HasValue)
            {
                var newSittingId = dto.SittingId ?? booking.SittingId;
                var newDate = dto.BookingDate?.ToDateTime(TimeOnly.MinValue) ?? booking.BookingDate;
                var newGuests = dto.NumberOfGuests ?? booking.NoOfGuests;

                var sittingInfo = await _ctx.Sittings
                    .Where(s => s.Id == newSittingId)
                    .Select(s => new
                    {
                        s.MaxGuests,
                        BookedGuests = s.Bookings
                            .Where(b => b.BookingDate == newDate && b.Status != "Cancelled" && b.BookingNumber != bookingNumber)
                            .Sum(b => b.NoOfGuests)
                    })
                    .FirstOrDefaultAsync();

                if (sittingInfo == null)
                    throw new NotFoundException("Sittningen finns inte.");

                if (sittingInfo.BookedGuests + newGuests > sittingInfo.MaxGuests)
                    throw new ValidationException("Sittningen är fullbokad.");
            }
            var allowedStatuses = new[] { "Confirmed", "Cancelled", "Pending" };
            if (dto.Status != null && !allowedStatuses.Contains(dto.Status))
            {
                throw new ValidationException("Ogiltigt status. Tillåtna värden: Confirmed, Cancelled, Pending.");
            }
            booking.NoOfGuests = dto.NumberOfGuests ?? booking.NoOfGuests;
            booking.SittingId = dto.SittingId ?? booking.SittingId;
            booking.Status = dto.Status ?? booking.Status;
            booking.Message = dto.Message ?? booking.Message;

            if (dto.BookingDate.HasValue)
            {
                booking.BookingDate = dto.BookingDate.Value.ToDateTime(TimeOnly.MinValue);
            }
            await _ctx.SaveChangesAsync();
            return await GetBookingByNumberAsync(bookingNumber);
        }
        public async Task DeleteBookingAsync(int bookingNumber)
        {
            var booking = await _ctx.Bookings
                .FirstOrDefaultAsync(b => b.BookingNumber == bookingNumber);
            if (booking == null)
            {
                throw new NotFoundException("Bokningen hittades inte.");
            }
            _ctx.Bookings.Remove(booking);
            await _ctx.SaveChangesAsync();
        }
    }
}