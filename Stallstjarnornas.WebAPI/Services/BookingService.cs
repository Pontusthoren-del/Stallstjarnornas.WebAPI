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

        public BookingService(StallstjarnornasDbContext ctx)
        {
            _ctx = ctx;
        }

        public Task CancelBookingAsync(int bookingNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<BookingResponseDto> CreateBookingAsync(CreateBookingDto dto)
        {

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

            var guest = await _ctx.Guests
                .FirstOrDefaultAsync(g => g.Email == dto.Email);

            //Bookingtabellen -> hittar högsta bokningsnumret ->om tabellen är TOM(null) - börja på 1000.
            var maxBookingNumber = await _ctx.Bookings
                .MaxAsync(b => (int?)b.BookingNumber) ?? 1000;

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

            return new BookingResponseDto(
                Id: booking.Id,
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
                CreatedDate: booking.CreatedDate
            );
        }

        public Task<BookingResponseDto> CreateBookingExistingGuestAsync(CreateBookingExistingGuestDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBookingAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BookingResponseDto>> FilterBookingsAsync(string? status, DateOnly? date, int? sittingId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BookingResponseDto>> GetAllBookingsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BookingResponseDto> GetBookingByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BookingResponseDto> GetBookingByNumberAsync(int bookingNumber)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BookingResponseDto>> GetBookingsByDateAsync(DateOnly date)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BookingResponseDto>> GetBookingsByMonthAsync(int year, int month)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BookingResponseDto>> GetBookingsBySittingAsync(int sittingId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BookingResponseDto>> GetBookingsByWeekAsync(DateOnly weekStart)
        {
            throw new NotImplementedException();
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
