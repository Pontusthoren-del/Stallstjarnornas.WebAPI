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

        [HttpPut("{id}")]
        public async Task<ActionResult<GuestDto>> UpdateGuest(int id, UpdateGuestDto dto)
        {
            var guest = await _service.UpdateGuestAsync(id, dto);
            if (guest == null) return NotFound();
            return Ok(guest);
        }

        [HttpPost]
        public async Task<ActionResult<GuestDto>> RegisterGuest(CreateGuestDto dto)
        {
            var guest = await _service.RegisterGuestAsync(dto);
            if (guest == null) return Conflict("En gäst med denna email finns redan");
            return CreatedAtAction(nameof(GetGuest), new { id = guest.Id }, guest);

        }

    }
}
