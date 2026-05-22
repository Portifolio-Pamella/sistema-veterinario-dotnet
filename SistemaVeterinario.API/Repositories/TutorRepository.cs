using Microsoft.EntityFrameworkCore;
using SistemaVeterinario.API.Data;
using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Repositories
{
    public class TutorRepository : ITutorRepository
    {
        private readonly AppDbContext _context;

        public TutorRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Tutor>> GetAllAsync()
        {
            return await _context.Tutores.ToListAsync();
        }

        public async Task<Tutor?> GetByIdAsync(decimal id)
        {
            return await _context.Tutores.FindAsync(id);
        }

        public async Task AddAsync(Tutor tutor)
        {
            if (tutor.IdTutor == 0)
            {
                var maxId = await _context.Tutores.MaxAsync(t => (decimal?)t.IdTutor) ?? 0;
                tutor.IdTutor = maxId + 1;
            }

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
            var tutor = await _context.Tutores.FindAsync(id);
            if (tutor != null)
            {
                _context.Tutores.Remove(tutor);
                await _context.SaveChangesAsync();
            }
        }
    }
}