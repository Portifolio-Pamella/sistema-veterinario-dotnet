using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SistemaVeterinario.Domain.Models;
using SistemaVeterinario.Application.Service.Interface;
using SistemaVeterinario.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace SistemaVeterinario.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PetController : ControllerBase
    {
        private readonly IPetService _service;
        private readonly ILogger<PetController> _logger;

        public PetController(IPetService service, ILogger<PetController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet(Name = "GetPets")]
        [ProducesResponseType(typeof(PagedResponse<PetDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponse<PetDto>>> GetAll()
        {
            _logger.LogInformation("Iniciando listagem paginada de pets.");
            try
            {
                int pageNumber = int.TryParse(Request.Query["pageNumber"], out var pNum) ? pNum : 1;
                int pageSize = int.TryParse(Request.Query["pageSize"], out var pSize) ? pSize : 10;
                string? especie = Request.Query["especie"].FirstOrDefault();
                string? search = Request.Query["search"].FirstOrDefault();
                string sortBy = Request.Query["sortBy"].FirstOrDefault() ?? "nome";
                bool ascending = bool.TryParse(Request.Query["ascending"], out var asc) ? asc : true;

                if (pageNumber < 1) pageNumber = 1;
                if (pageSize < 1) pageSize = 10;
                if (pageSize > 50) pageSize = 50;

                var pets = await _service.GetAllAsync();
                var query = pets.AsQueryable();

                // 1. Filtros
                if (!string.IsNullOrWhiteSpace(especie))
                    query = query.Where(p => p.EspeciePet != null && p.EspeciePet.ToLower().Contains(especie.Trim().ToLower()));

                if (!string.IsNullOrWhiteSpace(search))
                    query = query.Where(p => p.NomePet != null && p.NomePet.ToLower().Contains(search.Trim().ToLower()));

                // 2. Ordenação dinâmica
                query = sortBy.ToLower() switch
                {
                    "raca" => ascending ? query.OrderBy(p => p.RacaPet) : query.OrderByDescending(p => p.RacaPet),
                    "peso" => ascending ? query.OrderBy(p => p.PesoPet) : query.OrderByDescending(p => p.PesoPet),
                    _ => ascending ? query.OrderBy(p => p.NomePet) : query.OrderByDescending(p => p.NomePet)
                };

                // 3. Paginação
                var totalRecords = query.Count();
                var items = query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

                // 4. Mapeamento para DTO com HATEOAS
                var dtos = items.Select(CreatePetDto).ToList();

                // 5. Links HATEOAS globais
                var paginationLinks = CreatePaginationLinks(pageNumber, pageSize, especie, search, sortBy, ascending, totalPages);

                var response = new PagedResponse<PetDto>(
                    dtos,
                    pageNumber,
                    pageSize,
                    totalPages,
                    totalRecords,
                    paginationLinks
                );

                _logger.LogInformation("Listagem paginada de pets realizada com sucesso. Total: {Count}", totalRecords);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar pets.");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}", Name = "GetPetById")]
        public async Task<ActionResult<PetDto>> GetById(decimal id)
        {
            _logger.LogInformation("Iniciando busca do pet por ID: {PetId}", id);
            try
            {
                var p = await _service.GetByIdAsync(id);
                if (p == null)
                {
                    _logger.LogWarning("Pet não encontrado para o ID: {PetId}", id);
                    return NotFound();
                }
                _logger.LogInformation("Pet encontrado com sucesso para o ID: {PetId}", id);
                return Ok(CreatePetDto(p));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar pet por ID: {PetId}", id);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(Pet pet)
        {
            _logger.LogInformation("Iniciando cadastro de novo pet.");
            try
            {
                await _service.AddAsync(pet);
                _logger.LogInformation("Pet cadastrado com sucesso. ID: {PetId}", pet?.IdPet);
                return CreatedAtAction(nameof(GetById), new { id = pet.IdPet }, pet);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao cadastrar pet.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(decimal id, Pet pet)
        {
            if (id != pet.IdPet)
            {
                _logger.LogWarning("ID divergente na atualização de pet. Rota: {RouteId}, Corpo: {BodyId}", id, pet?.IdPet);
                return BadRequest("ID divergente.");
            }
            _logger.LogInformation("Iniciando atualização do pet com ID: {PetId}", id);
            try
            {
                await _service.UpdateAsync(pet);
                _logger.LogInformation("Pet atualizado com sucesso. ID: {PetId}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar pet com ID: {PetId}", id);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}", Name = "DeletePet")]
        public async Task<ActionResult> Delete(decimal id)
        {
            _logger.LogInformation("Iniciando exclusão do pet com ID: {PetId}", id);
            try
            {
                await _service.DeleteAsync(id);
                _logger.LogInformation("Pet excluído com sucesso. ID: {PetId}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir pet com ID: {PetId}", id);
                return BadRequest(ex.Message);
            }
        }

        private PetDto CreatePetDto(Pet p)
        {
            var links = new List<LinkDto>
            {
                new(Url.Link("GetPetById", new { id = p.IdPet })!, "self", "GET"),
                new(Url.Link("DeletePet", new { id = p.IdPet })!, "delete_pet", "DELETE")
            };

            return new PetDto(
                p.IdPet,
                p.IdTutor,
                p.NomePet,
                p.EspeciePet,
                p.RacaPet,
                p.SexoPet,
                p.DataNascimentoPet,
                p.PesoPet,
                p.CorPet,
                p.DataCadastroPet,
                links
            );
        }

        private List<LinkDto> CreatePaginationLinks(int pageNumber, int pageSize, string? especie, string? search, string sortBy, bool ascending, int totalPages)
        {
            var routeValues = new { pageNumber, pageSize, especie, search, sortBy, ascending };
            var links = new List<LinkDto>
            {
                new(Url.Link("GetPets", routeValues)!, "self", "GET")
            };

            if (pageNumber < totalPages)
            {
                var nextValues = new { pageNumber = pageNumber + 1, pageSize, especie, search, sortBy, ascending };
                links.Add(new(Url.Link("GetPets", nextValues)!, "next_page", "GET"));
            }

            if (pageNumber > 1)
            {
                var prevValues = new { pageNumber = pageNumber - 1, pageSize, especie, search, sortBy, ascending };
                links.Add(new(Url.Link("GetPets", prevValues)!, "prev_page", "GET"));
            }

            return links;
        }
    }
}