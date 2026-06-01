using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Stallstjarnornas.Library.Models;
using Stallstjarnornas.Test.TestHelpers;
using Stallstjarnornas.WebAPI.Data;
using Stallstjarnornas.WebAPI.DTOs.Booking;
using Stallstjarnornas.WebAPI.Interfaces;
using Stallstjarnornas.WebAPI.Services;

namespace Stallstjarnornas.Test.ServiceTest;

[TestClass]
public class BookingServiceTest
{
    private StallstjarnornasDbContext _ctx;
    private BookingService _service;
    private Mock<IGuestService> _mockGuestService;
    private Mock<IMailLogService> _mockMailService;

    [TestInitialize]
    public async Task Setup()
    {
        _ctx = DbContextFactory.CreateInMemoryContext();
        await TestDataHelper.SeedBasicDataAsync(_ctx);
        _mockGuestService = new Mock<IGuestService>();
        _mockMailService = new Mock<IMailLogService>();
        _service = new BookingService(_ctx, _mockGuestService.Object,_mockMailService.Object);
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
        var updatedBooking = await _ctx.Bookings
            .FirstOrDefaultAsync(b=>b.BookingNumber==1001);
        Assert.IsNotNull(updatedBooking);
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
    [TestMethod]
    public async Task FilterBookings_ByDate_ShouldReturnCorrectBookings()
    {
        // Arrange
        // Bokning 1001 och 1002 på 2026-06-01 finns redan via TestDataHelper 
        // Lägger till en bokning på ett annat datum för att verifiera filtreringen
        _ctx.Bookings.Add(new Booking
        {
            Id = 3,
            GuestId = 2,
            SittingId = 2,
            BookingDate = new DateTime(2026, 7, 1),
            NoOfGuests = 2,
            Status = "Confirmed",
            BookingNumber = 1003,
            CreatedDate = DateTime.Now
        });
        await _ctx.SaveChangesAsync();

        //Act - filtrerar på 2026-06-01
        var result = await _service.FilterBookingsAsync(
            status: null,
            date: new DateOnly(2026, 6, 1),
            sittingId: null,
            week: null,
            month: null,
            year: null,
            isPlaced: null,
            guestName: null,
            bookingNumber: null
        );

        //Assert
        //Ska retunera bokning 1001 och 1002 - inte 1003 som är bokat 2026-07-01
        Assert.AreEqual(2, result.Count());
        Assert.IsTrue(result.All(b => b.BookingDate == new DateOnly(2026, 6, 1)));
    }
    [TestMethod]
    public async Task FilterBookings_ByStatus_ShouldReturnCorrectBookings()
    {
        // Arrange
        // Bokning 1001 med Status "Confirmed" finns redan via TestDataHelper 
        // Bokning 1002 med Status "Cancelled" finns redan via TestDataHelper 

        // Act - filtrera på Confirmed
        var result = await _service.FilterBookingsAsync(
            status: "Confirmed",
            date: null,
            sittingId: null,
            week: null,
            month: null,
            year: null,
            isPlaced: null,
            guestName: null,
            bookingNumber: null
        );

        // Assert
        // Ska bara returnera bokning 1001 - inte 1002 som är Cancelled
        Assert.AreEqual(1, result.Count());
        Assert.IsTrue(result.All(b => b.Status == "Confirmed"));
    }
    [TestMethod]
    public async Task FilterBookings_ByIsPlaced_ShouldReturnCorrectBookings()
    {
        // Arrange
        // Bokning 1001 finns redan via TestDataHelper 
        // Lägger till en TableAssignment för bokning 1001 → IsPlaced = true
        _ctx.TableAssignments.Add(new TableAssignment
        {
            Id = 1,
            BookingId = 1,
            TableId = 1
        });
        await _ctx.SaveChangesAsync();

        // Act - filtrera på isPlaced = true
        var result = await _service.FilterBookingsAsync(
            status: null,
            date: null,
            sittingId: null,
            week: null,
            month: null,
            year: null,
            isPlaced: true,
            guestName: null,
            bookingNumber: null
        );

        // Assert
        // Ska bara returnera bokning 1001 som har en TableAssignment
        // Bokning 1002 har ingen TableAssignment → IsPlaced = false
        Assert.AreEqual(1, result.Count());
        Assert.IsTrue(result.All(b => b.IsPlaced == true));
    }
    [TestMethod]
    public async Task FilterBookings_NoFilter_ShouldReturnAllBookings()
    {
        // Arrange
        // Bokning 1001 och 1002 finns redan via TestDataHelper 

        // Act - inga filter
        var result = await _service.FilterBookingsAsync(
            status: null,
            date: null,
            sittingId: null,
            week: null,
            month: null,
            year: null,
            isPlaced: null,
            guestName: null,
            bookingNumber: null
        );

        // Assert
        // Ska returnera alla bokningar - 1001 och 1002
        Assert.AreEqual(2, result.Count());
    }
    [TestMethod]
    public async Task FilterBookings_ByMonthAndYear_ShouldReturnCorrectBookings()
    {
        // Arrange
        // Bokning 1001 och 1002 på juni 2026 finns redan via TestDataHelper 
        // Lägger till en bokning på juli 2026 för att verifiera filtreringen
        _ctx.Bookings.Add(new Booking
        {
            Id = 3,
            GuestId = 2,
            SittingId = 2,
            BookingDate = new DateTime(2026, 7, 1), // ← juli
            NoOfGuests = 2,
            Status = "Confirmed",
            BookingNumber = 1003,
            CreatedDate = DateTime.Now
        });
        await _ctx.SaveChangesAsync();

        // Act - filtrera på juni 2026
        var result = await _service.FilterBookingsAsync(
            status: null,
            date: null,
            sittingId: null,
            week: null,
            month: 6,
            year: 2026,
            isPlaced: null,
            guestName: null,
            bookingNumber: null
        );

        // Assert
        // Ska returnera bokning 1001 och 1002 - inte 1003 som är i juli
        Assert.AreEqual(2, result.Count());
        Assert.IsTrue(result.All(b => b.BookingDate.Month == 6 && b.BookingDate.Year == 2026));
    }
    [TestMethod]
    public async Task FilterBookings_ByWeekAndYear_ShouldReturnCorrectBookings()
    {
        // Arrange
        // Bokning 1001 och 1002 på 2026-06-01 finns redan via TestDataHelper 
        // 2026-06-01 är vecka 23 år 2026
        // Lägger till en bokning på en annan vecka för att verifiera filtreringen
        _ctx.Bookings.Add(new Booking
        {
            Id = 3,
            GuestId = 2,
            SittingId = 2,
            BookingDate = new DateTime(2026, 6, 15), // ← vecka 25
            NoOfGuests = 2,
            Status = "Confirmed",
            BookingNumber = 1003,
            CreatedDate = DateTime.Now
        });
        await _ctx.SaveChangesAsync();

        // Act - filtrera på vecka 23 år 2026
        var result = await _service.FilterBookingsAsync(
            status: null,
            date: null,
            sittingId: null,
            week: 23,
            month: null,
            year: 2026,
            isPlaced: null,
            guestName: null,
            bookingNumber: null
        );

        // Assert
        // Ska returnera bokning 1001 och 1002 - inte 1003 som är vecka 25
        Assert.AreEqual(2, result.Count());
        Assert.IsTrue(result.All(b =>
            System.Globalization.ISOWeek.GetWeekOfYear(
                b.BookingDate.ToDateTime(TimeOnly.MinValue)) == 23));
    }
    [TestMethod]
    public async Task DeleteBooking_ShouldRemoveBooking()
    {
        // Arrange
        // Bokning 1001 finns redan via TestDataHelper 

        // Act
        await _service.DeleteBookingAsync(1001);

        // Assert
        // Bokningen ska inte längre finnas i databasen
        var deletedBooking = await _ctx.Bookings
            .FirstOrDefaultAsync(b => b.BookingNumber == 1001);
        Assert.IsNull(deletedBooking);
    }

    [TestMethod]
    public async Task DeleteBooking_NotFound_ShouldThrowException()
    {
        // Arrange - bokning 9999 finns inte 

        // Act + Assert
        try
        {
            await _service.DeleteBookingAsync(9999);
            Assert.Fail("Skulle ha kastat ett exception");
        }
        catch (Exception ex)
        {
            Assert.AreEqual("Bokningen hittades inte.", ex.Message);
        }
    }
    [TestMethod]
    public async Task UpdateBooking_ShouldUpdateCorrectly()
    {
        // Arrange
        // Bokning 1001 finns redan via TestDataHelper ✅

        var dto = new UpdateBookingDto(
            BookingDate: null,
            NumberOfGuests: 4,
            SittingId: null,
            Status: "Confirmed",
            Message: "Uppdaterat meddelande"
        );

        // Act
        var result = await _service.UpdateBookingAsync(1001, dto);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(4, result.NumberOfGuests);
        Assert.AreEqual("Confirmed", result.Status);
        Assert.AreEqual("Uppdaterat meddelande", result.Message);
    }

    [TestMethod]
    public async Task UpdateBooking_NotFound_ShouldThrowException()
    {
        // Arrange - bokning 9999 finns inte ✅
        var dto = new UpdateBookingDto(
            BookingDate: null,
            NumberOfGuests: null,
            SittingId: null,
            Status: "Confirmed",
            Message: null
        );

        // Act + Assert
        try
        {
            await _service.UpdateBookingAsync(9999, dto);
            Assert.Fail("Skulle ha kastat ett exception");
        }
        catch (Exception ex)
        {
            Assert.AreEqual("Bokningen hittades inte.", ex.Message);
        }
    }

    [TestMethod]
    public async Task UpdateBooking_InvalidStatus_ShouldThrowException()
    {
        // Arrange - ogiltigt status
        var dto = new UpdateBookingDto(
            BookingDate: null,
            NumberOfGuests: null,
            SittingId: null,
            Status: "Felstavat",
            Message: null
        );

        // Act + Assert
        try
        {
            await _service.UpdateBookingAsync(1001, dto);
            Assert.Fail("Skulle ha kastat ett exception");
        }
        catch (Exception ex)
        {
            Assert.AreEqual("Ogiltigt status. Tillåtna värden: Confirmed, Cancelled, Pending.", ex.Message);
        }
    }
}