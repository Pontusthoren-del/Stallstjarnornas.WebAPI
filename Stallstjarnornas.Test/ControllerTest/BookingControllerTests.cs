using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Stallstjarnornas.WebAPI.DTOs.Booking;
using Stallstjarnornas.WebAPI.Interfaces;

namespace Stallstjarnornas.Test.ControllerTest;

[TestClass]
public class BookingControllerTests
{
    // Mockad version av servicen.
    // Vi använder den för att kontrollera vad servicen ska returnera.
    private Mock<IBookingService> _serviceMock = null!;

    // Själva controllern vi testar.
    private BookingController _controller = null!;

    [TestInitialize]
    public void Setup()
    {
        // Skapar en fake/mock service.
        _serviceMock = new Mock<IBookingService>();

        // Skickar in den mockade servicen till controllern.
        _controller = new BookingController(_serviceMock.Object);
    }

    [TestMethod]
    public async Task GetBookingByNumber_ShouldReturnOk_WhenBookingExists()
    {
        // ARRANGE
        // Skapar ett fake bokningsobjekt som servicen ska returnera.

        var booking = new BookingResponseDto(
            BookingNumber: 1,
            GuestName: "Pontus",
            GuestEmail: "pontus@test.se",
            GuestPhone: "0701234567",
            BookingDate: DateOnly.FromDateTime(DateTime.Now),
            SittingStartTime: new TimeOnly(17, 0),
            SittingEndTime: new TimeOnly(19, 0),
            NumberOfGuests: 2,
            Status: "Bekräftad",
            Message: null,
            CreatedDate: DateTime.Now,
            IsPlaced: true
        );

        // Bestämmer vad mock-servicen ska returnera
        // när GetBookingByNumberAsync(1) anropas.
        _serviceMock
            .Setup(x => x.GetBookingByNumberAsync(1))
            .ReturnsAsync(booking);

        // ACT
        // Kör controller-metoden.

        var result = await _controller.GetBookingByNumber(1);

        // ASSERT
        // Kontrollerar att resultatet blev HTTP 200 OK.

        var okResult = result.Result as OkObjectResult;

        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
    }

    [TestMethod]
    public async Task GetBookingByNumber_ShouldReturnBadRequest_WhenExceptionThrown()
    {
        // ARRANGE
        // Bestämmer att servicen ska kasta exception.

        _serviceMock
            .Setup(x => x.GetBookingByNumberAsync(1))
            .ThrowsAsync(new Exception("Booking not found"));

        // ACT
        // Kör controller-metoden.

        var result = await _controller.GetBookingByNumber(1);

        // ASSERT
        // Kontrollerar att controllern returnerar 400 BadRequest.

        var badRequest = result.Result as BadRequestObjectResult;

        Assert.IsNotNull(badRequest);
        Assert.AreEqual(400, badRequest.StatusCode);
    }

    [TestMethod]
    public async Task DeleteBooking_ShouldReturnOk_WhenDeleteSucceeds()
    {
        // ARRANGE
        // Bestämmer att delete ska lyckas utan exception.

        _serviceMock
            .Setup(x => x.DeleteBookingAsync(1))
            .Returns(Task.CompletedTask);

        // ACT
        // Kör delete-metoden i controllern.

        var result = await _controller.DeleteBooking(1);

        // ASSERT
        // Kontrollerar att resultatet blev 200 OK.

        var okResult = result as OkObjectResult;

        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
    }

    [TestMethod]
    public async Task DeleteBooking_ShouldReturnBadRequest_WhenExceptionThrown()
    {
        // ARRANGE
        // Bestämmer att delete ska kasta exception.

        _serviceMock
            .Setup(x => x.DeleteBookingAsync(1))
            .ThrowsAsync(new Exception("Booking not found"));

        // ACT
        // Kör delete-metoden.

        var result = await _controller.DeleteBooking(1);

        // ASSERT
        // Kontrollerar att controllern returnerar 400 BadRequest.

        var badRequest = result as BadRequestObjectResult;

        Assert.IsNotNull(badRequest);
        Assert.AreEqual(400, badRequest.StatusCode);
    }
}