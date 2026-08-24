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
    public class PetController : ControllerBase
    {
        private readonly IPetService _service;

        public PetController(IPetService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pet>>> GetAll()
        {
            try { return Ok(await _service.GetAllAsync()); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pet>> GetById(decimal id)
        {
            try
            {
                var p = await _service.GetByIdAsync(id);
                return p == null ? NotFound() : Ok(p);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost]
        public async Task<ActionResult> Create(Pet pet)
        {
            try
            {
                await _service.AddAsync(pet);
                return CreatedAtAction(nameof(GetById), new { id = pet.IdPet }, pet);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(decimal id, Pet pet)
        {
            if (id != pet.IdPet) return BadRequest("ID divergente.");
            try
            {
                await _service.UpdateAsync(pet);
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