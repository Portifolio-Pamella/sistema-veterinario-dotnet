using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Repositories.interfaces;

namespace SistemaVeterinario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeterinariosController : ControllerBase
    {
        private readonly IVeterinarioRepository _repository;

        public VeterinariosController(IVeterinarioRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repository.GetAllAsync());

        [HttpGet("{id:decimal}")]
        public async Task<IActionResult> GetById(decimal id)
        {
            var vet = await _repository.GetByIdAsync(id);
            if (vet == null) return NotFound(new { message = "Veterinário não encontrado." });
            return Ok(vet);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Veterinario veterinario)
        {
            if (veterinario == null) return BadRequest(new { message = "Dados inválidos." });
            try
            {
                await _repository.AddAsync(veterinario);
                return CreatedAtAction(nameof(GetById), new { id = veterinario.IdVeterinario }, veterinario);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id:decimal}")]
        public async Task<IActionResult> Update(decimal id, [FromBody] Veterinario veterinario)
        {
            if (veterinario == null || veterinario.IdVeterinario != id) return BadRequest(new { message = "IDs inconsistentes." });

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound(new { message = "Veterinário não encontrado." });

            try
            {
                await _repository.UpdateAsync(veterinario);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id:decimal}")]
        public async Task<IActionResult> Delete(decimal id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound(new { message = "Veterinário não encontrado." });

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}