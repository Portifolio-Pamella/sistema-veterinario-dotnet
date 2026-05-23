using Microsoft.EntityFrameworkCore;
using SistemaVeterinario.API.Data;
using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Repositories
{
    public class VeterinarioRepository : IVeterinarioRepository
    {
        private readonly AppDbContext _context;

        public VeterinarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Veterinario>> GetAllAsync()
        {
            return await _context.Veterinarios.ToListAsync();
        }

        public async Task<Veterinario?> GetByIdAsync(decimal id)
        {
            return await _context.Veterinarios.FirstOrDefaultAsync(v => v.IdVeterinario == id);
        }

        public async Task AddAsync(Veterinario veterinario)
        {
            veterinario.IdVeterinario = 0;

            await _context.Veterinarios.AddAsync(veterinario);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Veterinario veterinario)
        {
            var existing = await _context.Veterinarios.FindAsync(veterinario.IdVeterinario);

            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(veterinario);

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(decimal id)
        {
            var veterinario = await _context.Veterinarios.FindAsync(id);
            if (veterinario != null)
            {
                _context.Veterinarios.Remove(veterinario);
                await _context.SaveChangesAsync();
            }
        }
    }
}