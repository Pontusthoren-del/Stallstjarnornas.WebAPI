namespace Stallstjarnornas.WebAPI.DTOs.TableAssignment

{
    public record TableAssignmentResponseDto(
        int Id,
        int TableId,
        int BookingNumber,
        string GuestName,
        int NoOfGuests,
        DateOnly BookingDate,
        TimeOnly SittingStartTime,
        TimeOnly SittingEndTime
    );
}

