using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Repositories.interfaces;

namespace SistemaVeterinario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClinicasController : ControllerBase
    {
        private readonly IClinicaRepository _repository;

        public ClinicasController(IClinicaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repository.GetAllAsync());

        [HttpGet("{id:decimal}")]
        public async Task<IActionResult> GetById(decimal id)
        {
            var clinica = await _repository.GetByIdAsync(id);
            if (clinica == null) return NotFound(new { message = "Clínica não encontrada." });
            return Ok(clinica);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Clinica clinica)
        {
            if (clinica == null) return BadRequest(new { message = "Dados inválidos." });
            try
            {
                await _repository.AddAsync(clinica);
                return CreatedAtAction(nameof(GetById), new { id = clinica.IdClinica }, clinica);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id:decimal}")]
        public async Task<IActionResult> Update(decimal id, [FromBody] Clinica clinica)
        {
            if (clinica == null || clinica.IdClinica != id) return BadRequest(new { message = "IDs inconsistentes." });

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound(new { message = "Clínica não encontrada." });

            try
            {
                await _repository.UpdateAsync(clinica);
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
            if (existing == null) return NotFound(new { message = "Clínica não encontrada." });

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}