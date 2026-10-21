using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SistemaVeterinario.Application.Service;
using SistemaVeterinario.Domain.NoSqlModels;

namespace SistemaVeterinario.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class HistoricoAtendimentoController : ControllerBase
{
    private readonly HistoricoAtendimentoService _service;
    private readonly ILogger<HistoricoAtendimentoController> _logger;

    public HistoricoAtendimentoController(HistoricoAtendimentoService service, ILogger<HistoricoAtendimentoController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<HistoricoAtendimentoDocument>>> GetAll()
    {
        _logger.LogInformation("Listando todos os históricos de atendimento do MongoDB.");
        var historicos = await _service.GetAllAsync();
        return Ok(historicos);
    }

    [HttpGet("pet/{idPet:decimal}")]
    public async Task<ActionResult<IEnumerable<HistoricoAtendimentoDocument>>> GetByPetId(decimal idPet)
    {
        _logger.LogInformation("Buscando históricos do pet ID: {PetId} no MongoDB.", idPet);
        var historicos = await _service.GetByPetIdAsync(idPet);
        return Ok(historicos);
    }

    [HttpPost]
    public async Task<ActionResult> Create(HistoricoAtendimentoDocument historico)
    {
        _logger.LogInformation("Cadastrando novo histórico no MongoDB para o pet ID: {PetId}", historico.IdPet);
        await _service.CreateAsync(historico);
        return Ok(new { message = "Histórico de atendimento gravado com sucesso no MongoDB!", historico });
    }
}