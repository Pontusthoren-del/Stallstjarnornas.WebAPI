using Stallstjarnornas.WebAPI.Data;
using Stallstjarnornas.WebAPI.Services;
using Stallstjarnornas.Test.TestHelpers;

using System.Threading.Tasks;
using Stallstjarnornas.Library.Models;

namespace Stallstjarnornas.Test.ServiceTest
{
    [TestClass]
    public class GuestServiceTest
    {
        private StallstjarnornasDbContext _ctx;
        private GuestService _service;
        [TestInitialize]
        public void SetUp()
        {
            _ctx = DbContextFactory.CreateInMemoryContext();
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
            var guest = new Guest
            {
                Id = 1,
                Name = "Test Testersson",
                Phone = "1234567891",
                Email = "Test@test.test"
            };
            
            _ctx.Guests.Add(guest);
            await _ctx.SaveChangesAsync();

            // Act
            var result = await _service.GetGuestByIdAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Test Testersson", result.Name);
            Assert.AreEqual("Test@test.test", result.Email); 
        }

        [TestMethod]
        public async Task GetUserByIdAsync_WhenGuestDoesNotExist_ReturnNull()
        {
            //Arrange
            //Act
            var result = await _service.GetGuestByIdAsync(-1);

            //Assert
            Assert.IsNull(result);
        }
        
    }

}



