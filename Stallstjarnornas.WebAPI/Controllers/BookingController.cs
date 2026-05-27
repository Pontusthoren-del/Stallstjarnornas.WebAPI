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
        try
        {
            var result = await _service.CreateBookingAsync(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("BookingNumber/{bookingNumber}")]
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
    [HttpGet("Filter-Booking")]
    public async Task<ActionResult<IEnumerable<BookingResponseDto>>> FilterBookings(
    [FromQuery] string? status,
    [FromQuery] DateOnly? date,
    [FromQuery] int? sittingId,
    [FromQuery] int? week,
    [FromQuery] int? month,
    [FromQuery] int? year,
    [FromQuery] bool? isPlaced)
    {
        try
        {
            var result = await _service.FilterBookingsAsync(status, date, sittingId, week, month, year, isPlaced);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("Cancel/{bookingNumber}")]
    public async Task<ActionResult> CancelBooking(int bookingNumber)
    {
        try
        {
            await _service.CancelBookingAsync(bookingNumber);
            return Ok("Bokningen är avbokad.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpDelete("Delete/{bookingNumber}")]
    public async Task<IActionResult> DeleteBooking(int bookingNumber)
    {
        try
        {
            await _service.DeleteBookingAsync(bookingNumber);
            return Ok($"{bookingNumber} är bortagen!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPut("Update/{bookingNumber}")]
    public async Task<IActionResult> UpdateBooking(int bookingNumber, UpdateBookingDto dto)
    {
        try
        {
            var result = await _service.UpdateBookingAsync(bookingNumber, dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}