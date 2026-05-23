using Microsoft.AspNetCore.Mvc;
using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Services; // Certifique-se de que os Services estão aqui
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pet>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pet>> GetById(decimal id)
        {
            var pet = await _service.GetByIdAsync(id);
            return pet == null ? NotFound() : Ok(pet);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Pet pet)
        {
            await _service.AddAsync(pet);
            return CreatedAtAction(nameof(GetById), new { id = pet.IdPet }, pet);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(decimal id, Pet pet)
        {
            if (id != pet.IdPet) return BadRequest("ID do pet divergente.");

            var tutorExistente = await _tutorService.GetByIdAsync(pet.IdTutor);
            if (tutorExistente == null)
            {
                return BadRequest($"O tutor com ID {pet.IdTutor} não foi encontrado.");
            }

            await _service.UpdateAsync(pet);
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