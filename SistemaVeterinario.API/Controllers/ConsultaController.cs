using Microsoft.AspNetCore.Mvc;
using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaService _service;

        public ConsultaController(IConsultaService service)
        {
            _service = service;
        }

        // GET: api/Consulta - Retorna todas as consultas agendadas ou realizadas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Consulta>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        // GET: api/Consulta/{id} - Busca detalhes de uma consulta específica
        [HttpGet("{id}")]
        public async Task<ActionResult<Consulta>> GetById(decimal id)
        {
            var consulta = await _service.GetByIdAsync(id);
            return consulta == null ? NotFound() : Ok(consulta);
        }

        // POST: api/Consulta - Cria um novo registro de consulta
        [HttpPost]
        public async Task<ActionResult> Create(Consulta consulta)
        {
            await _service.AddAsync(consulta);
            return CreatedAtAction(nameof(GetById), new { id = consulta.IdConsulta }, consulta);
        }

        // PUT: api/Consulta/{id} - Atualiza o status ou diagnóstico de uma consulta
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(decimal id, Consulta consulta)
        {
            if (id != consulta.IdConsulta) return BadRequest("ID da consulta divergente.");

            await _service.UpdateAsync(consulta);
            return NoContent();
        }

        // DELETE: api/Consulta/{id} - Cancela ou remove uma consulta do histórico
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(decimal id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}