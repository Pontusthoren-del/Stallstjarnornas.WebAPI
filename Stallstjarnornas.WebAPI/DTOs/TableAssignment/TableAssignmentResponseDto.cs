using System.ComponentModel.DataAnnotations;

namespace Stallstjarnornas.WebAPI.DTOs.TableAssignment

{
    public record TableAssignmentResponseDto(
        int Id,
        
        List<int> TableIds,//Admin ska kunna assigna flera bord för en bokning
        int BookingNumber,
        string GuestName,
        int NoOfGuests,
        DateOnly BookingDate,
        TimeOnly SittingStartTime,
        TimeOnly SittingEndTime
    );
}

