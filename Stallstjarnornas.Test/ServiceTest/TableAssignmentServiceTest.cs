using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Stallstjarnornas.Library.Models;
using Stallstjarnornas.Test.TestHelpers;
using Stallstjarnornas.WebAPI.Controllers;
using Stallstjarnornas.WebAPI.Data;
using Stallstjarnornas.WebAPI.DTOs.TableAssignment;
using Stallstjarnornas.WebAPI.Interfaces;
using Stallstjarnornas.WebAPI.Migrations;
using Stallstjarnornas.WebAPI.Services;

namespace Stallstjarnornas.Test.ServiceTest
{
    [TestClass]
    public class TableAssignmentServiceTest
    {
        private StallstjarnornasDbContext _ctx;
        private Mock<ITableAssignmentService> _mockTableAssignmentService; //sätter upp en fejkad service via interface
        private TableAssignmentController _controller;



        [TestInitialize]
        public async Task Setup()
        {
            _ctx = DbContextFactory.CreateInMemoryContext();
            _mockTableAssignmentService = new Mock<ITableAssignmentService>();
            _controller = new TableAssignmentController(_mockTableAssignmentService.Object);//.objeckt är själva interfacet som ska återspeglas
            await TestDataHelper.SeedBasicDataAsync(_ctx);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _ctx.Dispose();
        }

        


        //ServiceTester
        [TestMethod]
        //Testet använder sig av customer id "2" från seeddata
        public async Task CreateNewAssignment_WithCorrectInput_ShouldReturnResponseDTO()
        {
            //Arrange
            //Fejkad Bokning
            var fakeBooking = _ctx.Bookings.Add(new Booking
            {
                Id = 3,
                GuestId = 2,
                SittingId = 1,
                BookingDate = new DateTime(2026, 6, 1),
                NoOfGuests = 2,
                Status = "Pending",
                BookingNumber = 1003,
                CreatedDate = DateTime.Now
            });

            await _ctx.SaveChangesAsync();
            //Fejkat DTO-inskick
            var fakeTableAssignment = new CreateTableAssignmentDto(1003, new List<int>() { 1 });


            //Act
            TableAssignmentService _tas = new TableAssignmentService(_ctx);
            var result = await _tas.CreateTableAssignmentAsync(fakeTableAssignment);
            var expected = new TableAssignmentResponseDto(new List<int>() { 1 }, 1003, "woop woopsson", 2, new DateOnly(2026, 06, 01), 1);
            //Assert
            Assert.AreEqual(result.bookingNumber, expected.bookingNumber, "Correct input should yield correct response");
            CollectionAssert.AreEqual(result.TableIds, expected.TableIds, "Correct input should yield correct response");//collection assert för att det är listor

            var pullTest = await _ctx.TableAssignments.FirstOrDefaultAsync(ta => ta.Booking.BookingNumber == 1003);

            Assert.AreEqual(1, pullTest.TableId);
            Assert.AreEqual(1003, pullTest.Booking.BookingNumber);
            //Testar ifall det sparades till databasen
        }

        [TestMethod]
        public async Task CreateNewAssignment_WithAllreadyBookedTables_ShouldThrowException()
        {
            //Arrange
            //Fejkad bokning (använder guestID 2 som är Erik i testdata)
            var fakeBooking = _ctx.Bookings.Add(new Booking
            {
                Id = 3,
                GuestId = 2,
                SittingId = 1,
                BookingDate = new DateTime(2026, 6, 1),
                NoOfGuests = 2,
                Status = "Pending",
                BookingNumber = 1003,
                CreatedDate = DateTime.Now
            });
            //Fejkad tableassigmnet sedan innan 
            var fakeOldTableAssignment = _ctx.TableAssignments.Add(new TableAssignment { TableId = 1, BookingId = 1 });

            await _ctx.SaveChangesAsync();

            //Fejkat DTO-inskick
            var fakeNewTableAssignment = new CreateTableAssignmentDto(1003, new List<int>() { 1 });

            //Act
            //Assert
            TableAssignmentService _tas = new TableAssignmentService(_ctx);
            try
            {
                await _tas.CreateTableAssignmentAsync(fakeNewTableAssignment);
                Assert.Fail("Skulle ha givit exception");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Table is allready being used", ex.Message);
            }


        }

