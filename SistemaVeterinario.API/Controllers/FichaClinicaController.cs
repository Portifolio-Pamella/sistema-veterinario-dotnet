using Microsoft.AspNetCore.Mvc;
using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FichaClinicaController : ControllerBase
    {
        private readonly IFichaClinicaService _service;

        public FichaClinicaController(IFichaClinicaService service)
        {
            _service = service;
        }

        // GET: api/FichaClinica - Lista todas as fichas clínicas cadastradas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FichaClinica>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        // GET: api/FichaClinica/{id} - Busca a ficha clínica por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<FichaClinica>> GetById(decimal id)
        {
            var ficha = await _service.GetByIdAsync(id);
            return ficha == null ? NotFound() : Ok(ficha);
        }

        // POST: api/FichaClinica - Cria uma nova ficha clínica para um pet
        [HttpPost]
        public async Task<ActionResult> Create(FichaClinica ficha)
        {
            await _service.AddAsync(ficha);
            return CreatedAtAction(nameof(GetById), new { id = ficha.IdFichaClinica }, ficha);
        }

        // PUT: api/FichaClinica/{id} - Atualiza informações de uma ficha existente
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(decimal id, FichaClinica ficha)
        {
            if (id != ficha.IdFichaClinica) return BadRequest("ID da ficha clínica divergente.");

            await _service.UpdateAsync(ficha);
            return NoContent();
        }

        // DELETE: api/FichaClinica/{id} - Remove uma ficha clínica do sistema
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(decimal id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}