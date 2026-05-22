using Microsoft.EntityFrameworkCore;
using Stallstjarnornas.Library.Models;
using Stallstjarnornas.Test.TestHelpers;
using Stallstjarnornas.WebAPI.Data;
using Stallstjarnornas.WebAPI.DTOs.Guest;
using Stallstjarnornas.WebAPI.Services;
using System.ComponentModel.DataAnnotations;

using System.Threading.Tasks;

namespace Stallstjarnornas.Test.ServiceTest
{
    [TestClass]
    public class GuestServiceTest
    {
        private StallstjarnornasDbContext _ctx;
        private GuestService _service;
        [TestInitialize]
        public async Task SetUp()
        {
            _ctx = DbContextFactory.CreateInMemoryContext();
            await TestHelpers.TestDataHelper.SeedBasicDataAsync(_ctx);
            _service = new GuestService(_ctx);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _ctx.Dispose();
        }

        [TestMethod]
        public async Task GetGuestByIdAsync_ShouldReturnGuest_WhenGuestExists()
        {
            // Arrange

            // Act
            var result = await _service.GetGuestByIdAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Anna Lindqvist", result.Name);
            Assert.AreEqual("anna@test.com", result.Email);
        }

        [TestMethod]
        public async Task GetGuestByIdAsync_WhenGuestDoesNotExist_ReturnNull()
        {

            //Act
            var result = await _service.GetGuestByIdAsync(999);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetAllGuestAsync_ReturnAllGuest()
        {
            _ctx.Guests.Add(new Guest
            {
                Name = "Viktor Andersson",
                Phone = "1234134324",
                Email = "häst@häst.polle"
            });
            await _ctx.SaveChangesAsync();

            var result = await _service.GetAllGuestsAsync();

            Assert.AreEqual(3, result.Count());
            Assert.IsTrue(result.Any(g => g.Name == "Anna Lindqvist"));
            Assert.IsTrue(result.Any(g => g.Name == "Erik Johansson"));
            Assert.IsTrue(result.Any(g => g.Name == "Viktor Andersson"));
        }

        [TestMethod]
        public async Task GetGuestEntityByEmailAsync_ReturnMatchingEmail()
        {
            var result = await _service.GetGuestEntityByEmailAsync("anna@test.com");

            Assert.IsNotNull(result);
            Assert.AreEqual("Anna Lindqvist", result.Name);
            Assert.AreEqual("anna@test.com", result.Email);
        }

        [TestMethod]
        public async Task GetGuestEntityByEmailAsync_ShouldReturnNull_WhenEmailDoesNotExist()
        {
            // Act
            var result = await _service.GetGuestEntityByEmailAsync("finns@inte.com");

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task UpdateGuestAsync_ShouldUpdateAndReturnGuest_WhenGuestExists()
        {
            //Arrange
            var dto = new UpdateGuestDto(
                "Anna Svensson",
                "1234568789",
                "Anna@Banana.se"
                );

            //Act 
            var result = await _service.UpdateGuestAsync(1, dto);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Anna Svensson", result.Name);
            Assert.AreEqual("1234568789", result.Phone);
            Assert.AreEqual("Anna@Banana.se", result.Email);
        }

        [TestMethod]
        public async Task UpdateGuestAsync_ShouldReturnNull_WhenGuestDoesNotExist()
        {
            // Arrange
            var dto = new UpdateGuestDto(
                "Finns Inte",
                "0000000000",
                "finns@inte.com"
            );

            // Act
            var result = await _service.UpdateGuestAsync(999, dto);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task RegisterGuestAsync_ShouldCreateAndReturnGuest_WhenEmailIsNew()
        {
            //arrange
            var dto = new CreateGuestDto(
                "Viktor Andersson",
                "1234134324",
                "Viktor@test.com"
                );

            var result = await _service.RegisterGuestAsync(dto);

            Assert.IsNotNull(result);
            Assert.AreEqual("Viktor Andersson", dto.Name);
            Assert.AreEqual("Viktor@test.com", dto.Email);

            var guestInDb = await _ctx.Guests.FirstOrDefaultAsync(g => g.Email == "Viktor@test.com");
            Assert.IsNotNull(guestInDb);
        }

        [TestMethod]
        public async Task RegisterGuestAsync_ShouldReturnNull_WhenEmailAlreadyExists()
        {
            // Arrange 
            var dto = new CreateGuestDto(
                "Anna Lindqvist",
                "0701234567",
                "anna@test.com"
            );

            // Act
            var result = await _service.RegisterGuestAsync(dto);

            // Assert
            Assert.IsNull(result);
        }
    }
}

