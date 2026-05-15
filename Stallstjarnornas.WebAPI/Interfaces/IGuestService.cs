using Stallstjarnornas.WebAPI.DTOs.Guest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stallstjarnornas.WebAPI.Interfaces
{
    public interface IGuestService
    {
        
            Task<GuestDto?> GetGuestByIdAsync(int id);
        
    }
}
