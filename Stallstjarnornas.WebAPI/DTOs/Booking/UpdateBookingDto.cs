namespace Stallstjarnornas.WebAPI.DTOs.Booking
{
    public record UpdateBookingDto(
        DateOnly? BookingDate,
        int? NumberOfGuests,
        int? SittingId,
        string? Status,
        string? Message
    );
}
