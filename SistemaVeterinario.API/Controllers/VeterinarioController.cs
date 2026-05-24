using Microsoft.AspNetCore.Mvc;
using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Services;

namespace SistemaVeterinario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeterinarioController : ControllerBase
    {
        private readonly IVeterinarioService _service;
        public VeterinarioController(IVeterinarioService service) => _service = service;

        /// <summary>Lista todos os veterinários cadastrados.</summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Veterinario>>> GetAll()
        {
            try { return Ok(await _service.GetAllAsync()); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// <summary>Busca um veterinário específico pelo ID.</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Veterinario>> GetById(decimal id)
        {
            try
            {
                var v = await _service.GetByIdAsync(id);
                return v == null ? NotFound("Veterinário não encontrado.") : Ok(v);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// <summary>Cadastra um novo veterinário.</summary>
        [HttpPost]
        public async Task<ActionResult> Create(Veterinario v)
        {
            try
            {
                await _service.AddAsync(v);
                return CreatedAtAction(nameof(GetById), new { id = v.IdVeterinario }, v);
            }
            catch (Exception ex) { return BadRequest($"Erro ao cadastrar: {ex.Message}"); }
        }

        /// <summary>Atualiza os dados de um veterinário.</summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(decimal id, Veterinario v)
        {
            if (id != v.IdVeterinario) return BadRequest("IDs divergentes.");
            try
            {
                await _service.UpdateAsync(v);
                return NoContent();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// <summary>Remove um veterinário pelo ID.</summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(decimal id)
        {
            try { await _service.DeleteAsync(id); return NoContent(); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}