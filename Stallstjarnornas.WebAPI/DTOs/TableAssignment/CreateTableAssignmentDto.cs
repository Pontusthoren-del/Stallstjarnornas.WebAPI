using System.ComponentModel.DataAnnotations;

namespace Stallstjarnornas.WebAPI.DTOs.TableAssignment
{
    public record CreateTableAssignmentDto(
        [Required]
        int BookingId,
        [Required]
        int TableId
        );
}
