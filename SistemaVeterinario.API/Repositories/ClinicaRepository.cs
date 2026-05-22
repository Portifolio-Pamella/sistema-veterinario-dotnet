using SistemaVeterinario.API.Data;
using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Repositories; // ESSA LINHA É A QUE RESOLVE O ERRO CS0246
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Repositories
{
    public class ClinicaRepository : IClinicaRepository
    {
        private readonly AppDbContext _context;

        public ClinicaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Clinica>> GetAllAsync()
            => await _context.Clinicas.ToListAsync();

        public async Task<Clinica?> GetByIdAsync(decimal id)
            => await _context.Clinicas.FindAsync(id);

        public async Task AddAsync(Clinica clinica)
        {
            await _context.Clinicas.AddAsync(clinica);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Clinica clinica)
        {
            _context.Clinicas.Update(clinica);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(decimal id)
        {
            var clinica = await GetByIdAsync(id);
            if (clinica != null)
            {
                _context.Clinicas.Remove(clinica);
                await _context.SaveChangesAsync();
            }
        }
    }
}