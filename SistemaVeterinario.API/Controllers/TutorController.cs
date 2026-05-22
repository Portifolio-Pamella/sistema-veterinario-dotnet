using Microsoft.AspNetCore.Mvc;
using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TutorController : ControllerBase
    {
        private readonly ITutorService _service;

        public TutorController(ITutorService service)
        {
            _service = service;
        }

        // GET: api/Tutor - Lista todos os tutores cadastrados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tutor>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        // GET: api/Tutor/{id} - Busca um tutor específico por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Tutor>> GetById(decimal id)
        {
            var tutor = await _service.GetByIdAsync(id);
            return tutor == null ? NotFound() : Ok(tutor);
        }

        // POST: api/Tutor - Cadastra um novo tutor
        [HttpPost]
        public async Task<ActionResult> Create(Tutor tutor)
        {
            await _service.AddAsync(tutor);
            return CreatedAtAction(nameof(GetById), new { id = tutor.IdTutor }, tutor);
        }

        // PUT: api/Tutor/{id} - Atualiza dados de um tutor existente
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(decimal id, Tutor tutor)
        {
            if (id != tutor.IdTutor) return BadRequest("ID do tutor divergente.");

            await _service.UpdateAsync(tutor);
            return NoContent();
        }

        // DELETE: api/Tutor/{id} - Remove um tutor do sistema
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(decimal id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}