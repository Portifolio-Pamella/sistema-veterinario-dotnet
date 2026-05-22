using Microsoft.AspNetCore.Mvc;
using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoricoController : ControllerBase
    {
        private readonly IHistoricoService _service;

        public HistoricoController(IHistoricoService service)
        {
            _service = service;
        }

        // GET: api/Historico - Lista todo o histórico registrado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Historico>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        // GET: api/Historico/{id} - Busca um registro de histórico por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Historico>> GetById(decimal id)
        {
            var historico = await _service.GetByIdAsync(id);
            return historico == null ? NotFound() : Ok(historico);
        }

        // POST: api/Historico - Registra um novo evento no histórico do pet
        [HttpPost]
        public async Task<ActionResult> Create(Historico historico)
        {
            await _service.AddAsync(historico);
            return CreatedAtAction(nameof(GetById), new { id = historico.IdHistorico }, historico);
        }

        // PUT: api/Historico/{id} - Atualiza um registro de histórico
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(decimal id, Historico historico)
        {
            if (id != historico.IdHistorico) return BadRequest("ID do histórico divergente.");

            await _service.UpdateAsync(historico);
            return NoContent();
        }

        // DELETE: api/Historico/{id} - Remove um registro do histórico
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(decimal id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}