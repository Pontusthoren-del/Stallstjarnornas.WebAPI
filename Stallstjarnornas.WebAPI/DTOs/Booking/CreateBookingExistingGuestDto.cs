using System.ComponentModel.DataAnnotations;

namespace Stallstjarnornas.WebAPI.DTOs.Booking
{
    public record CreateBookingExistingGuestDto(
        [Required]
        int GuestId,
        DateOnly Date,
        int NumberOfGuests,
        int SittingId,
        string? Message
        );

}
