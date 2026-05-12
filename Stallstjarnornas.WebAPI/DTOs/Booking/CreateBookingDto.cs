using System.ComponentModel.DataAnnotations;

namespace Stallstjarnornas.WebAPI.DTOs.Booking
{
    public record CreateBookingDto(
        [Required, MinLength(2)]
        string Name,
        [Required,Phone]
        string Phone,
        [Required, RegularExpression(
  @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
         ErrorMessage = "Ange en giltig e-postadress")]
        string Email,
        [Required, Range(1, 8, ErrorMessage = "Max 8 personer kan bokas online. Är ni fler, kontakta oss direkt!")]
        int NumberOfGuests,
        [Required]
        DateOnly BookingDate,
        [Required]
        int SittingId,
        string? Message
    );
}