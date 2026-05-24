using Microsoft.AspNetCore.Mvc;
using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Services;

namespace SistemaVeterinario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetController : ControllerBase
    {
        private readonly IPetService _service;
        private readonly ITutorService _tutorService;

        public PetController(IPetService service, ITutorService tutorService)
        {
            _service = service;
            _tutorService = tutorService;
        }

        /// <summary>Lista todos os pets.</summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pet>>> GetAll()
        {
            try { return Ok(await _service.GetAllAsync()); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// <summary>Busca um pet por ID.</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Pet>> GetById(decimal id)
        {
            try
            {
                var p = await _service.GetByIdAsync(id);
                return p == null ? NotFound() : Ok(p);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// <summary>Cadastra um novo pet (Obrigatório informar ID de um tutor existente).</summary>
        [HttpPost]
        public async Task<ActionResult> Create(Pet pet)
        {
            try
            {
                await _service.AddAsync(pet);
                return CreatedAtAction(nameof(GetById), new { id = pet.IdPet }, pet);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// <summary>Atualiza um pet existente.</summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(decimal id, Pet pet)
        {
            if (id != pet.IdPet) return BadRequest("ID divergente.");
            try
            {
                await _service.UpdateAsync(pet);
                return NoContent();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// <summary>Remove um pet pelo ID.</summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(decimal id)
        {
            try { await _service.DeleteAsync(id); return NoContent(); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}