
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Moq;
using Stallstjarnornas.WebAPI.Controllers;
using Stallstjarnornas.WebAPI.DTOs.Guest;
using Stallstjarnornas.WebAPI.Interfaces;

namespace Stallstjarnornas.Test.ControllerTest;
[TestClass]
public class GuestControllerTest
{
    private Mock<IGuestService> _serviceMock = null!;

    private GuestController _controller = null!;

    private GuestDto CreateTestGuest(int id, string email)
    {
        var guest = new GuestDto(Id: id, Name: "Viktor", Phone: "123123123", Email: email
            );
        return guest;
    }

    [TestInitialize]
    public void SetUp()
    {
        _serviceMock = new Mock<IGuestService>();

        _controller = new GuestController(_serviceMock.Object);
    }
    [TestMethod]

    public async Task GetGuestById_ShouldReturnOk_WhenSearchingExistingGuest()
    {
        //Arrange
        var guest = CreateTestGuest(1, "test@test.se");

        _serviceMock
            .Setup(ig => ig.GetGuestByIdAsync(1))
            .ReturnsAsync(guest);
        //Act
        var result = await _controller.GetGuest(1);

        //Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
    }

    [TestMethod]
    public async Task GetGuestById_ShouReturnNotFound_WhenSearchingNonExistingGuest()
    {
        //Arrange
        //var guest = CreateTestGuest();

        _serviceMock
            .Setup(ig => ig.GetGuestByIdAsync(1))
            .ReturnsAsync((GuestDto?)null);
        //Act
        var result = await _controller.GetGuest(1);

        //Assert
        var nullResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(nullResult);
        Assert.AreEqual(404, nullResult.StatusCode);
    }
    [TestMethod]
    public async Task GetAllGuests_ShouldReturnOk_WhenAListOfGuestExists()
    {
        //Arrange
        var guest1 = CreateTestGuest(1, "test1@test.se");
        var guest2 = CreateTestGuest(2, "test2@test.se");
        var guest3 = CreateTestGuest(3, "test3@test.se");

        var guests = new List<GuestDto> { guest1, guest2, guest3 };
        _serviceMock
            .Setup(ig => ig.GetAllGuestsAsync())
            .ReturnsAsync(guests);
        //Act
        var result = await _controller.GetAllGuests();

        //Assert
        var okResult = result.Result as OkObjectResult;

        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
    }


    [TestMethod]
    public async Task GetAllGuests_ShouldReturnOk_WhenListIsEmpty()
    {
        //Arrange


        var guests = new List<GuestDto> { };
        _serviceMock
            .Setup(g => g.GetAllGuestsAsync())
            .ReturnsAsync(guests);
        //Act
        var result = await _controller.GetAllGuests();

        //Assert
        var okResult = result.Result as OkObjectResult;

        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
    }

    [TestMethod]
    public async Task RegisterGuest_ShouldReturn201_WhenRegisterNewGuest()
    {
        var dto = new CreateGuestDto("Test Test", "123123123", "test@test.se");
        var guest = CreateTestGuest(1, "test@test.se");
        _serviceMock.Setup(g => g.RegisterGuestAsync(dto))
            .ReturnsAsync(guest);

        //Act
        var result = await _controller.RegisterGuest(dto);

        //Assert
        var okResult = result.Result as CreatedAtActionResult;

        Assert.IsNotNull(okResult);
        Assert.AreEqual(201, okResult.StatusCode);
    }

    [TestMethod]
    public async Task RegisterGuest_ShouldReturn409_WhenEmailIsNotUnique()
    {
        //Arrange
        var dto = new CreateGuestDto("Test Test", "123123123", "test@test.se");
       

        _serviceMock.Setup(g => g.RegisterGuestAsync(dto))
            .ReturnsAsync((GuestDto?)null);
        
        //Act
        var result = await _controller.RegisterGuest(dto);
        var conflictResult = result.Result as ConflictObjectResult;

        Assert.IsNotNull(conflictResult);
        Assert.AreEqual(409, conflictResult.StatusCode);
    }

    [TestMethod]
    public async Task DeleteGuest_ShouldReturnNoContent_WhenGuestGetsDeleted()
    {
        //Arrange

        _serviceMock.Setup(g => g.DeleteGuestAsync(1))
            .ReturnsAsync(true);

        //Act
        var result = await _controller.DeleteGuest(1);
        var noContentResult = result as NoContentResult;
        //Assert
        Assert.IsNotNull(noContentResult);
        Assert.AreEqual(204, noContentResult.StatusCode);
    }

    [TestMethod]
    public async Task DeleteGuest_ShouldReturnNotFound_WhenTryNonExistingGuest()
    {
        //Arrange

        _serviceMock.Setup(g => g.DeleteGuestAsync(999))
            .ReturnsAsync(false);

        //Act
        var result = await _controller.DeleteGuest(999);
        var notFoundResult = result as NotFoundObjectResult;

        //Assert
        Assert.IsNotNull(notFoundResult);
        Assert.AreEqual(404, notFoundResult.StatusCode);
    }

    [TestMethod]
    public async Task UpdateGuest_ShouldReturnOk_WhenTryToUpdateGuest()
    {
        //Arrange
        var dto = new UpdateGuestDto("Test Test", "123123123", "test@test.se");
        var guest = CreateTestGuest(1, "test@test.se");
        _serviceMock.Setup(g => g.UpdateGuestAsync(1, dto))
            .ReturnsAsync(guest);

        //Act
        var result = await _controller.UpdateGuest(1, dto);
        var okResult = result.Result as OkObjectResult;

        //Assert
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
    }

    [TestMethod]
    public async Task UpdateGuest_ShouldRetunNotFound_WhenTryToUpdateNonExistingGuest()
    {
        var dto = new UpdateGuestDto("Test Test", "123123123", "test@test.se");

        _serviceMock.Setup(g => g.UpdateGuestAsync(1, dto))
            .ReturnsAsync((GuestDto?) null);

        var result = await _controller.UpdateGuest(1, dto);
        var notFoundResult = result.Result as NotFoundObjectResult;

        Assert.IsNotNull(notFoundResult);
        Assert.AreEqual(404, notFoundResult.StatusCode);
    }
}



