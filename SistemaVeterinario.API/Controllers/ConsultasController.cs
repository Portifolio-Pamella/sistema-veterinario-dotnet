using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Repositories.interfaces;

namespace SistemaVeterinario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultasController : ControllerBase
    {
        private readonly IConsultaRepository _repository;

        public ConsultasController(IConsultaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repository.GetAllAsync());

        [HttpGet("{id:decimal}")]
        public async Task<IActionResult> GetById(decimal id)
        {
            var consulta = await _repository.GetByIdAsync(id);
            if (consulta == null) return NotFound(new { message = "Consulta não encontrada." });
            return Ok(consulta);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Consulta consulta)
        {
            if (consulta == null) return BadRequest(new { message = "Dados inválidos." });
            try
            {
                await _repository.AddAsync(consulta);
                return CreatedAtAction(nameof(GetById), new { id = consulta.IdConsulta }, consulta);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id:decimal}")]
        public async Task<IActionResult> Update(decimal id, [FromBody] Consulta consulta)
        {
            if (consulta == null || consulta.IdConsulta != id) return BadRequest(new { message = "IDs inconsistentes." });

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound(new { message = "Consulta não encontrada." });

            try
            {
                await _repository.UpdateAsync(consulta);
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
            if (existing == null) return NotFound(new { message = "Consulta não encontrada." });

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}