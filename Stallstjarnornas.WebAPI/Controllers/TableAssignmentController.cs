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
            var result = await _tass.CreateTableAssignmentAsync(dto);
            return Ok(result);
        }
    }
}
