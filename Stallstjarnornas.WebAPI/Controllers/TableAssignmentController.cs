using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Stallstjarnornas.WebAPI.DTOs.TableAssignment;
using Stallstjarnornas.WebAPI.Interfaces;


namespace Stallstjarnornas.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TableAssignmentController : ControllerBase
    {
        private readonly ITableAssignmentService _tass;

        public TableAssignmentController(ITableAssignmentService tass)
        {
            _tass = tass;
        }

        [HttpPost("Assign-table")]
        public async Task<ActionResult<TableAssignmentResponseDto>> CreateTableAssignmentAsync(CreateTableAssignmentDto dto)
        {
            try
            {
                var result = await _tass.CreateTableAssignmentAsync(dto);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("Find-Available-Tables")]
        public async Task<ActionResult<GetAvailableTablesResponseDto>> GetAvailableTablesAsync([FromQuery] GetAvailableTablesDto dto)//FromQuery gör att värdena tas från URL:en (det du skriver efter ? i länken)
        {
            try
            {
                var result = await _tass.GetAvailableTablesAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete-Table-Assignments")]
        public async Task<ActionResult> DeleteAssignedTablesAsync(DeleteAssignedTablesDTO dto)
        {
            await _tass.DeleteAssignedTablesAsync(dto);
            return Ok("Table assignment was successfully deleted");
        }

    }
}
