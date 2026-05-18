using Microsoft.AspNetCore.Mvc;

using Stallstjarnornas.WebAPI.DTOs.Guest;
using Stallstjarnornas.WebAPI.Interfaces;

namespace Stallstjarnornas.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestController : ControllerBase
    {
        private readonly IGuestService _service;

        public GuestController(IGuestService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GuestDto>> GetGuest(int id)
        {
            var guest = await _service.GetGuestByIdAsync(id);
            if (guest == null) return NotFound();
            return Ok(guest);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GuestDto>>> GetAllGuests()
        {
            var guests = await _service.GetAllGuestsAsync();
            return Ok(guests);
        }
    }
}
