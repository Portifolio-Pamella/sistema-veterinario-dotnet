using Microsoft.AspNetCore.Mvc;
using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeterinarioController : ControllerBase
    {
        private readonly IVeterinarioService _service;

        public VeterinarioController(IVeterinarioService service)
        {
            _service = service;
        }

        // GET: api/Veterinario - Lista todos os veterinários registrados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Veterinario>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        // GET: api/Veterinario/{id} - Busca um veterinário específico pelo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Veterinario>> GetById(decimal id)
        {
            var veterinario = await _service.GetByIdAsync(id);
            return veterinario == null ? NotFound() : Ok(veterinario);
        }

        // POST: api/Veterinario - Cadastra um novo veterinário
        [HttpPost]
        public async Task<ActionResult> Create(Veterinario veterinario)
        {
            await _service.AddAsync(veterinario);
            return CreatedAtAction(nameof(GetById), new { id = veterinario.IdVeterinario }, veterinario);
        }

        // PUT: api/Veterinario/{id} - Atualiza os dados ou status de um veterinário
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(decimal id, Veterinario veterinario)
        {
            if (id != veterinario.IdVeterinario) return BadRequest("ID do veterinário divergente.");

            await _service.UpdateAsync(veterinario);
            return NoContent();
        }

        // DELETE: api/Veterinario/{id} - Remove um registro de veterinário
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(decimal id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}