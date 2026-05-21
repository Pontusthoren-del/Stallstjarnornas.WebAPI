using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Stallstjarnornas.Library.Models;
using Stallstjarnornas.Test.TestHelpers;
using Stallstjarnornas.WebAPI.Data;
using Stallstjarnornas.WebAPI.DTOs.Booking;
using Stallstjarnornas.WebAPI.Interfaces;
using Stallstjarnornas.WebAPI.Services;

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

    [TestMethod]
    public async Task CreateBooking_NewGuest_ShouldCreateBookingAndReturnDto()
    {
        // ARRANGE
        // Mockar GetGuestEntityByEmailAsync - returnerar null eftersom gästen inte finns sedan tidigare
        _mockGuestService
            .Setup(g => g.GetGuestEntityByEmailAsync("ny@test.com"))
            .ReturnsAsync((Guest?)null);

        
        // Skapar en DTO med bokningsinformation som en ny gäst skulle skicka in
        // Sittning 1 finns redan i databasen via TestDataHelper
        var dto = new CreateBookingDto(
            Name: "Ny Person",
            Phone: "070-000 00 00",
            Email: "ny@test.com",
            NumberOfGuests: 2,
            BookingDate: new DateOnly(2026, 6, 1),
            SittingId: 1,
            Message: null
            );

        // ACT
        // Kör den riktiga CreateBookingAsync-metoden med vår testdata
        var result = await _service.CreateBookingAsync(dto);


        // ASSERT
        // Kontrollerar att resultatet inte är null
        Assert.IsNotNull(result);
        // Kontrollerar att gästnamnet är korrekt
        Assert.AreEqual("Ny Person", result.GuestName);
        // Kontrollerar att antal gäster stämmer
        Assert.AreEqual(2, result.NumberOfGuests);
        // En ny bokning ska alltid ha status Pending
        Assert.AreEqual("Pending", result.Status);
        // En ny bokning är aldrig placerad vid ett bord
        Assert.IsFalse(result.IsPlaced);
    }

    [TestMethod]
    public async Task CreateBooking_ExistingGuest_ShouldReuseGuest()
    {
        // Arrange
        // Hämtar en redan existerande gäst från InMemory-databasen.
        // Detta är viktigt eftersom EF Core redan trackar entityn.
        // Om vi istället hade skapat en ny Guest med samma Id
        // hade EF Core kastat ett tracking-fel.

        var existingGuest = await _ctx.Guests.FindAsync(1);

        // Mockar GuestService så att den returnerar den befintliga gästen
        // när bokningen görs med samma emailadress.
        _mockGuestService
            .Setup(g => g.GetGuestEntityByEmailAsync("anna@test.com"))
             .ReturnsAsync(existingGuest); 

        // DTO som simulerar en bokning från en redan registrerad gäst.
        var dto = new CreateBookingDto(
            Name: "Anna Lindqvist",
            Phone: "070-123 45 67",
            Email: "anna@test.com",
            NumberOfGuests: 2,
            BookingDate: new DateOnly(2026, 6, 1),
            SittingId: 1,
            Message: null
        );

        //Act
        var result = await _service.CreateBookingAsync(dto);

        //Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Anna Lindqvist", result.GuestName);


        // Verifierar att det fortfarande bara finns EN gäst med Annas email i databasen
        // Detta bevisar att BookingService återanvände den befintliga gästen istället för att skapa en dubblett
        var guestCount = _ctx.Guests.Count(g => g.Email == "anna@test.com");
        Assert.AreEqual(1, guestCount);
        
    }
}