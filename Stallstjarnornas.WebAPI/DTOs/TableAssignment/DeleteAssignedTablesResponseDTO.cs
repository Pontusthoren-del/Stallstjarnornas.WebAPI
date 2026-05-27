using System.ComponentModel.DataAnnotations;

namespace Stallstjarnornas.WebAPI.DTOs.TableAssignment
{
    public record DeleteAssignedTablesResponseDTO
        (
            [Required]
        int BookingId,
            [Required]
        List<int> TableIds//Admin ska kunna assigna flera bord för en bokning

            );

}
