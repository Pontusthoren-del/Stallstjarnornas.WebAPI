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

namespace Stallstjarnornas.WebAPI.Services
{

    public class GuestService : IGuestService
    {
        private readonly StallstjarnornasDbContext _context;

        public GuestService(StallstjarnornasDbContext context)
        {
            _context = context;
        }

        public async Task<Guest> CreateGuestAsync(string name, string phone, string email)
        {
            var guest = new Guest
            {
                Name = name,
                Phone = phone,
                Email = email
            };

            _context.Guests.Add(guest);
            return guest;
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
    }
}
