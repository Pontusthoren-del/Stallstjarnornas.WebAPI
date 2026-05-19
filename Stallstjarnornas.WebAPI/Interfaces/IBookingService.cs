using Stallstjarnornas.WebAPI.DTOs.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stallstjarnornas.WebAPI.Interfaces
{
    public interface IBookingService
    {
        // Gäst
        Task<BookingResponseDto> CreateBookingAsync(CreateBookingDto dto);
        Task<BookingResponseDto> GetBookingByNumberAsync(int bookingNumber);
        Task CancelBookingAsync(int bookingNumber);
        Task<BookingResponseDto> RebookBookingAsync(int bookingNumber, UpdateBookingDto dto);

        // Admin
        Task<IEnumerable<BookingResponseDto>> GetAllBookingsAsync();
        Task<BookingResponseDto> UpdateBookingAsync(int id, UpdateBookingDto dto);
        Task DeleteBookingAsync(int id);
        Task<IEnumerable<BookingResponseDto>> FilterBookingsAsync(string? status, DateOnly? date, int? sittingId, int? week, int? month, int? year,bool? isPlaced);
    }
}
