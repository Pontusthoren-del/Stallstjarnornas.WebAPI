

namespace Stallstjarnornas.WebAPI.DTOs.Guest
{
    public record GuestDto(

        int Id,
        string Name,
        string Phone,
        string Email,
        string? Message
    );

}
