using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Repositories;

namespace SistemaVeterinario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Rota base: api/pets
    public class PetsController : ControllerBase
    {
        private readonly IPetRepository _repository;

        public PetsController(IPetRepository repository)
        {
            _repository = repository;
        }

        #region 1. CRUD Completo - Métodos GET (3 rotas parametrizadas)

        // 1ª Rota: GET api/pets (Listagem geral)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pets = await _repository.GetAllAsync();
            return Ok(pets); // Retorna HTTP 200 Ok
        }

        // 2ª Rota: GET api/pets/{id} (Buscar por ID específico)
        [HttpGet("{id:decimal}")]
        public async Task<IActionResult> GetById(decimal id)
        {
            var pet = await _repository.GetByIdAsync(id);

            if (pet == null)
            {
                // Retorna HTTP 404 NotFound caso o ID não exista no Oracle
                return NotFound(new { message = $"Pet com ID {id} não foi encontrado." });
            }

            return Ok(pet); // Retorna HTTP 200 Ok
        }

        // 3ª Rota: GET api/pets/filtrar (Buscar por espécie via Query String: api/pets/filtrar?especie=Cachorro)
        [HttpGet("filtrar")]
        public async Task<IActionResult> GetByEspecie([FromQuery] string especie)
        {
            if (string.IsNullOrWhiteSpace(especie))
            {
                // Retorna HTTP 400 BadRequest se o parâmetro vier vazio
                return BadRequest(new { message = "O parâmetro 'especie' é obrigatório para o filtro." });
            }

            var pets = await _repository.GetByEspecieAsync(especie);
            return Ok(pets); // Retorna HTTP 200 Ok
        }

        #endregion

        #region 2. CRUD Completo - POST, PUT e DELETE

        // Criação de Recurso: POST api/pets
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Pet pet)
        {
            if (pet == null)
            {
                return BadRequest(new { message = "Os dados do pet não podem ser nulos." }); // Retorna HTTP 400
            }

            // Validações básicas antes de mandar para o banco
            [cite_start] if (string.IsNullOrWhiteSpace(pet.NomePet) || pet.IdTutor <= 0) [cite: 26]
            {
                [cite_start] return BadRequest(new { message = "Nome do pet e ID do Tutor são obrigatórios." }); [cite: 26]
            }

            try
            {
                await _repository.AddAsync(pet);

                // Retorna HTTP 201 Created e preenche o Header 'Location' com a URL do GetById
                [cite_start] return CreatedAtAction(nameof(GetById), new { id = pet.IdPet }, pet); [cite: 26]
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
            [cite_start] if (pet == null || pet.IdPet != id) [cite: 26]
            {
                return BadRequest(new { message = "Inconsistência nos dados fornecidos ou IDs divergentes." });
            }

            var existingPet = await _repository.GetByIdAsync(id);
            if (existingPet == null)
            {
                return NotFound(new { message = $"Pet com ID {id} não existe para ser atualizado." }); // Retorna HTTP 404
            }

            try
            {
                // Atualiza as propriedades do pet existente
                [cite_start] existingPet.NomePet = pet.NomePet; [cite: 26]
                [cite_start] existingPet.EspeciePet = pet.EspeciePet; [cite: 26]
                [cite_start] existingPet.RacaPet = pet.RacaPet; [cite: 26]
                [cite_start] existingPet.SexoPet = pet.SexoPet; [cite: 26]
                [cite_start] existingPet.PesoPet = pet.PesoPet; [cite: 26]
                [cite_start] existingPet.CorPet = pet.CorPet; [cite: 26]

                await _repository.UpdateAsync(existingPet);
                return NoContent(); // Retorna HTTP 204 NoContent (Sucesso sem conteúdo no corpo)
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
                return NotFound(new { message = $"Pet com ID {id} não foi encontrado." }); // Retorna HTTP 404
            }

            try
            {
                await _repository.DeleteAsync(id);
                return NoContent(); // Retorna HTTP 204 NoContent
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Erro ao remover do Oracle: {ex.Message}" });
            }
        }

        #endregion
    }
}