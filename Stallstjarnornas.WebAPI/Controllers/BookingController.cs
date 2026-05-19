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

    [HttpPost]
    public async Task<ActionResult<BookingResponseDto>> CreateBooking(CreateBookingDto dto)
    {
        var result = await _service.CreateBookingAsync(dto);
        return Ok(result);
    }

    [HttpGet("{bookingNumber}")]
    public async Task<ActionResult<BookingResponseDto>> GetBookingByNumber(int bookingNumber)
    {
        try
        {
            var result = await _service.GetBookingByNumberAsync(bookingNumber);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookingResponseDto>>> FilterBookings(
    [FromQuery] string? status,
    [FromQuery] DateOnly? date,
    [FromQuery] int? sittingId,
    [FromQuery] int? week,
    [FromQuery] int? month,
    [FromQuery] int? year)
    {
        try
        {
            var result = await _service.FilterBookingsAsync(status, date, sittingId, week, month, year);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}