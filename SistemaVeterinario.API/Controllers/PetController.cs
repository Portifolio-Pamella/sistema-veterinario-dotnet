using Microsoft.AspNetCore.Mvc;
using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Services; // Certifique-se de que os Services estão aqui
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Controllers
{
    [ApiController] // Indica que a classe é um controlador de API, habilitando comportamentos automáticos como validação de modelo
    [Route("api/[controller]")] // Define a rota base: "api/pet"
    public class PetController : ControllerBase
    {
        // Injeção de dependência dos serviços que contêm a lógica de negócio
        private readonly IPetService _service;
        private readonly ITutorService _tutorService;

        public PetController(IPetService service, ITutorService tutorService)
        {
            _service = service;
            _tutorService = tutorService;
        }

        // [HttpGet]: Retorna a lista de todos os pets cadastrados no banco
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pet>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        // [HttpGet("{id}")]: Busca um pet específico pelo seu ID único
        [HttpGet("{id}")]
        public async Task<ActionResult<Pet>> GetById(decimal id)
        {
            var pet = await _service.GetByIdAsync(id);
            return pet == null ? NotFound() : Ok(pet); // Retorna 404 se não achar, ou 200 com o pet
        }

        // [HttpPost]: Cadastra um novo pet no sistema
        [HttpPost]
        public async Task<ActionResult> Create(Pet pet)
        {
            await _service.AddAsync(pet);
            // Retorna o status 201 (Created) e o link para o objeto recém criado
            return CreatedAtAction(nameof(GetById), new { id = pet.IdPet }, pet);
        }

        // [HttpPut("{id}")]: Atualiza os dados de um pet existente
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(decimal id, Pet pet)
        {
            // Verifica se o ID da rota é igual ao ID do objeto enviado (segurança)
            if (id != pet.IdPet) return BadRequest("ID do pet divergente.");

            // Validação de negócio: Verifica se o tutor atribuído ao pet realmente existe antes de salvar
            var tutorExistente = await _tutorService.GetByIdAsync(pet.IdTutor);
            if (tutorExistente == null)
            {
                return BadRequest($"O tutor com ID {pet.IdTutor} não foi encontrado.");
            }

            await _service.UpdateAsync(pet);
            return NoContent(); // Retorna 204 indicando sucesso sem conteúdo adicional
        }

        // [HttpDelete("{id}")]: Remove um pet do sistema pelo ID
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(decimal id)
        {
            await _service.DeleteAsync(id);
            return NoContent(); // Retorna 204 após a exclusão bem-sucedida
        }
    }
}