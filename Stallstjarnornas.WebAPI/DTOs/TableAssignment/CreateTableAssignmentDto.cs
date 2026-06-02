using System.ComponentModel.DataAnnotations;

namespace Stallstjarnornas.WebAPI.DTOs.TableAssignment
{
    public record CreateTableAssignmentDto(
        [Required]
        int bookingNumber,//Admin ska kunna assigna flera bord för en bokning
        //int BookingId,
        [Required]
        List<int> TableIds

        );
}
