using Microsoft.AspNetCore.Mvc;
using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetController : ControllerBase
    {
        private readonly IPetService _service;

        public PetController(IPetService service)
        {
            _service = service;
        }

        // GET: api/Pet - Lista todos os pets cadastrados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pet>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        // GET: api/Pet/{id} - Busca um pet específico por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Pet>> GetById(decimal id)
        {
            var pet = await _service.GetByIdAsync(id);
            return pet == null ? NotFound() : Ok(pet);
        }

        // POST: api/Pet - Cadastra um novo pet
        [HttpPost]
        public async Task<ActionResult> Create(Pet pet)
        {
            await _service.AddAsync(pet);
            return CreatedAtAction(nameof(GetById), new { id = pet.IdPet }, pet);
        }

        // PUT: api/Pet/{id} - Atualiza dados do pet
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(decimal id, Pet pet)
        {
            if (id != pet.IdPet) return BadRequest("ID do pet divergente.");

            await _service.UpdateAsync(pet);
            return NoContent();
        }

        // DELETE: api/Pet/{id} - Remove um pet do sistema
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(decimal id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}