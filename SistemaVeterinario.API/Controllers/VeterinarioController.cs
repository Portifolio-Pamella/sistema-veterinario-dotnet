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
    public class VeterinarioController : ControllerBase
    {
        private readonly IVeterinarioService _service;
        private readonly ILogger<VeterinarioController> _logger;

        public VeterinarioController(IVeterinarioService service, ILogger<VeterinarioController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet(Name = "GetVeterinarios")]
        [ProducesResponseType(typeof(PagedResponse<VeterinarioDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponse<VeterinarioDto>>> GetAll()
        {
            _logger.LogInformation("Iniciando listagem paginada de veterinários.");
            try
            {
                // Captura de parâmetros via Query String com valores padrão
                int pageNumber = int.TryParse(Request.Query["pageNumber"], out var pNum) ? pNum : 1;
                int pageSize = int.TryParse(Request.Query["pageSize"], out var pSize) ? pSize : 10;
                string? especialidade = Request.Query["especialidade"].FirstOrDefault();
                string? search = Request.Query["search"].FirstOrDefault();
                string sortBy = Request.Query["sortBy"].FirstOrDefault() ?? "nome";
                bool ascending = bool.TryParse(Request.Query["ascending"], out var asc) ? asc : true;

                // Proteção contra paginação inválida
                if (pageNumber < 1) pageNumber = 1;
                if (pageSize < 1) pageSize = 10;
                if (pageSize > 50) pageSize = 50;

                var veterinarios = await _service.GetAllAsync();
                var query = veterinarios.AsQueryable();

                // 1. Filtros
                if (!string.IsNullOrWhiteSpace(especialidade))
                    query = query.Where(v => v.EspecialidadeVeterinario != null &&
                                             v.EspecialidadeVeterinario.ToLower().Contains(especialidade.Trim().ToLower()));

                if (!string.IsNullOrWhiteSpace(search))
                    query = query.Where(v => (v.NomeVeterinario != null && v.NomeVeterinario.ToLower().Contains(search.Trim().ToLower())) ||
                                             (v.CrmVeterinario != null && v.CrmVeterinario.ToLower().Contains(search.Trim().ToLower())));

                // 2. Ordenação dinâmica
                query = sortBy.ToLower() switch
                {
                    "crm" => ascending ? query.OrderBy(v => v.CrmVeterinario) : query.OrderByDescending(v => v.CrmVeterinario),
                    "especialidade" => ascending ? query.OrderBy(v => v.EspecialidadeVeterinario) : query.OrderByDescending(v => v.EspecialidadeVeterinario),
                    _ => ascending ? query.OrderBy(v => v.NomeVeterinario) : query.OrderByDescending(v => v.NomeVeterinario)
                };

                // 3. Paginação (Skip / Take)
                var totalRecords = query.Count();
                var items = query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

                // 4. Mapeamento para DTO com HATEOAS nos itens individuais
                var dtos = items.Select(CreateVeterinarioDto).ToList();

                // 5. Links HATEOAS globais de navegação da página
                var paginationLinks = CreatePaginationLinks(pageNumber, pageSize, especialidade, search, sortBy, ascending, totalPages);

                var response = new PagedResponse<VeterinarioDto>(
                    dtos,
                    pageNumber,
                    pageSize,
                    totalPages,
                    totalRecords,
                    paginationLinks
                );

                _logger.LogInformation("Listagem paginada realizada com sucesso. Total encontrados: {Count}", totalRecords);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar veterinários.");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}", Name = "GetVeterinarioById")]
        public async Task<ActionResult<VeterinarioDto>> GetById(decimal id)
        {
            _logger.LogInformation("Iniciando busca do veterinário por ID: {VetId}", id);
            try
            {
                var v = await _service.GetByIdAsync(id);
                if (v == null)
                {
                    _logger.LogWarning("Veterinário não encontrado para o ID: {VetId}", id);
                    return NotFound("Veterinário não encontrado.");
                }
                _logger.LogInformation("Veterinário encontrado com sucesso para o ID: {VetId}", id);
                return Ok(CreateVeterinarioDto(v));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar veterinário por ID: {VetId}", id);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(Veterinario v)
        {
            _logger.LogInformation("Iniciando cadastro de novo veterinário.");
            try
            {
                await _service.AddAsync(v);
                _logger.LogInformation("Veterinário cadastrado com sucesso. ID: {VetId}", v?.IdVeterinario);
                return CreatedAtAction(nameof(GetById), new { id = v.IdVeterinario }, v);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao cadastrar veterinário.");
                return BadRequest($"Erro ao cadastrar: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(decimal id, Veterinario v)
        {
            if (id != v.IdVeterinario)
            {
                _logger.LogWarning("IDs divergentes na atualização de veterinário. Rota: {RouteId}, Corpo: {BodyId}", id, v?.IdVeterinario);
                return BadRequest("IDs divergentes.");
            }
            _logger.LogInformation("Iniciando atualização do veterinário com ID: {VetId}", id);
            try
            {
                await _service.UpdateAsync(v);
                _logger.LogInformation("Veterinário atualizado com sucesso. ID: {VetId}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar veterinário com ID: {VetId}", id);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}", Name = "DeleteVeterinario")]
        public async Task<ActionResult> Delete(decimal id)
        {
            _logger.LogInformation("Iniciando exclusão do veterinário com ID: {VetId}", id);
            try
            {
                await _service.DeleteAsync(id);
                _logger.LogInformation("Veterinário excluído com sucesso. ID: {VetId}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir veterinário com ID: {VetId}", id);
                return BadRequest(ex.Message);
            }
        }

        private VeterinarioDto CreateVeterinarioDto(Veterinario v)
        {
            var links = new List<LinkDto>
            {
                new(Url.Link("GetVeterinarioById", new { id = v.IdVeterinario })!, "self", "GET"),
                new(Url.Link("DeleteVeterinario", new { id = v.IdVeterinario })!, "delete_veterinario", "DELETE")
            };

            return new VeterinarioDto(
                v.IdVeterinario,
                v.NomeVeterinario,
                v.CrmVeterinario,
                v.EspecialidadeVeterinario,
                v.TelefoneVeterinario,
                v.EmailVeterinario,
                v.StatusVeterinario,
                v.DataCadastroVeterinario,
                links
            );
        }

        private List<LinkDto> CreatePaginationLinks(int pageNumber, int pageSize, string? especialidade, string? search, string sortBy, bool ascending, int totalPages)
        {
            var routeValues = new { pageNumber, pageSize, especialidade, search, sortBy, ascending };

            var links = new List<LinkDto>
            {
                new(Url.Link("GetVeterinarios", routeValues)!, "self", "GET")
            };

            if (pageNumber < totalPages)
            {
                var nextRouteValues = new { pageNumber = pageNumber + 1, pageSize, especialidade, search, sortBy, ascending };
                links.Add(new(Url.Link("GetVeterinarios", nextRouteValues)!, "next_page", "GET"));
            }

            if (pageNumber > 1)
            {
                var prevRouteValues = new { pageNumber = pageNumber - 1, pageSize, especialidade, search, sortBy, ascending };
                links.Add(new(Url.Link("GetVeterinarios", prevRouteValues)!, "prev_page", "GET"));
            }

            return links;
        }
    }
}