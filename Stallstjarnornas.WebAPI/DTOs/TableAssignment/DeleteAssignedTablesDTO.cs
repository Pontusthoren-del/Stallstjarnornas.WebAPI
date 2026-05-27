using System.ComponentModel.DataAnnotations;

namespace Stallstjarnornas.WebAPI.DTOs.TableAssignment
{
    public record DeleteAssignedTablesDTO(
            [Required]
        int BookingId

            );

} 
