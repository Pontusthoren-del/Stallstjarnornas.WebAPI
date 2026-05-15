using Microsoft.AspNetCore.Mvc;
using Stallstjarnornas.WebAPI.DTOs.Booking;
using Stallstjarnornas.WebAPI.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _service;

    public BookingController(IBookingService service)
    {
        _service = service;
    }

    [HttpPost("Create-Booking")]
    public async Task<ActionResult<BookingResponseDto>> CreateBooking(CreateBookingDto dto)
    {
        var result = await _service.CreateBookingAsync(dto);
        return Ok(result);
    }

    [HttpPost("existing-guest")]
    public async Task<ActionResult<BookingResponseDto>> CreateBookingExistingGuest(CreateBookingExistingGuestDto dto)
    {
        var result = await _service.CreateBookingExistingGuestAsync(dto);
        return Ok(result);
    }
}