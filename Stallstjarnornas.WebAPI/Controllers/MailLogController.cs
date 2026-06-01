using Microsoft.AspNetCore.Mvc;
using Stallstjarnornas.WebAPI.Interfaces;

namespace Stallstjarnornas.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MailLogController : ControllerBase
    {
        private readonly IMailLogService _service;

        public MailLogController(IMailLogService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetMailLog()
        {
            var logs = await _service.GetMailLogAsync();
            return Ok(logs);
        }
    }
}
