using Microsoft.AspNetCore.Mvc;
using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AcompanhamentoController : ControllerBase
    {
        private readonly IAcompanhamentoService _service;

        public AcompanhamentoController(IAcompanhamentoService service)
        {
            _service = service;
        }

        // GET: api/Acompanhamento - Lista todos os acompanhamentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Acompanhamento>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        // GET: api/Acompanhamento/{id} - Busca um acompanhamento específico
        [HttpGet("{id}")]
        public async Task<ActionResult<Acompanhamento>> GetById(decimal id)
        {
            var acompanhamento = await _service.GetByIdAsync(id);
            return acompanhamento == null ? NotFound() : Ok(acompanhamento);
        }

        // POST: api/Acompanhamento - Cria um novo acompanhamento
        [HttpPost]
        public async Task<ActionResult> Create(Acompanhamento acompanhamento)
        {
            await _service.AddAsync(acompanhamento);
            return CreatedAtAction(nameof(GetById), new { id = acompanhamento.IdAcompanhamento }, acompanhamento);
        }

        // PUT: api/Acompanhamento/{id} - Atualiza um acompanhamento existente
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(decimal id, Acompanhamento acompanhamento)
        {
            if (id != acompanhamento.IdAcompanhamento) return BadRequest();
            await _service.UpdateAsync(acompanhamento);
            return NoContent();
        }

        // DELETE: api/Acompanhamento/{id} - Remove um acompanhamento
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(decimal id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}