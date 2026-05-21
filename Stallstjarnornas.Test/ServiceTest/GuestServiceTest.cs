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
        public async Task GetUserByIdAsync_WhenGuestDoesNotExist_ReturnNull()
        {
           
            //Act
            var result = await _service.GetGuestByIdAsync(3);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetAllUsersAsync_ReturnAllGuest()
        {
            var guest = new Guest
            {
                Name = "Viktor Andersson",
                Phone = "1234134324",
                Email = "häst@häst.polle"
            };
            _ctx.Guests.Add(guest);
            _ctx.SaveChanges();

        }
        
    }

}



