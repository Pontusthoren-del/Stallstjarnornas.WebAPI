using Moq;
using Stallstjarnornas.Library.Models;
using Stallstjarnornas.Test.TestHelpers;
using Stallstjarnornas.WebAPI.Data;
using Stallstjarnornas.WebAPI.Interfaces;
using Stallstjarnornas.WebAPI.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Stallstjarnornas.Test;

[TestClass]
public class BookingServiceTest
{
    private StallstjarnornasDbContext _ctx;
    private BookingService _service;
    private Mock<IGuestService> _mockGuestService;

    [TestInitialize]
    public async Task Setup()
    {
        _ctx = DbContextFactory.CreateInMemoryContext();
        await TestDataHelper.SeedBasicDataAsync(_ctx);
        _mockGuestService = new Mock<IGuestService>();
        _service = new BookingService(_ctx, _mockGuestService.Object);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _ctx.Dispose();
    }

    [TestMethod]
    public async Task CancelBooking_ShouldSetStatusToCancelled()
    {
        // Arrange
        var booking = new Booking
        {
            Id = 1,
            GuestId = 1,
            SittingId = 1,
            BookingDate = new DateTime(2026, 6, 1),
            NoOfGuests = 2,
            Status = "Confirmed",
            BookingNumber = 1001,
            CreatedDate = DateTime.Now
        };
        _ctx.Bookings.Add(booking);
        await _ctx.SaveChangesAsync();

        // Act
        await _service.CancelBookingAsync(1001);

        // Assert
        var updatedBooking = await _ctx.Bookings.FindAsync(1);
        Assert.AreEqual("Cancelled", updatedBooking.Status);
    }

    [TestMethod]
    public async Task CancelBooking_AlreadyCancelled_ShouldThrowException()
    {
        var booking = new Booking
        {
            Id = 2,
            GuestId = 1,
            SittingId = 1,
            BookingDate = new DateTime(2026, 6, 1),
            NoOfGuests = 2,
            Status = "Cancelled",
            BookingNumber = 1002,
            CreatedDate = DateTime.Now
        };
        _ctx.Bookings.Add(booking);
        await _ctx.SaveChangesAsync();

        try
        {
            await _service.CancelBookingAsync(1002);
            Assert.Fail("Skulle ha kastat ett exception");
        }
        catch (Exception ex)
        {
            Assert.AreEqual("Bokningen är redan avbokad.", ex.Message);
        }
    }

    [TestMethod]
    public async Task CancelBooking_NotFound_ShouldThrowException()
    {
        try
        {
            await _service.CancelBookingAsync(9999);
            Assert.Fail("Skulle ha kastat ett exception");
        }
        catch (Exception ex)
        {
            Assert.AreEqual("Bokning hittades inte.", ex.Message);
        }
    }

    [TestMethod]
    public async Task GetBookingByNumber_ShouldReturnCorrectBooking()
    {
        // Arrange
        var booking = new Booking
        {
            Id = 1,
            GuestId = 1,
            SittingId = 1,
            BookingDate = new DateTime(2026, 6, 1),
            NoOfGuests = 2,
            Status = "Confirmed",
            BookingNumber = 1001,
            CreatedDate = DateTime.Now,
            Message = "Glutenallergi"
        };
        _ctx.Bookings.Add(booking);
        await _ctx.SaveChangesAsync();

        // Act
        var result = await _service.GetBookingByNumberAsync(1001);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1001, result.BookingNumber);
        Assert.AreEqual("Anna Lindqvist", result.GuestName);
        Assert.AreEqual(2, result.NumberOfGuests);
        Assert.AreEqual("Confirmed", result.Status);
        Assert.AreEqual("Glutenallergi", result.Message);
        Assert.IsFalse(result.IsPlaced);
    }
}