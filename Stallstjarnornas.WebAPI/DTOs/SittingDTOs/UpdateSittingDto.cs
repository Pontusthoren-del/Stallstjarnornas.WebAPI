using System.ComponentModel.DataAnnotations;

namespace Stallstjarnornas.WebAPI.DTOs.SittingDTOs
{
    public record UpdateSittingDto
    (
        [Required] TimeOnly StartTime,
        [Required] TimeOnly EndTime
    );
}
