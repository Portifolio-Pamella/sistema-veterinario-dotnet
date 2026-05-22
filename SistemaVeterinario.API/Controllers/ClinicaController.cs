using Microsoft.AspNetCore.Mvc;
using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Services; // Apenas o plural
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClinicaController : ControllerBase
    {
        private readonly IClinicaService _service;

        public ClinicaController(IClinicaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clinica>>> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Clinica>> GetById(decimal id)
        {
            var clinica = await _service.GetByIdAsync(id);
            return clinica == null ? NotFound() : Ok(clinica);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Clinica clinica)
        {
            await _service.AddAsync(clinica);
            return CreatedAtAction(nameof(GetById), new { id = clinica.IdClinica }, clinica);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(decimal id, Clinica clinica)
        {
            if (id != clinica.IdClinica) return BadRequest("ID da clínica inválido.");
            await _service.UpdateAsync(clinica);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(decimal id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}