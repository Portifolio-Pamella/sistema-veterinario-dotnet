using Microsoft.AspNetCore.Mvc;
using SistemaVeterinario.Domain.Models;
using SistemaVeterinario.Application.Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace SistemaVeterinario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TutorController : ControllerBase
    {
        private readonly ITutorService _service;

        public TutorController(ITutorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tutor>>> GetAll()
        {
            try { return Ok(await _service.GetAllAsync()); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tutor>> GetById(decimal id)
        {
            try
            {
                var t = await _service.GetByIdAsync(id);
                return t == null ? NotFound() : Ok(t);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost]
        public async Task<ActionResult> Create(Tutor tutor)
        {
            try
            {
                await _service.AddAsync(tutor);
                return CreatedAtAction(nameof(GetById), new { id = tutor.IdTutor }, tutor);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(decimal id, Tutor tutor)
        {
            if (id != tutor.IdTutor) return BadRequest("IDs divergentes.");
            try
            {
                await _service.UpdateAsync(tutor);
                return NoContent();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(decimal id)
        {
            try { await _service.DeleteAsync(id); return NoContent(); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}