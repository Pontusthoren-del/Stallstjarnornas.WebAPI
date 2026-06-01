using System.ComponentModel.DataAnnotations;

namespace Stallstjarnornas.WebAPI.DTOs.Booking
{
    public record UpdateBookingDto(
        DateOnly? BookingDate,
        [Range(1, 8, ErrorMessage = "Max 8 personer kan bokas online. Är ni fler, kontakta oss direkt!")]
        int? NumberOfGuests,
        int? SittingId,
        string? Status,
        string? Message
    );
}
