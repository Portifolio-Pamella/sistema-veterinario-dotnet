using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaVeterinario.API.Data;
using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Repositories.interfaces;

namespace SistemaVeterinario.API.Repositories
{
    public class ConsultaRepository : IConsultaRepository
    {
        private readonly AppDbContext _context;

        public ConsultaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Consulta>> GetAllAsync() =>
            await _context.Consultas.Include(c => c.Pet).Include(c => c.Veterinario).Include(c => c.Clinica).ToListAsync();

        public async Task<Consulta> GetByIdAsync(decimal id) =>
            await _context.Consultas.Include(c => c.Pet).Include(c => c.Veterinario).Include(c => c.Clinica).FirstOrDefaultAsync(c => c.IdConsulta == id);

        public async Task AddAsync(Consulta consulta)
        {
            await _context.Consultas.AddAsync(consulta);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Consulta consulta)
        {
            _context.Consultas.Update(consulta);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(decimal id)
        {
            var consulta = await GetByIdAsync(id);
            if (consulta != null)
            {
                _context.Consultas.Remove(consulta);
                await _context.SaveChangesAsync();
            }
        }
    }
}