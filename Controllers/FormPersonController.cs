using FormAPI.DTOs;
using FormAPI.Entity;
using FormAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FormAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormPersonController : ControllerBase
    {
        private readonly FormPersonService _service;

        public FormPersonController(FormPersonService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFormPersonDto dto)
        {
            var newUser = await _service.CreateAsync(dto);

            return Ok(newUser);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            List<ResponseFormPersonDto> users = await _service.GetAllAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            ResponseFormPersonDto user = await _service.GetByIdAsync(id);

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateFormPersonDto updateuser)
        {
            var user = await _service.UpdateAsync(id, updateuser);

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
