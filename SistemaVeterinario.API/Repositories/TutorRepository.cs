using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaVeterinario.API.Data;
using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Repositories.interfaces;

namespace SistemaVeterinario.API.Repositories
{
    public class TutorRepository : ITutorRepository
    {
        private readonly AppDbContext _context;

        public TutorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tutor>> GetAllAsync() => await _context.Tutores.ToListAsync();

        public async Task<Tutor> GetByIdAsync(decimal id) => await _context.Tutores.FindAsync(id);

        public async Task AddAsync(Tutor tutor)
        {
            await _context.Tutores.AddAsync(tutor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Tutor tutor)
        {
            _context.Tutores.Update(tutor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(decimal id)
        {
            var tutor = await GetByIdAsync(id);
            if (tutor != null)
            {
                _context.Tutores.Remove(tutor);
                await _context.SaveChangesAsync();
            }
        }
    }
}