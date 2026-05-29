
using Microsoft.AspNetCore.Mvc;
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
        var nullResult = result.Result as NotFoundResult;
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
            .Setup(ig => ig.GetAllGuestsAsync())
            .ReturnsAsync(guests);
        //Act
        var result = await _controller.GetAllGuests();

        //Assert
        var okResult = result.Result as OkObjectResult;

        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
    }
}



