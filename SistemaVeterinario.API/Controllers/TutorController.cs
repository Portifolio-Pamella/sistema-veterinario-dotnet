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
    public class TutorController : ControllerBase
    {
        private readonly ITutorService _service;
        private readonly ILogger<TutorController> _logger;

        public TutorController(ITutorService service, ILogger<TutorController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet(Name = "GetTutores")]
        [ProducesResponseType(typeof(PagedResponse<TutorDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponse<TutorDto>>> GetAll()
        {
            _logger.LogInformation("Iniciando listagem paginada de tutores.");
            try
            {
                int pageNumber = int.TryParse(Request.Query["pageNumber"], out var pNum) ? pNum : 1;
                int pageSize = int.TryParse(Request.Query["pageSize"], out var pSize) ? pSize : 10;
                string? cidade = Request.Query["cidade"].FirstOrDefault();
                string? search = Request.Query["search"].FirstOrDefault();
                string sortBy = Request.Query["sortBy"].FirstOrDefault() ?? "nome";
                bool ascending = bool.TryParse(Request.Query["ascending"], out var asc) ? asc : true;

                if (pageNumber < 1) pageNumber = 1;
                if (pageSize < 1) pageSize = 10;
                if (pageSize > 50) pageSize = 50;

                var tutores = await _service.GetAllAsync();
                var query = tutores.AsQueryable();

                // 1. Filtros
                if (!string.IsNullOrWhiteSpace(cidade))
                    query = query.Where(t => t.CidadeTutor != null && t.CidadeTutor.ToLower().Contains(cidade.Trim().ToLower()));

                if (!string.IsNullOrWhiteSpace(search))
                    query = query.Where(t => (t.NomeTutor != null && t.NomeTutor.ToLower().Contains(search.Trim().ToLower())) ||
                                             (t.CpfTutor != null && t.CpfTutor.Contains(search.Trim())));

                // 2. Ordenação dinâmica
                query = sortBy.ToLower() switch
                {
                    "email" => ascending ? query.OrderBy(t => t.EmailTutor) : query.OrderByDescending(t => t.EmailTutor),
                    "cidade" => ascending ? query.OrderBy(t => t.CidadeTutor) : query.OrderByDescending(t => t.CidadeTutor),
                    _ => ascending ? query.OrderBy(t => t.NomeTutor) : query.OrderByDescending(t => t.NomeTutor)
                };

                // 3. Paginação
                var totalRecords = query.Count();
                var items = query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

                // 4. Mapeamento para DTO com HATEOAS
                var dtos = items.Select(CreateTutorDto).ToList();

                // 5. Links HATEOAS globais
                var paginationLinks = CreatePaginationLinks(pageNumber, pageSize, cidade, search, sortBy, ascending, totalPages);

                var response = new PagedResponse<TutorDto>(
                    dtos,
                    pageNumber,
                    pageSize,
                    totalPages,
                    totalRecords,
                    paginationLinks
                );

                _logger.LogInformation("Listagem paginada de tutores realizada com sucesso. Total: {Count}", totalRecords);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar tutores.");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}", Name = "GetTutorById")]
        public async Task<ActionResult<TutorDto>> GetById(decimal id)
        {
            _logger.LogInformation("Iniciando busca do tutor por ID: {TutorId}", id);
            try
            {
                var t = await _service.GetByIdAsync(id);
                if (t == null)
                {
                    _logger.LogWarning("Tutor não encontrado para o ID: {TutorId}", id);
                    return NotFound();
                }
                _logger.LogInformation("Tutor encontrado com sucesso para o ID: {TutorId}", id);
                return Ok(CreateTutorDto(t));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar tutor por ID: {TutorId}", id);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(Tutor tutor)
        {
            _logger.LogInformation("Iniciando cadastro de novo tutor.");
            try
            {
                await _service.AddAsync(tutor);
                _logger.LogInformation("Tutor cadastrado com sucesso. ID: {TutorId}", tutor?.IdTutor);
                return CreatedAtAction(nameof(GetById), new { id = tutor.IdTutor }, tutor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao cadastrar tutor.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(decimal id, Tutor tutor)
        {
            if (id != tutor.IdTutor)
            {
                _logger.LogWarning("IDs divergentes na atualização de tutor. Rota: {RouteId}, Corpo: {BodyId}", id, tutor?.IdTutor);
                return BadRequest("IDs divergentes.");
            }
            _logger.LogInformation("Iniciando atualização do tutor com ID: {TutorId}", id);
            try
            {
                await _service.UpdateAsync(tutor);
                _logger.LogInformation("Tutor atualizado com sucesso. ID: {TutorId}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar tutor com ID: {TutorId}", id);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}", Name = "DeleteTutor")]
        public async Task<ActionResult> Delete(decimal id)
        {
            _logger.LogInformation("Iniciando exclusão do tutor com ID: {TutorId}", id);
            try
            {
                await _service.DeleteAsync(id);
                _logger.LogInformation("Tutor excluído com sucesso. ID: {TutorId}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir tutor com ID: {TutorId}", id);
                return BadRequest(ex.Message);
            }
        }

        private TutorDto CreateTutorDto(Tutor t)
        {
            var links = new List<LinkDto>
            {
                new(Url.Link("GetTutorById", new { id = t.IdTutor })!, "self", "GET"),
                new(Url.Link("DeleteTutor", new { id = t.IdTutor })!, "delete_tutor", "DELETE")
            };

            return new TutorDto(
                t.IdTutor,
                t.NomeTutor,
                t.CpfTutor,
                t.TelefoneTutor,
                t.EmailTutor,
                t.CepTutor,
                t.RuaTutor,
                t.NumeroTutor,
                t.ComplementoTutor,
                t.BairroTutor,
                t.CidadeTutor,
                t.EstadoTutor,
                t.DataCadastroTutor,
                links
            );
        }

        private List<LinkDto> CreatePaginationLinks(int pageNumber, int pageSize, string? cidade, string? search, string sortBy, bool ascending, int totalPages)
        {
            var routeValues = new { pageNumber, pageSize, cidade, search, sortBy, ascending };
            var links = new List<LinkDto>
            {
                new(Url.Link("GetTutores", routeValues)!, "self", "GET")
            };

            if (pageNumber < totalPages)
            {
                var nextValues = new { pageNumber = pageNumber + 1, pageSize, cidade, search, sortBy, ascending };
                links.Add(new(Url.Link("GetTutores", nextValues)!, "next_page", "GET"));
            }

            if (pageNumber > 1)
            {
                var prevValues = new { pageNumber = pageNumber - 1, pageSize, cidade, search, sortBy, ascending };
                links.Add(new(Url.Link("GetTutores", prevValues)!, "prev_page", "GET"));
            }

            return links;
        }
    }
}