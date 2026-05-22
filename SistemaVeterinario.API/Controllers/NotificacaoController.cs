using Microsoft.AspNetCore.Mvc;
using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificacaoController : ControllerBase
    {
        private readonly INotificacaoService _service;

        public NotificacaoController(INotificacaoService service)
        {
            _service = service;
        }

        // GET: api/Notificacao - Lista todas as notificações enviadas ou pendentes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notificacao>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        // GET: api/Notificacao/{id} - Busca uma notificação específica pelo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Notificacao>> GetById(decimal id)
        {
            var notificacao = await _service.GetByIdAsync(id);
            return notificacao == null ? NotFound() : Ok(notificacao);
        }

        // POST: api/Notificacao - Cria uma nova notificação
        [HttpPost]
        public async Task<ActionResult> Create(Notificacao notificacao)
        {
            await _service.AddAsync(notificacao);
            return CreatedAtAction(nameof(GetById), new { id = notificacao.IdNotificacao }, notificacao);
        }

        // PUT: api/Notificacao/{id} - Atualiza o status de uma notificação
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(decimal id, Notificacao notificacao)
        {
            if (id != notificacao.IdNotificacao) return BadRequest("ID da notificação divergente.");

            await _service.UpdateAsync(notificacao);
            return NoContent();
        }

        // DELETE: api/Notificacao/{id} - Remove uma notificação
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(decimal id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}