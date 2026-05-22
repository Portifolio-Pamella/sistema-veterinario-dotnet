using Microsoft.EntityFrameworkCore;
using SistemaVeterinario.API.Data;
using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Repositories
{
    public class HistoricoRepository : IHistoricoRepository
    {
        private readonly AppDbContext _context;

        public HistoricoRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Historico>> GetAllAsync()
        {
            return await _context.Historicos
                .Include(h => h.Pet)
                .ToListAsync();
        }

        public async Task<Historico?> GetByIdAsync(decimal id)
        {
            return await _context.Historicos
                .Include(h => h.Pet)
                .FirstOrDefaultAsync(h => h.IdHistorico == id);
        }

        public async Task AddAsync(Historico historico)
        {
            await _context.Historicos.AddAsync(historico);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Historico historico)
        {
            _context.Historicos.Update(historico);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(decimal id)
        {
            var historico = await _context.Historicos.FindAsync(id);
            if (historico != null)
            {
                _context.Historicos.Remove(historico);
                await _context.SaveChangesAsync();
            }
        }
    }
}