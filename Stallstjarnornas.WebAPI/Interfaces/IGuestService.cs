using Stallstjarnornas.WebAPI.DTOs.Guest;
using Stallstjarnornas.Library.Models;


namespace Stallstjarnornas.WebAPI.Interfaces
{
    public interface IGuestService
    {
        Task<GuestDto?> UpdateGuestAsync(int id, UpdateGuestDto dto);
        Task<GuestDto?> GetGuestByIdAsync(int id);
        Task<IEnumerable<GuestDto>> GetAllGuestsAsync();
        Task<Guest?> GetGuestEntityByEmailAsync(string email);

    }
}
