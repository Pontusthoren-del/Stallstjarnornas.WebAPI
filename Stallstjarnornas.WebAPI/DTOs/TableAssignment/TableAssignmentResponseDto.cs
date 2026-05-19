using System.ComponentModel.DataAnnotations;

namespace Stallstjarnornas.WebAPI.DTOs.TableAssignment

{
    public record TableAssignmentResponseDto(
        List<int> TableIds,//Admin ska kunna assigna flera bord för en bokning
        int BookingId,
        string GuestName,
        int NoOfGuests,
        DateOnly BookingDate,
        int sittingId
        
    );
}

