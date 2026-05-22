using Microsoft.EntityFrameworkCore;
using SistemaVeterinario.API.Data;
using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Repositories
{
    public class FichaClinicaRepository : IFichaClinicaRepository
    {
        private readonly AppDbContext _context;

        public FichaClinicaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FichaClinica>> GetAllAsync()
        {
            return await _context.FichasClinicas
                .Include(f => f.Pet)
                .ToListAsync();
        }

        public async Task<FichaClinica?> GetByIdAsync(decimal id)
        {
            return await _context.FichasClinicas
                .Include(f => f.Pet)
                .FirstOrDefaultAsync(f => f.IdFichaClinica == id);
        }

        public async Task AddAsync(FichaClinica ficha)
        {
            await _context.FichasClinicas.AddAsync(ficha);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FichaClinica ficha)
        {
            _context.FichasClinicas.Update(ficha);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(decimal id)
        {
            var ficha = await _context.FichasClinicas.FindAsync(id);
            if (ficha != null)
            {
                _context.FichasClinicas.Remove(ficha);
                await _context.SaveChangesAsync();
            }
        }
    }
}