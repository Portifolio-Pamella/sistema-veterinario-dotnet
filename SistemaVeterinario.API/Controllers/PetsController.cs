using System;
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

        // 1ª Rota: GET api/pets (Listagem geral)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pets = await _repository.GetAllAsync();
            return Ok(pets);
        }

        // 2ª Rota: GET api/pets/{id} (Buscar por ID)
        [HttpGet("{id:decimal}")]
        public async Task<IActionResult> GetById(decimal id)
        {
            var pet = await _repository.GetByIdAsync(id);
            if (pet == null)
            {
                return NotFound(new { message = $"Pet com ID {id} não foi encontrado." });
            }
            return Ok(pet);
        }

        // 3ª Rota: GET api/pets/filtrar (Filtrar por espécie via Query String)
        [HttpGet("filtrar")]
        public async Task<IActionResult> GetByEspecie([FromQuery] string especie)
        {
            if (string.IsNullOrWhiteSpace(especie))
            {
                return BadRequest(new { message = "O parâmetro 'especie' é obrigatório para o filtro." });
            }

            var pets = await _repository.GetByEspecieAsync(especie);
            return Ok(pets);
        }

        // Criação: POST api/pets
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Pet pet)
        {
            if (pet == null)
            {
                return BadRequest(new { message = "Os dados do pet não podem ser nulos." });
            }

            try
            {
                await _repository.AddAsync(pet);
                return CreatedAtAction(nameof(GetById), new { id = pet.IdPet }, pet);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Erro ao salvar no Oracle: {ex.Message}" });
            }
        }

        // Atualização: PUT api/pets/{id}
        [HttpPut("{id:decimal}")]
        public async Task<IActionResult> Update(decimal id, [FromBody] Pet pet)
        {
            if (pet == null || pet.IdPet != id)
            {
                return BadRequest(new { message = "Inconsistência nos dados fornecidos ou IDs divergentes." });
            }

            var existingPet = await _repository.GetByIdAsync(id);
            if (existingPet == null)
            {
                return NotFound(new { message = $"Pet com ID {id} não existe para ser atualizado." });
            }

            try
            {
                existingPet.NomePet = pet.NomePet;
                existingPet.EspeciePet = pet.EspeciePet;
                existingPet.RacaPet = pet.RacaPet;
                existingPet.SexoPet = pet.SexoPet;
                existingPet.PesoPet = pet.PesoPet;
                existingPet.CorPet = pet.CorPet;

                await _repository.UpdateAsync(existingPet);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Erro ao atualizar no Oracle: {ex.Message}" });
            }
        }

        // Remoção: DELETE api/pets/{id}
        [HttpDelete("{id:decimal}")]
        public async Task<IActionResult> Delete(decimal id)
        {
            var existingPet = await _repository.GetByIdAsync(id);
            if (existingPet == null)
            {
                return NotFound(new { message = $"Pet com ID {id} não foi encontrado." });
            }

            try
            {
                await _repository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Erro ao remover do Oracle: {ex.Message}" });
            }
        }
    }
}