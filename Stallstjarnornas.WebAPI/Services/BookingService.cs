using Microsoft.EntityFrameworkCore;
using Stallstjarnornas.WebAPI.Data;
using Stallstjarnornas.WebAPI.DTOs.Booking;
using Stallstjarnornas.WebAPI.Interfaces;
using Stallstjarnornas.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stallstjarnornas.WebAPI.Services
{
    public class BookingService : IBookingService
    {

        private readonly StallstjarnornasDbContext _ctx;
        private readonly IGuestService _guestService;

        public BookingService(StallstjarnornasDbContext ctx, IGuestService guestService)
        {
            _ctx = ctx;
            _guestService = guestService;
        }

        public async Task CancelBookingAsync(int bookingNumber)
        {
            var booking = await _ctx.Bookings
                .FirstOrDefaultAsync(b=>b.BookingNumber == bookingNumber);
            if (booking == null)
            {
                throw new Exception("Bokning hittades inte.");
            }
            if (booking.Status == "Cancelled")  
            {
                throw new Exception("Bokningen är redan avbokad.");
            }

            booking.Status = "Cancelled";
            await _ctx.SaveChangesAsync();
        }

        public async Task<BookingResponseDto> CreateBookingAsync(CreateBookingDto dto)
        {
            var guest = await _guestService.GetGuestEntityByEmailAsync(dto.Email);

            if (guest == null)
            {
                guest = await _guestService.CreateGuestAsync(dto.Name, dto.Phone, email: dto.Email);
            }
            //Ser till att jag hämtar bara det jag behöver från Sitting
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

            //Här ser jag först så sittningen finns, och sen om sittningen är full.
            if (sittingInfo == null)
            {
                throw new Exception("Sittningen finns inte.");
            }
            if (sittingInfo.BookedGuests + dto.NumberOfGuests > sittingInfo.MaxGuests)
            {
                throw new Exception("Sittningen är fullbokad.");
            }

            //Bookingtabellen -> hittar högsta bokningsnumret ->om tabellen är TOM(null) - börja på 1000.
            var maxBookingNumber = await _ctx.Bookings
                .MaxAsync(b => (int?)b.BookingNumber) ?? 1000;
            //Här skapas bokningen
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
            //Här retuneras bokningen med rätt info
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


        public Task DeleteBookingAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BookingResponseDto>> FilterBookingsAsync(
        string? status, DateOnly? date, int? sittingId, int? week, int? month, int? year, bool? isPlaced)
        {
            //här hämtar vi alla bokningar som en "query" - men inget skickas till databasen än
            var query = _ctx.Bookings.AsQueryable();

            // Dessa körs i SQL
            if (date.HasValue)
            {
                query = query.Where(b =>
                    b.BookingDate == date.Value.ToDateTime(TimeOnly.MinValue));
            }
            if (sittingId.HasValue)
            {
                query = query.Where(b => b.SittingId == sittingId.Value);
            }
            if (status != null)
            {
                query = query.Where(b => b.Status == status);
            }
            // Månad + år körs i SQL
            if (month.HasValue && year.HasValue)
            {
                query = query.Where(b =>
                    b.BookingDate.Month == month.Value &&
                    b.BookingDate.Year == year.Value);
            }
            // Bara år utan vecka körs i SQL
            else if (year.HasValue && !week.HasValue)
            {
                query = query.Where(b => b.BookingDate.Year == year.Value);
            }
            // HÄR skickas allt till databasen i ett enda anrop!
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
                    IsPlaced = b.TableAssignments.Any()
                })
                .ToListAsync();

            // Vecka filtreras i minnet med ISOWeek - för den kan inte köras i SQL
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
            if (isPlaced.HasValue)
            {
                result = result.Where(b => b.IsPlaced == isPlaced.Value).ToList();
            }

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
        public Task<IEnumerable<BookingResponseDto>> GetAllBookingsAsync()
        {
            throw new NotImplementedException();
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
                throw new Exception("Bokningen hittades inte");

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

        public Task<BookingResponseDto> RebookBookingAsync(int bookingNumber, UpdateBookingDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<BookingResponseDto> UpdateBookingAsync(int id, UpdateBookingDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
