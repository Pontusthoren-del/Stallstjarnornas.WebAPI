using Microsoft.AspNetCore.Mvc;
using Stallstjarnornas.WebAPI.DTOs.Booking;
using Stallstjarnornas.WebAPI.Interfaces;
using System.Threading.Tasks;

namespace Stallstjarnornas.WebAPI.Controllers
{
    public class BookingController:ControllerBase
    {
        [HttpPost]
        public async Task Task<ActionResult>(CreateBookingDto newBooking)
        {
            //var result = await 
        }
    }
}
