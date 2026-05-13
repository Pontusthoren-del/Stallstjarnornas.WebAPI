namespace Stallstjarnornas.WebAPI.DTOs.SittingDTOs
{
    public record SittingDto(
        
        int Id,
        int OperatingDayId,
        TimeOnly StartTime,
        TimeOnly EndTime,
        int MaxGuest,
        int CurrentBookings, //logig i service
        int AvailebleSeats  //logig i service
        );
    
    
}