        [TestMethod]
        public async Task CreateNewAssignment_WithTableIDThatDontExist_ShouldThrowException()
        {
            //Arrange
            //Måste slänga in bokningen i databasen för att servicen ska kunna hitta den. 
            //Hämtar guest från seed pga slippa skriva för mycket
            var fakeBooking = _ctx.Add(new Booking
            {
                Id = 3,
                GuestId = 2,
                SittingId = 1,
                BookingDate = new DateTime(2026, 6, 1),
                NoOfGuests = 2,
                Status = "Pending",
                BookingNumber = 1003,
                CreatedDate = DateTime.Now
            });

            //Faketablesassigmnet dto
            var testAssignment = new CreateTableAssignmentDto(1003, new List<int>() { 32 });
            await _ctx.SaveChangesAsync();
            //Act

            var _tas = new TableAssignmentService(_ctx);
            //Assert
            try
            {
                await _tas.CreateTableAssignmentAsync(testAssignment);
                Assert.Fail("Should have thrown exception");
            }
            catch (Exception _ex)
            {
                Assert.AreEqual("One or more tables where not found", _ex.Message);
            }

        }
        [TestMethod]
        public async Task CreateNewAssignment_WithLessTablesThanNeededForBooking_ShouldThrowException()
        {
            //Arrange
            //Måste slänga in bokningen i databasen för att servicen ska kunna hitta den. 
            //Hämtar guest från seed pga slippa skriva för mycket
            var fakeBooking = _ctx.Add(new Booking
            {
                Id = 3,
                GuestId = 2,
                SittingId = 1,
                BookingDate = new DateTime(2026, 6, 1),
                NoOfGuests = 4,
                Status = "Pending",
                BookingNumber = 1003,
                CreatedDate = DateTime.Now
            });
            //Faketablesassigmnet dto
            var testAssignment = new CreateTableAssignmentDto(1003, new List<int>() { 2 });
            await _ctx.SaveChangesAsync();
            //Act
            var _tas = new TableAssignmentService(_ctx);
            //Assert
            try
            {
                await _tas.CreateTableAssignmentAsync(testAssignment);
                Assert.Fail("Should have thrown exception");
            }
            catch (Exception _ex)
            {
                Assert.AreEqual("You need to assign more tables", _ex.Message);
            }

        }
        [TestMethod]
        public async Task CreateNewAssignment_WithNoBookingFound_ShouldThrowException()
        {
            //Faketablesassigmnet dto
            var testAssignment = new CreateTableAssignmentDto(3, new List<int>() { 2 });
            await _ctx.SaveChangesAsync();
            //Act
            var _tas = new TableAssignmentService(_ctx);
            //Assert
            try
            {
                await _tas.CreateTableAssignmentAsync(testAssignment);
                Assert.Fail("Should have thrown exception");
            }
            catch (Exception _ex)
            {
                Assert.AreEqual("Booking not found", _ex.Message);
            }


        }

        [TestMethod]
        public async Task CreateNewAssignment_WithNoTablesProvided_ShouldThrowException()
        {
            //Arrange
            //Måste slänga in bokningen i databasen för att servicen ska kunna hitta den. 
            //Hämtar guest från seed pga slippa skriva för mycket
            var fakeBooking = _ctx.Add(new Booking
            {
                Id = 3,
                GuestId = 2,
                SittingId = 1,
                BookingDate = new DateTime(2026, 6, 1),
                NoOfGuests = 1,
                Status = "Pending",
                BookingNumber = 1003,
                CreatedDate = DateTime.Now
            });
            //Faketablesassigmnet dto
            var testAssignment = new CreateTableAssignmentDto(1003, new List<int>() { });
            await _ctx.SaveChangesAsync();
            //Act
            var _tas = new TableAssignmentService(_ctx);
            //Assert
            try
            {
                await _tas.CreateTableAssignmentAsync(testAssignment);
                Assert.Fail("Should have thrown exception");
            }
            catch (Exception _ex)
            {
                Assert.AreEqual("No tables found", _ex.Message);
            }

        }

        [TestMethod]
        public async Task CreateNewAssignment_WithDuplicateTablesProvided_ShouldThrowException()
        {
            //Arrange
            //Måste slänga in bokningen i databasen för att servicen ska kunna hitta den. 
            //Hämtar guest från seed pga slippa skriva för mycket
            var fakeBooking = _ctx.Add(new Booking
            {
                Id = 3,
                GuestId = 2,
                SittingId = 1,
                BookingDate = new DateTime(2026, 6, 1),
                NoOfGuests = 4,
                Status = "Pending",
                BookingNumber = 1003,
                CreatedDate = DateTime.Now
            });
            //Faketablesassigmnet dto
            var testAssignment = new CreateTableAssignmentDto(1003, new List<int>() { 1, 1 });
            await _ctx.SaveChangesAsync();
            //Act
            var _tas = new TableAssignmentService(_ctx);
            //Assert
            try
            {
                await _tas.CreateTableAssignmentAsync(testAssignment);
                Assert.Fail("Should have thrown exception");
            }
            catch (Exception _ex)
            {
                Assert.AreEqual("You assigned one or more tables more than once", _ex.Message);
            }

        }

