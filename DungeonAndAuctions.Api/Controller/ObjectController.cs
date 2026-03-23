using D_A.Application.Services.Interfaces.Api;
using Microsoft.AspNetCore.Mvc;

namespace DungeonAndAuctions.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ObjectController : ControllerBase
    {
        private readonly IUserApiQueryService _service;

        public ObjectController(IUserApiQueryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync()
        {
            var result = await _service.ListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindByIdAsync(int id)
        {
            var result = await _service.FindByIdAsync(id);
            if (result is null) return NotFound();
            return Ok(result);
        }
    }
}
