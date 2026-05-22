using Microsoft.EntityFrameworkCore;
using SistemaVeterinario.API.Data;
using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Repositories
{
    public class ConsultaRepository : IConsultaRepository
    {
        private readonly AppDbContext _context;

        public ConsultaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Consulta>> GetAllAsync()
        {
            // Usamos Include para carregar os objetos relacionados (Pet, Clínica, Veterinário)
            return await _context.Consultas
                .Include(c => c.Pet)
                .Include(c => c.Clinica)
                .Include(c => c.Veterinario)
                .ToListAsync();
        }

        public async Task<Consulta?> GetByIdAsync(decimal id)
        {
            return await _context.Consultas
                .Include(c => c.Pet)
                .Include(c => c.Clinica)
                .Include(c => c.Veterinario)
                .FirstOrDefaultAsync(c => c.IdConsulta == id);
        }

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
            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta != null)
            {
                _context.Consultas.Remove(consulta);
                await _context.SaveChangesAsync();
            }
        }
    }
}