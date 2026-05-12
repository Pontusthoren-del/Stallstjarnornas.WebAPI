using Stallstjarnornas.Library.Models;
using System.ComponentModel.DataAnnotations;

namespace Stallstjarnornas.WebAPI.DTOs.MailLogDTOs
{
    public record CreateEmailConfirmationRequest
    {
        [Required] string GuestName;
        [Required, Phone] string PhoneNo;
        [Required, EmailAddress] string Email;
        [Required] Booking Booking;
        [Required] MailLog MailType;//Mailtyp skiftar beroende på bokning/avbokning/ombokning


    }
}
