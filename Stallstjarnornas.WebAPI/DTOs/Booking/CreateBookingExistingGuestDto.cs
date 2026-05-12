using System.ComponentModel.DataAnnotations;

namespace Stallstjarnornas.WebAPI.DTOs.Booking
{
    public record CreateBookingExistingGuestDto(
        [Required]
        int GuestId,
        DateTime Date,
        int NumberOfGuests,
        int SittingId,
        string? Message
        );

}
