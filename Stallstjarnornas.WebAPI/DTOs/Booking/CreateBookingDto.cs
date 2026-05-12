using System.ComponentModel.DataAnnotations;

namespace Stallstjarnornas.WebAPI.DTOs.Booking
{
    public record CreateBookingDto(
        [Required, MinLength(2)]
        string Name,
        [Required]
        string Phone,
        [Required, EmailAddress]
        string Email,
        [Required, Range(1, 8, ErrorMessage = "Max 8 personer kan bokas online. Är ni fler, kontakta oss direkt!")]
        int NoOfGuests,
        [Required]
    DateTime BookingDate,
        [Required]
        int SittingId,
        string? Message
    )
    {

    };
}