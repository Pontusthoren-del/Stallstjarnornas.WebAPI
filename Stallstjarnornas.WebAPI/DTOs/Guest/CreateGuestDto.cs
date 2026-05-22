using System.ComponentModel.DataAnnotations;

namespace Stallstjarnornas.WebAPI.DTOs.Guest
{
    public record CreateGuestDto
    (
        [Required, MinLength(2)] string Name,
        [Required, Phone] string Phone,
        [Required, RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        ErrorMessage = "Ange en giltig e-postadress")] string Email
    );

}
