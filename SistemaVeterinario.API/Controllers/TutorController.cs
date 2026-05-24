using Microsoft.AspNetCore.Mvc;
using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Services;

namespace SistemaVeterinario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TutorController : ControllerBase
    {
        private readonly ITutorService _service;
        public TutorController(ITutorService service) => _service = service;

        /// <summary>Lista todos os tutores.</summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tutor>>> GetAll()
        {
            try { return Ok(await _service.GetAllAsync()); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// <summary>Busca um tutor por ID.</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Tutor>> GetById(decimal id)
        {
            try
            {
                var t = await _service.GetByIdAsync(id);
                return t == null ? NotFound("Tutor não encontrado.") : Ok(t);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// <summary>Cadastra um novo tutor.</summary>
        [HttpPost]
        public async Task<ActionResult> Create(Tutor t)
        {
            try
            {
                await _service.AddAsync(t);
                return CreatedAtAction(nameof(GetById), new { id = t.IdTutor }, t);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// <summary>Atualiza os dados de um tutor.</summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(decimal id, Tutor t)
        {
            if (id != t.IdTutor) return BadRequest("IDs divergentes.");
            try
            {
                await _service.UpdateAsync(t);
                return NoContent();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// <summary>Remove um tutor pelo ID.</summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(decimal id)
        {
            try { await _service.DeleteAsync(id); return NoContent(); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}