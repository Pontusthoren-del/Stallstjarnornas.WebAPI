using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.OpenApi.Validations;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
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

namespace Stallstjarnornas.Test.ControllerTest
{
    [TestClass]
    public class TableAssignmentControllerTests
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
            //Test av controllern
            [TestMethod]
            //Test steg för steg: 
            //1. jag arrangerar först vad jag skickar in till controllern med assignment (input-data)
            //2. jag sätter upp vilken respons min FEJKADE service ska ge tillbaka via fake-response
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
                var result = await _controller.GetAvailableTablesAsync(assignment);
                var comparingResult = result.Result as OkObjectResult;//måste "plocka ut" svaret ur http-wrapper för svaret är ett actionresult och inte en dto
                                                                      //Assert
                Assert.AreEqual(fakeResponse, comparingResult.Value);
            }

            //JAg vill testa att bad request returneras med detta test. (ska ske ifall exception kastas från servicen alltså måste jag fejka det)
            //Testet ser ifall bad request returneras när exception castas (oavsett vilket)
            [TestMethod]
            public async Task GetAvailableTables_ShouldReturnBadRequest_WhenServiceThrowsException()
            {
                var fakeRequest = new GetAvailableTablesDto(new DateOnly(2027, 5, 10), 3);
                _mockTableAssignmentService.Setup(tas => tas.GetAvailableTablesAsync(fakeRequest)).ThrowsAsync(new Exception());
                var actual = await _controller.GetAvailableTablesAsync(fakeRequest);
                Assert.IsInstanceOfType( actual.Result,typeof(BadRequestObjectResult));//Ser ifall svaret är av typen bad request object
            }

            [TestMethod]
            public async Task CreateTableAssignment_ShouldReturnOKresponse_WhenInputIsCorrect()
            {
                var assignment = new CreateTableAssignmentDto(1, new List<int> { 1, 2 });
                var fakeResponse = new TableAssignmentResponseDto(assignment.TableIds, assignment.bookingNumber, "Testman", 4, new DateOnly(2027, 05, 10), 1);
                _mockTableAssignmentService.Setup(tas => tas.CreateTableAssignmentAsync(assignment)).ReturnsAsync(fakeResponse);
                var actual = await _controller.CreateTableAssignmentAsync(assignment);
                var x=actual.Result as OkObjectResult;
                Assert.AreEqual(x.Value, fakeResponse);
            }

            [TestMethod]
            public async Task CreateTableAssignment_ShouldReturnBadrequest_WhenServiceThrowsException()
            {
                var assignment = new CreateTableAssignmentDto(1, new List<int> { 1, 2 });
                _mockTableAssignmentService.Setup(tas => tas.CreateTableAssignmentAsync(assignment)).ThrowsAsync(new Exception());
                var actual = await _controller.CreateTableAssignmentAsync(assignment);

                Assert.IsInstanceOfType(actual.Result, typeof(BadRequestObjectResult));
            }

            [TestMethod]
            public async Task DeleteTableAssignment_ShouldReturnOkResponse_WhenCorrectInput()
            {
                var assignment = new DeleteAssignedTablesDTO(1);
                _mockTableAssignmentService.Setup(tas => tas.DeleteAssignedTablesAsync(assignment)); //metoder returnerar inget så krävs endast att actual blir ett okresponseObjekt.
                var actual = await _controller.DeleteAssignedTablesAsync(assignment);

                Assert.IsInstanceOfType(actual, typeof(OkObjectResult));


            }

            [TestMethod]
            public async Task DeleteTableAssignment_ShouldReturnBadRequest_WhenServiceThrowsException()
            {
                var assignment = new DeleteAssignedTablesDTO(1);
                _mockTableAssignmentService.Setup(tas => tas.DeleteAssignedTablesAsync(assignment)).ThrowsAsync(new Exception());
                var actual = await _controller.DeleteAssignedTablesAsync(assignment);

                Assert.IsInstanceOfType(actual, typeof(BadRequestObjectResult));
            }
        }
    }
}