        [TestMethod]
        public async Task CreateNewAssignment_WithTwoTableProvided_ShouldCreateTwoAssignments()
        {
            //Arrange
            //Måste slänga in bokningen i databasen för att servicen ska kunna hitta den. 
            //Hämtar guest från seed pga slippa skriva för mycket
            var fakeBooking = _ctx.Add(new Booking
            {
                Id = 3,
                GuestId = 2,
                SittingId = 1,
                BookingDate = new DateTime(2026, 6, 1),
                NoOfGuests = 4,
                Status = "Pending",
                BookingNumber = 1003,
                CreatedDate = DateTime.Now
            });
            //Faketablesassigmnet dto
            var testAssignment = new CreateTableAssignmentDto(1003, new List<int>() { 1, 2 });
            await _ctx.SaveChangesAsync();
            //Act
            var _tas = new TableAssignmentService(_ctx);
            await _tas.CreateTableAssignmentAsync(testAssignment);

            var noOfAssignmentsCreated = await _ctx.TableAssignments.Where(ta => ta.BookingId == 3).ToListAsync();
            //Assert

            Assert.AreEqual(2, noOfAssignmentsCreated.Count);



        }

        //Tests for getting available tables
        [TestMethod]
        public async Task GetAvailableTables_CorrectInput_ShouldReturn23Tables()
        {
            //Arrange
            var fakeBooking = _ctx.Add(new Booking
            {
                
                GuestId = 2,
                SittingId = 1,
                BookingDate = new DateTime(2026, 8, 1),
                NoOfGuests = 4,
                Status = "Pending",
                BookingNumber = 1003,
                CreatedDate = DateTime.Now
            });
           


            //Faketablesassigmnet dto
            var testAssignment = new CreateTableAssignmentDto(1003, new List<int>() {1, 2 });
            var fakeGetAvailableTablesDTO = new GetAvailableTablesDto(new DateOnly(2026, 8, 1), 1);
            await _ctx.SaveChangesAsync();

            var tableIds = _ctx.Tables.Take(2).Select(t => t.Id).ToList();
            TableAssignmentService _tas = new TableAssignmentService(_ctx);

            await _tas.CreateTableAssignmentAsync(testAssignment);
            var result = await _tas.GetAvailableTablesAsync(fakeGetAvailableTablesDTO);
            Assert.AreEqual(23, result.TableIds.Count());
        }

        [TestMethod]
        public async Task GetAvailableTables_WrongSittingIdInput_ShouldThrowException()
        {
            //Arrange
          
            var fakeGetAvailableTablesDTO = new GetAvailableTablesDto(new DateOnly(2026, 8, 1), 3);
            await _ctx.SaveChangesAsync();

            TableAssignmentService _tas = new TableAssignmentService(_ctx);
            //Act/Assert
            try
            {
                await _tas.GetAvailableTablesAsync(fakeGetAvailableTablesDTO);//Om detta anrop castar exception så hoppar körningen direkt till catch, annars om anropet är ok så kommer assert.fail köras
                Assert.Fail("Should have thrown exception");
            }
            catch (Exception _ex)
            {
                Assert.AreEqual("You must assign sitting Id 1 or 2", _ex.Message);
            }
        }

        [TestMethod]
        public async Task DeleteTableAssignment_NoAssigmnetsFound_ShouldThrowException()
        {
            //Arrange
            var fakeBooking = _ctx.Add(new Booking
            {
                Id = 3,
                GuestId = 2,
                SittingId = 1,
                BookingDate = new DateTime(2026, 8, 1),
                NoOfGuests = 4,
                Status = "Pending",
                BookingNumber = 1003,
                CreatedDate = DateTime.Now
            });

            var fakeDeleteTableAssignmentsDTO = new DeleteAssignedTablesDTO(3);
            await _ctx.SaveChangesAsync();

            TableAssignmentService _tas = new TableAssignmentService(_ctx);
            //Act/Assert
            try
            {
                await _tas.DeleteAssignedTablesAsync(fakeDeleteTableAssignmentsDTO);//Om detta anrop castar exception så hoppar körningen direkt till catch, annars om anropet är ok så kommer assert.fail köras
                Assert.Fail("Should have thrown exception");
            }
            catch (Exception _ex)
            {
                Assert.AreEqual("No assignments found for booking", _ex.Message);
            }
        }
    }
}
