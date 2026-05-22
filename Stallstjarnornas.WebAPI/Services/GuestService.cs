using Stallstjarnornas.Library.Models;

using Stallstjarnornas.WebAPI.Data;
using Stallstjarnornas.WebAPI.DTOs.Guest;
using Stallstjarnornas.WebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;

namespace Stallstjarnornas.WebAPI.Services
{

    public class GuestService : IGuestService
    {
        private readonly StallstjarnornasDbContext _context;

        public GuestService(StallstjarnornasDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteGuestAsync(int id)
        {
            var guest = await _context.Guests.FindAsync(id);
            if (guest == null) return false;

            _context.Guests.Remove(guest);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<GuestDto>> GetAllGuestsAsync()
        {
            return await _context.Guests
                .Select(g => new GuestDto(
                    g.Id,
                    g.Name,
                    g.Phone,
                    g.Email
                    ))
                .ToListAsync();
        }

        public async Task<GuestDto?> GetGuestByIdAsync(int id)
        {
            var guest = await _context.Guests.FindAsync(id);
            if (guest == null) return null;

            return new GuestDto(
                guest.Id,
                guest.Name,
                guest.Phone,
                guest.Email
            );
        }

        public async Task<Guest?> GetGuestEntityByEmailAsync(string email)
        {
            return await _context.Guests
                .FirstOrDefaultAsync(g => g.Email == email);
        }

        public async Task<GuestDto?> RegisterGuestAsync(CreateGuestDto dto)
        {
            var guest = await GetGuestEntityByEmailAsync(dto.Email);
            if (guest != null) return null;

            var newGuest = new Guest
            {
                Name = dto.Name,
                Phone = dto.Phone,
                Email = dto.Email
            };
            _context.Guests.Add(newGuest);
            await _context.SaveChangesAsync();

            return new GuestDto(
                newGuest.Id,
                newGuest.Name,
                newGuest.Phone,
                newGuest.Email
            );
        }

        public async Task<GuestDto?> UpdateGuestAsync(int id, UpdateGuestDto dto)
        {
            var guest = await _context.Guests.FindAsync(id);

            if (guest == null) return null;

            guest.Name = dto.Name;
            guest.Phone = dto.Phone;
            guest.Email = dto.Email;

            await _context.SaveChangesAsync();

            return new GuestDto(
                guest.Id,
                guest.Name,
                guest.Phone,
                guest.Email
                );
        }



    }
}
