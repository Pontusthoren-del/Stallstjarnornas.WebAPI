namespace Stallstjarnornas.WebAPI.DTOs.Booking
{
    public record BookingResponseDto(
        int Id,
        int BookingNumber,
        string GuestName,
        string GuestEmail,
        string GuestPhone,
        DateOnly BookingDate,
        TimeOnly SittingStartTime,
        TimeOnly SittingEndTime,
        int NumberOfGuests,
        string Status,
        string? Message,
        DateTime CreatedDate
        );
}
