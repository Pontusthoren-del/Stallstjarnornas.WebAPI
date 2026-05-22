using Stallstjarnornas.WebAPI.DTOs.Guest;
using Stallstjarnornas.Library.Models;
using Stallstjarnornas.WebAPI.DTOs.Booking;


namespace Stallstjarnornas.WebAPI.Interfaces
{
    public interface IGuestService
    {
        Task<GuestDto?> UpdateGuestAsync(int id, UpdateGuestDto dto);
        Task<GuestDto?> GetGuestByIdAsync(int id);
        Task<IEnumerable<GuestDto>> GetAllGuestsAsync();
        Task<Guest?> GetGuestEntityByEmailAsync(string email);
        Task<GuestDto?> RegisterGuestAsync(CreateGuestDto dto);
        Task<bool> DeleteGuestAsync(int id);
    }
}
