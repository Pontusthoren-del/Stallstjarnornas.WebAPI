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
        //Bokningen 1001 finns redan via TestDataHelper

        // Act
        await _service.CancelBookingAsync(1001);

        // Assert
        var updatedBooking = await _ctx.Bookings.FindAsync(1);
        Assert.AreEqual("Cancelled", updatedBooking.Status);
    }

    [TestMethod]
    public async Task CancelBooking_AlreadyCancelled_ShouldThrowException()
    {

        //Arrange
        //Bokningen 1002 finns redan och är Cancelled via TestDataHelper

        //Act+Assert
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
        // Arrange - bokning 9999 finns inte i databasen 

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
        // Bokningen 1001 finns redan via TestDataHelper

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

        // Sittning 1 finns redan via TestDataHelper 
        // ny@test.com finns INTE i databasen - testar att ny gäst skapas
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
        // Hämtar Anna (Id=1) som redan finns i InMemory-databasen via TestDataHelper.
        // Detta är viktigt eftersom EF Core redan trackar entityn.
        // Om vi istället hade skapat en ny Guest med samma Id
        // hade EF Core kastat ett tracking-fel.
        var existingGuest = await _ctx.Guests.FindAsync(1);

        // Mockar IGuestService så att den returnerar den befintliga gästen
        // när bokningen görs med Annas emailadress.
        // Detta simulerar att gästen redan finns registrerad i systemet.
        _mockGuestService
            .Setup(g => g.GetGuestEntityByEmailAsync("anna@test.com"))
            .ReturnsAsync(existingGuest);

        // DTO som simulerar en bokning från en redan registrerad gäst.
        // Sittning 1 finns redan via TestDataHelper 
        var dto = new CreateBookingDto(
            Name: "Anna Lindqvist",
            Phone: "070-123 45 67",
            Email: "anna@test.com",
            NumberOfGuests: 2,
            BookingDate: new DateOnly(2026, 6, 1),
            SittingId: 1,
            Message: null
        );

        // Act
        // Kör den riktiga CreateBookingAsync med en redan registrerad gäst
        var result = await _service.CreateBookingAsync(dto);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Anna Lindqvist", result.GuestName);

        // Verifierar att det fortfarande bara finns EN gäst med Annas email i databasen.
        // Detta bevisar att BookingService återanvände den befintliga gästen
        // istället för att skapa en dubblett.
        var guestCount = _ctx.Guests.Count(g => g.Email == "anna@test.com");
        Assert.AreEqual(1, guestCount);
    }

    [TestMethod]
    public async Task CreateBooking_SittingFull_ShouldThrowExepction()
    {
        //Arrange
        //Mockar GetGuestEntityByEmailAsync - retunerar null(ny gäst)
        _mockGuestService
            .Setup(g => g.GetGuestEntityByEmailAsync("ny@test.com"))
            .ReturnsAsync((Guest?)null);

        //Fyller upp sittningen 1 med 50 gäster (max)
        //Använder mig av ID 3 här
        //För i TestDataHelper så har vi redan 2 bokningar med ID 1 o 2 
        //Annars blir det fel.
        _ctx.Bookings.Add(new Booking
        {
            Id = 3,
            GuestId = 1,
            SittingId = 1,
            BookingDate = new DateTime(2026, 6, 1),
            NoOfGuests = 50, // ← fullt!
            Status = "Confirmed",
            BookingNumber = 1001,
            CreatedDate = DateTime.Now
        });
        await _ctx.SaveChangesAsync();

        //Här försöker jag då boka på 2 nya gäster till på första sittningen
        //Då ska jag få tillbaka felmeddelandet att det är fullt
        //Och det får jag
        //Testet är OK!
        var dto = new CreateBookingDto(
            Name: "Ny Person",
            Phone: "070-000 00 00",
            Email: "ny@test.com",
            NumberOfGuests: 2,
            BookingDate: new DateOnly(2026, 6, 1),
            SittingId: 1,
            Message: null
        );

        //Act + Assert
        try
        {
            await _service.CreateBookingAsync(dto);
            Assert.Fail("Skulle ha kastat ett execption");
        }
        //Viktigt att man skriver EXAKT likadant som i sin metod.
        //MISSADE en punkt.. Så då blev testet fel först
        //Men nu funka det med en punkt... 
        catch (Exception ex)
        {
            Assert.AreEqual("Sittningen är fullbokad.", ex.Message);
        }
    }

    [TestMethod]
    public async Task CreateBooking_SittingNotFound_ShouldThrowExepction()
    {
        //Arrange
        _mockGuestService
            .Setup(g => g.GetGuestEntityByEmailAsync("ny@test.com"))
            .ReturnsAsync((Guest?)null);

        var dto = new CreateBookingDto(
            Name: "Ny Person",
            Phone: "070-000 00 00",
            Email: "ny@test.com",
            NumberOfGuests: 2,
            BookingDate: new DateOnly(2026, 6, 1),
            SittingId: 99,
            Message: null
            );

        //Act+Assert
        try
        {
            await _service.CreateBookingAsync(dto);
            Assert.Fail("Skulle ha kastat ett exception");
        }
        catch (Exception ex)
        {
            Assert.AreEqual("Sittningen finns inte.", ex.Message);
        }
    }
}