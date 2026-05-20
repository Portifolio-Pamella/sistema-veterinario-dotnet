using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Repositories.interfaces;

namespace SistemaVeterinario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TutoresController : ControllerBase
    {
        private readonly ITutorRepository _repository;

        public TutoresController(ITutorRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repository.GetAllAsync());

        [HttpGet("{id:decimal}")]
        public async Task<IActionResult> GetById(decimal id)
        {
            var tutor = await _repository.GetByIdAsync(id);
            if (tutor == null) return NotFound(new { message = "Tutor não encontrado." });
            return Ok(tutor);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Tutor tutor)
        {
            if (tutor == null) return BadRequest(new { message = "Dados inválidos." });
            try
            {
                await _repository.AddAsync(tutor);
                return CreatedAtAction(nameof(GetById), new { id = tutor.IdTutor }, tutor);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id:decimal}")]
        public async Task<IActionResult> Update(decimal id, [FromBody] Tutor tutor)
        {
            if (tutor == null || tutor.IdTutor != id) return BadRequest(new { message = "IDs inconsistentes." });

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound(new { message = "Tutor não encontrado." });

            try
            {
                await _repository.UpdateAsync(tutor);
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
            if (existing == null) return NotFound(new { message = "Tutor não encontrado." });

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}