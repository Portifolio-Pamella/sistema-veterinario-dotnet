using Microsoft.EntityFrameworkCore;
using SistemaVeterinario.API.Data;
using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Repositories
{
    public class AcompanhamentoRepository : IAcompanhamentoRepository
    {
        private readonly AppDbContext _context;
        public AcompanhamentoRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Acompanhamento>> GetAllAsync() => await _context.Acompanhamentos.ToListAsync();

        public async Task<Acompanhamento?> GetByIdAsync(decimal id) => await _context.Acompanhamentos.FindAsync(id);

        public async Task AddAsync(Acompanhamento a) { _context.Acompanhamentos.Add(a); await _context.SaveChangesAsync(); }

        public async Task UpdateAsync(Acompanhamento a) { _context.Acompanhamentos.Update(a); await _context.SaveChangesAsync(); }

        public async Task DeleteAsync(decimal id)
        {
            var item = await GetByIdAsync(id);
            if (item != null) { _context.Acompanhamentos.Remove(item); await _context.SaveChangesAsync(); }
        }
    }
}