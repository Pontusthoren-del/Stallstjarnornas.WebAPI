using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Stallstjarnornas.WebAPI.DTOs.Booking;
using Stallstjarnornas.WebAPI.Exceptions;
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
    public async Task GetBookingByNumber_ShouldReturnNotFound_WhenBookingDoesNotExist()
    {
        // ARRANGE
        // Bestämmer att servicen ska kasta NotFoundException
        _serviceMock
            .Setup(x => x.GetBookingByNumberAsync(9999))
            .ThrowsAsync(new NotFoundException("Bokningen hittades inte."));

        // ACT
        var result = await _controller.GetBookingByNumber(9999);

        // ASSERT
        // Kontrollerar att controllern returnerar 404 NotFound
        var notFound = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFound);
        Assert.AreEqual(404, notFound.StatusCode);
    }

    [TestMethod]
    public async Task GetBookingByNumber_ShouldReturnBadRequest_WhenExceptionThrown()
    {
        // ARRANGE
        // Bestämmer att servicen ska kasta ett generellt exception.
        _serviceMock
            .Setup(x => x.GetBookingByNumberAsync(1))
            .ThrowsAsync(new Exception("Något gick fel."));

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
    public async Task DeleteBooking_ShouldReturnNotFound_WhenBookingDoesNotExist()
    {
        // ARRANGE
        // Bestämmer att servicen ska kasta NotFoundException
        _serviceMock
            .Setup(x => x.DeleteBookingAsync(9999))
            .ThrowsAsync(new NotFoundException("Bokningen hittades inte."));

        // ACT
        var result = await _controller.DeleteBooking(9999);

        // ASSERT
        // Kontrollerar att controllern returnerar 404 NotFound
        var notFound = result as NotFoundObjectResult;
        Assert.IsNotNull(notFound);
        Assert.AreEqual(404, notFound.StatusCode);
    }

    [TestMethod]
    public async Task DeleteBooking_ShouldReturnBadRequest_WhenExceptionThrown()
    {
        // ARRANGE
        // Bestämmer att delete ska kasta ett generellt exception.
        _serviceMock
            .Setup(x => x.DeleteBookingAsync(1))
            .ThrowsAsync(new Exception("Något gick fel."));

        // ACT
        // Kör delete-metoden.
        var result = await _controller.DeleteBooking(1);

        // ASSERT
        // Kontrollerar att controllern returnerar 400 BadRequest.
        var badRequest = result as BadRequestObjectResult;
        Assert.IsNotNull(badRequest);
        Assert.AreEqual(400, badRequest.StatusCode);
    }

    [TestMethod]
    public async Task CreateBooking_ShouldReturnOk_WhenSuccess()
    {
        // ARRANGE
        // Skapar input-DTO som skickas till controllern (som om det kom från API-anrop)
        var dto = new CreateBookingDto(
            Name: "Anna",
            Phone: "0701234567",
            Email: "anna@test.se",
            NumberOfGuests: 2,
            BookingDate: new DateOnly(2026, 6, 1),
            SittingId: 1,
            Message: null
        );

        // Skapar ett förväntat svar från servicen (mockad data)
        // Detta ersätter riktig service + databas i testet
        var response = new BookingResponseDto(
            BookingNumber: 1,
            GuestName: "Anna",
            GuestEmail: "anna@test.se",
            GuestPhone: "0701234567",
            BookingDate: new DateOnly(2026, 6, 1),
            SittingStartTime: new TimeOnly(17, 0),
            SittingEndTime: new TimeOnly(19, 0),
            NumberOfGuests: 2,
            Status: "Pending",
            Message: null,
            CreatedDate: DateTime.Now,
            IsPlaced: false
        );

        // Säger till mockad service:
        // "Om CreateBookingAsync anropas med denna DTO, returnera detta response"
        _serviceMock
            .Setup(x => x.CreateBookingAsync(dto))
            .ReturnsAsync(response);

        // ACT
        // Anropar controller-metoden som testas
        var result = await _controller.CreateBooking(dto);

        // ASSERT
        // Plockar ut HTTP 200 OK-resultatet från ActionResult
        var okResult = result.Result as OkObjectResult;

        // Säkerställer att vi faktiskt fick ett OK-svar
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);

        // Plockar ut själva datan som controllern returnerade
        var value = okResult.Value as BookingResponseDto;

        // Säkerställer att rätt typ returnerades
        Assert.IsNotNull(value);

        // Verifierar att innehållet är korrekt
        Assert.AreEqual("Anna", value.GuestName);
    }

    [TestMethod]
    public async Task CreateBooking_ShouldReturnNotFound_WhenSittingDoesNotExist()
    {
        // ARRANGE
        var dto = new CreateBookingDto(
            Name: "Anna",
            Phone: "0701234567",
            Email: "anna@test.se",
            NumberOfGuests: 2,
            BookingDate: new DateOnly(2026, 6, 1),
            SittingId: 99,
            Message: null
        );

        // Bestämmer att servicen ska kasta NotFoundException
        // eftersom sittningen inte finns
        _serviceMock
            .Setup(x => x.CreateBookingAsync(It.IsAny<CreateBookingDto>()))
            .ThrowsAsync(new NotFoundException("Sittningen finns inte."));

        // ACT
        var result = await _controller.CreateBooking(dto);

        // ASSERT
        // Kontrollerar att controllern returnerar 404 NotFound
        var notFound = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFound);
        Assert.AreEqual(404, notFound.StatusCode);
    }

    [TestMethod]
    public async Task CreateBooking_ShouldReturnBadRequest_WhenExceptionThrown()
    {
        // ARRANGE
        // Skapar en DTO med giltig input - spelar ingen roll här
        // eftersom servicen kastar exception oavsett vad vi skickar in
        var dto = new CreateBookingDto(
            Name: "Anna",
            Phone: "0701234567",
            Email: "anna@test.se",
            NumberOfGuests: 2,
            BookingDate: new DateOnly(2026, 6, 1),
            SittingId: 1,
            Message: null
        );

        // Bestämmer att servicen ska kasta exception oavsett vilken DTO som skickas in
        // It.IsAny<> används för att matcha alla möjliga DTOs
        _serviceMock
            .Setup(x => x.CreateBookingAsync(It.IsAny<CreateBookingDto>()))
            .ThrowsAsync(new Exception("Sittningen är full"));

        // ACT
        var result = await _controller.CreateBooking(dto);

        // ASSERT
        // Kontrollerar att controllern fångar exception och returnerar 400 BadRequest
        var badRequest = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(badRequest);
        Assert.AreEqual(400, badRequest.StatusCode);
        // Verifierar även att felmeddelandet från exception skickas tillbaka i svaret
        Assert.AreEqual("Sittningen är full", badRequest.Value);
    }

    [TestMethod]
    public async Task CancelBooking_ShouldReturnOk_WhenSuccess()
    {
        // ARRANGE
        // Bestämmer att avbokning ska lyckas utan exception
        _serviceMock
            .Setup(x => x.CancelBookingAsync(1))
            .ReturnsAsync("Pontus");

        // ACT
        var result = await _controller.CancelBooking(1);

        // ASSERT
        // Kontrollerar att resultatet blev 200 OK
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
    }

    [TestMethod]
    public async Task CancelBooking_ShouldReturnNotFound_WhenBookingDoesNotExist()
    {
        // ARRANGE
        // Bestämmer att servicen ska kasta NotFoundException
        _serviceMock
            .Setup(x => x.CancelBookingAsync(9999))
            .ThrowsAsync(new NotFoundException("Bokning hittades inte."));

        // ACT
        var result = await _controller.CancelBooking(9999);

        // ASSERT
        // Kontrollerar att controllern returnerar 404 NotFound
        var notFound = result as NotFoundObjectResult;
        Assert.IsNotNull(notFound);
        Assert.AreEqual(404, notFound.StatusCode);
    }

    [TestMethod]
    public async Task CancelBooking_ShouldReturnConflict_WhenAlreadyCancelled()
    {
        // ARRANGE
        // Bestämmer att servicen ska kasta ConflictException
        // eftersom bokningen redan är avbokad
        _serviceMock
            .Setup(x => x.CancelBookingAsync(1))
            .ThrowsAsync(new ConflictException("Bokningen är redan avbokad."));

        // ACT
        var result = await _controller.CancelBooking(1);

        // ASSERT
        // Kontrollerar att controllern returnerar 409 Conflict
        var conflict = result as ConflictObjectResult;
        Assert.IsNotNull(conflict);
        Assert.AreEqual(409, conflict.StatusCode);
    }

    [TestMethod]
    public async Task CancelBooking_ShouldReturnBadRequest_WhenExceptionThrown()
    {
        // ARRANGE
        // Bestämmer att servicen ska kasta ett generellt exception
        // t.ex. om bokningen inte finns eller redan är avbokad
        _serviceMock
            .Setup(x => x.CancelBookingAsync(1))
            .ThrowsAsync(new Exception("Något gick fel."));

        // ACT
        var result = await _controller.CancelBooking(1);

        // ASSERT
        // Kontrollerar att controllern fångar exception och returnerar 400 BadRequest
        var badRequest = result as BadRequestObjectResult;
        Assert.IsNotNull(badRequest);
        Assert.AreEqual(400, badRequest.StatusCode);
    }

    [TestMethod]
    public async Task UpdateBooking_ShouldReturnOk_WhenSuccess()
    {
        // ARRANGE
        // Skapar en DTO med de fält vi vill uppdatera
        // null-fält betyder att de inte ska ändras
        var dto = new UpdateBookingDto(
            BookingDate: null,
            NumberOfGuests: 4,
            SittingId: null,
            Status: "Confirmed",
            Message: "Uppdaterat meddelande"
        );

        // Skapar det förväntade svaret från servicen med uppdaterade värden
        var response = new BookingResponseDto(
            BookingNumber: 1001,
            GuestName: "Anna Lindqvist",
            GuestEmail: "anna@test.com",
            GuestPhone: "070-123 45 67",
            BookingDate: new DateOnly(2026, 6, 1),
            SittingStartTime: new TimeOnly(17, 0),
            SittingEndTime: new TimeOnly(19, 0),
            NumberOfGuests: 4,
            Status: "Confirmed",
            Message: "Uppdaterat meddelande",
            CreatedDate: DateTime.Now,
            IsPlaced: false
        );

        // Bestämmer vad mock-servicen ska returnera
        // när UpdateBookingAsync anropas med bokningsnummer 1001 och vår DTO
        _serviceMock
            .Setup(x => x.UpdateBookingAsync(1001, dto))
            .ReturnsAsync(response);

        // ACT
        var result = await _controller.UpdateBooking(1001, dto);

        // ASSERT
        // Kontrollerar att resultatet blev 200 OK
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
    }

    [TestMethod]
    public async Task UpdateBooking_ShouldReturnNotFound_WhenBookingDoesNotExist()
    {
        // ARRANGE
        var dto = new UpdateBookingDto(
            BookingDate: null,
            NumberOfGuests: null,
            SittingId: null,
            Status: "Confirmed",
            Message: null
        );

        // Bestämmer att servicen ska kasta NotFoundException
        _serviceMock
            .Setup(x => x.UpdateBookingAsync(9999, It.IsAny<UpdateBookingDto>()))
            .ThrowsAsync(new NotFoundException("Bokningen hittades inte."));

        // ACT
        var result = await _controller.UpdateBooking(9999, dto);

        // ASSERT
        // Kontrollerar att controllern returnerar 404 NotFound
        var notFound = result as NotFoundObjectResult;
        Assert.IsNotNull(notFound);
        Assert.AreEqual(404, notFound.StatusCode);
    }

    [TestMethod]
    public async Task UpdateBooking_ShouldReturnBadRequest_WhenExceptionThrown()
    {
        // ARRANGE
        // Skapar en DTO med ett ogiltigt status för att trigga exception i servicen
        var dto = new UpdateBookingDto(
            BookingDate: null,
            NumberOfGuests: null,
            SittingId: null,
            Status: "Felstavat",
            Message: null
        );

        // It.IsAny<> används för att matcha alla bokningsnummer och DTOs
        // eftersom vi bara vill testa att controllern hanterar exception korrekt
        _serviceMock
            .Setup(x => x.UpdateBookingAsync(It.IsAny<int>(), It.IsAny<UpdateBookingDto>()))
            .ThrowsAsync(new Exception("Ogiltigt status."));

        // ACT
        var result = await _controller.UpdateBooking(1001, dto);

        // ASSERT
        // Kontrollerar att controllern fångar exception och returnerar 400 BadRequest
        var badRequest = result as BadRequestObjectResult;
        Assert.IsNotNull(badRequest);
        Assert.AreEqual(400, badRequest.StatusCode);
    }

    [TestMethod]
    public async Task FilterBookings_ShouldReturnOk_WhenSuccess()
    {
        // ARRANGE
        // Skapar en lista med ett fake bokningsobjekt som servicen ska returnera
        var bookings = new List<BookingResponseDto>
        {
            new BookingResponseDto(
                BookingNumber: 1001,
                GuestName: "Anna Lindqvist",
                GuestEmail: "anna@test.com",
                GuestPhone: "070-123 45 67",
                BookingDate: new DateOnly(2026, 6, 1),
                SittingStartTime: new TimeOnly(17, 0),
                SittingEndTime: new TimeOnly(19, 0),
                NumberOfGuests: 2,
                Status: "Confirmed",
                Message: null,
                CreatedDate: DateTime.Now,
                IsPlaced: false
            )
        };

        // Bestämmer att servicen returnerar vår lista när status = "Confirmed"
        // och alla andra filter är null
        _serviceMock
            .Setup(x => x.FilterBookingsAsync("Confirmed", null, null, null, null, null, null,null,null))
            .ReturnsAsync(bookings);

        // ACT
        // Anropar controller-metoden med status-filter
        var result = await _controller.FilterBookings(
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

        // ASSERT
        // Kontrollerar att resultatet blev 200 OK
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
    }

    [TestMethod]
    public async Task FilterBookings_ShouldReturnBadRequest_WhenExceptionThrown()
    {
        // ARRANGE
        // It.IsAny<> används för alla parametrar eftersom vi bara vill
        // testa att controllern hanterar exception oavsett vilka filter som skickas in
        _serviceMock
            .Setup(x => x.FilterBookingsAsync(
                It.IsAny<string?>(),
                It.IsAny<DateOnly?>(),
                It.IsAny<int?>(),
                It.IsAny<int?>(),
                It.IsAny<int?>(),
                It.IsAny<int?>(),
                It.IsAny<bool?>(),
                It.IsAny<string?>(),
                It.IsAny<int?>()))
           
            .ThrowsAsync(new Exception("Något gick fel."));

        // ACT
        // Anropar med alla null - filtren spelar ingen roll här
        var result = await _controller.FilterBookings(null, null, null, null, null, null, null,null,null);

        // ASSERT
        // Kontrollerar att controllern fångar exception och returnerar 400 BadRequest
        var badRequest = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(badRequest);
        Assert.AreEqual(400, badRequest.StatusCode);
    }
}