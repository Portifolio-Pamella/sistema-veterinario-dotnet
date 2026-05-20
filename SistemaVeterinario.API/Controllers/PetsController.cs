using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Repositories;

namespace SistemaVeterinario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly IPetRepository _repository;

        public PetsController(IPetRepository repository)
        {
            _repository = repository;
        }

        // 1ª Rota Parametrizada / Listagem Geral: GET /api/pets
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pets = await _repository.GetAllAsync();
            return Ok(pets); // Retorna HTTP 200
        }

        // 2ª Rota Parametrizada / Buscar por ID: GET /api/pets/{id}
        [HttpGet("{id:decimal}")]
        public async Task<IActionResult> GetById(decimal id)
        {
            var pet = await _repository.GetByIdAsync(id);
            if (pet == null) return NotFound(new { message = "Pet não encontrado." }); // Retorna HTTP 404

            return Ok(pet); // Retorna HTTP 200
        }

        // 3ª Rota Parametrizada / Filtrar por Espécie: GET /api/pets/especie/{especie}
        [HttpGet("especie/{especie}")]
        public async Task<IActionResult> GetByEspecie(string especie)
        {
            if (string.IsNullOrWhiteSpace(especie)) return BadRequest(new { message = "A espécie deve ser informada." }); // Retorna HTTP 400

            var pets = await _repository.GetByEspecieAsync(especie);
            return Ok(pets);
        }

        // Criação de Recurso: POST /api/pets
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Pet pet)
        {
            if (pet == null) return BadRequest(new { message = "Dados inválidos." });

            await _repository.AddAsync(pet);
            // Retorna HTTP 201 e indica onde o recurso criado pode ser acessado
            return CreatedAtAction(nameof(GetById), new { id = pet.IdPet }, pet);
        }

        // Atualização: PUT /api/pets/{id}
        [SuppressMessage("Style", "IDE0060")]
        [HttpPut("{id:decimal}")]
        public async Task<IActionResult> Update(decimal id, [FromBody] Pet pet)
        {
            if (pet == null) return BadRequest(new { message = "Dados do pet inválidos." });

            var existingPet = await _repository.GetByIdAsync(id);
            if (existingPet == null) return NotFound(new { message = "Pet não encontrado para atualização." });

            await _repository.UpdateAsync(pet);
            return NoContent(); // Retorna HTTP 204
        }

        // Remoção: DELETE /api/pets/{id}
        [HttpDelete("{id:decimal}")]
        public async Task<IActionResult> Delete(decimal id)
        {
            var existingPet = await _repository.GetByIdAsync(id);
            if (existingPet == null) return NotFound(new { message = "Pet não encontrado." });

            await _repository.DeleteAsync(id);
            return NoContent(); // Retorna HTTP 204
        }
    }
}