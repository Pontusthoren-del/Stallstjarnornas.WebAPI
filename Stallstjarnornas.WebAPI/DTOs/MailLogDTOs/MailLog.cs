using Stallstjarnornas.Library.Models;
using System.ComponentModel.DataAnnotations;

namespace Stallstjarnornas.WebAPI.DTOs.MailLogDTOs
{
    public record CreateEmailLogPullRequest
    {
        [MinLength(2)] string? GuestName;
        [Phone] string? PhoneNo;
        [Required, RegularExpression(@"^[^@\s]+@[^@\s]+.[^@\s]+$", ErrorMessage = "Ange en giltig e-postadress")] string Email;
        [Required] Booking Booking;
        [Required] MailLog MailType;//Mailtyp skiftar beroende på bokning/avbokning/ombokning
    }
}
