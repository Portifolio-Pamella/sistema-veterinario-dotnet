using Microsoft.AspNetCore.Mvc;
using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetClinicaController : ControllerBase
    {
        private readonly IPetClinicaService _service;

        public PetClinicaController(IPetClinicaService service)
        {
            _service = service;
        }

        // GET: api/PetClinica - Lista todos os vínculos pet-clínica
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetClinica>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        // GET: api/PetClinica/{id} - Busca um vínculo específico pelo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<PetClinica>> GetById(decimal id)
        {
            var petClinica = await _service.GetByIdAsync(id);
            return petClinica == null ? NotFound() : Ok(petClinica);
        }

        // POST: api/PetClinica - Cria um novo vínculo entre um pet e uma clínica
        [HttpPost]
        public async Task<ActionResult> Create(PetClinica petClinica)
        {
            await _service.AddAsync(petClinica);
            return CreatedAtAction(nameof(GetById), new { id = petClinica.IdPetClinica }, petClinica);
        }

        // PUT: api/PetClinica/{id} - Atualiza um vínculo existente
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(decimal id, PetClinica petClinica)
        {
            if (id != petClinica.IdPetClinica) return BadRequest("ID do vínculo divergente.");

            await _service.UpdateAsync(petClinica);
            return NoContent();
        }

        // DELETE: api/PetClinica/{id} - Remove um vínculo (desvincula pet da clínica)
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(decimal id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}