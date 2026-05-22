using Microsoft.EntityFrameworkCore;
using SistemaVeterinario.API.Data;
using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Repositories
{
    public class MedicamentoRepository : IMedicamentoRepository
    {
        private readonly AppDbContext _context;

        public MedicamentoRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Medicamento>> GetAllAsync()
        {
            return await _context.Medicamentos
                .Include(m => m.Pet)
                .ToListAsync();
        }

        public async Task<Medicamento?> GetByIdAsync(decimal id)
        {
            return await _context.Medicamentos
                .Include(m => m.Pet)
                .FirstOrDefaultAsync(m => m.IdMedicamento == id);
        }

        public async Task AddAsync(Medicamento medicamento)
        {
            await _context.Medicamentos.AddAsync(medicamento);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Medicamento medicamento)
        {
            _context.Medicamentos.Update(medicamento);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(decimal id)
        {
            var medicamento = await _context.Medicamentos.FindAsync(id);
            if (medicamento != null)
            {
                _context.Medicamentos.Remove(medicamento);
                await _context.SaveChangesAsync();
            }
        }
    }
}