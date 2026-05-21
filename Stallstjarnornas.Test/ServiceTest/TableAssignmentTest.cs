using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
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
    public class TableAssignmentTest
    {
        private StallstjarnornasDbContext _ctx;
        private Mock<ITableAssignmentService> _mockTableAssignmentService; //sätter upp en fejkad service via interface
        private TableAssignmentController _controller;



        [TestInitialize]
        public void Setup()
        {
            _ctx = DbContextFactory.CreateInMemoryContext();
            _mockTableAssignmentService = new Mock<ITableAssignmentService>();
            _controller = new TableAssignmentController(_mockTableAssignmentService.Object);//.objeckt är själva interfacet som ska återspeglas
        }

        [TestCleanup]
        public void Cleanup()
        {
            _ctx.Dispose();
        }

        //Test av controllern
        [TestMethod]
        //Test steg för steg: 
        //1. jag arrangerar först vad jag skickar in till controllern med assignment (input-data)
        //2. jag sätter upp vilken respons min FEKADE service ska ge tillbaka via fake-response
        //3. via mock säger jag: "om GetAvailableTablesAsync anropas med detta assignment så returnera fake-response"
        //4. jag använder min riktiga controller, men med en fejkad service (via interface + mock)
        //5. controllern anropar servicen, men det är mocken som svarar med mitt fake-response
        //6. jag får tillbaka ett ActionResult från controllern, så jag måste plocka ut värdet ur http-wrappern
        //7. jag jämför resultatet (DTO:n) med det jag förväntar mig (fake-response)
        public async Task GetAvailableTables_ShouldReturnListOfAvailableTables()
        {
            //Arrange
            var assignment = new GetAvailableTablesDto(new DateOnly(2026, 5, 10),
                1);
            var fakeResponse = new GetAvailableTablesResponseDto(assignment.bookingDate, assignment.sittingid, new List<int>() { 1, 2 });
            
            //Om någon anropar GetAvailableTablesAsync med exakt detta assignment, så ska mocken svara med fakeValue.
            _mockTableAssignmentService.Setup(tas => tas.GetAvailableTablesAsync(assignment))
                .ReturnsAsync(fakeResponse); 


            //Act
            var result= await _controller.GetAvailableTablesAsync(assignment);
            var comparingResult = result.Result as OkObjectResult;//måste "plocka ut" svaret ur http-wrapper för svaret är ett actionresult och inte en dto
            //Assert
            Assert.AreEqual(fakeResponse, comparingResult.Value);
        }
    }
}
