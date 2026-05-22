using Microsoft.AspNetCore.Mvc;
using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicamentoController : ControllerBase
    {
        private readonly IMedicamentoService _service;

        public MedicamentoController(IMedicamentoService service)
        {
            _service = service;
        }

        // GET: api/Medicamento - Lista todos os medicamentos registrados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medicamento>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        // GET: api/Medicamento/{id} - Busca detalhes de um medicamento específico
        [HttpGet("{id}")]
        public async Task<ActionResult<Medicamento>> GetById(decimal id)
        {
            var medicamento = await _service.GetByIdAsync(id);
            return medicamento == null ? NotFound() : Ok(medicamento);
        }

        // POST: api/Medicamento - Registra uma nova prescrição/medicamento
        [HttpPost]
        public async Task<ActionResult> Create(Medicamento medicamento)
        {
            await _service.AddAsync(medicamento);
            return CreatedAtAction(nameof(GetById), new { id = medicamento.IdMedicamento }, medicamento);
        }

        // PUT: api/Medicamento/{id} - Atualiza o status ou dados de um medicamento
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(decimal id, Medicamento medicamento)
        {
            if (id != medicamento.IdMedicamento) return BadRequest("ID do medicamento divergente.");

            await _service.UpdateAsync(medicamento);
            return NoContent();
        }

        // DELETE: api/Medicamento/{id} - Remove um registro de medicamento
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(decimal id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}