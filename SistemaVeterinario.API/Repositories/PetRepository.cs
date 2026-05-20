using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaVeterinario.API.Data;
using SistemaVeterinario.API.Models;

namespace SistemaVeterinario.API.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly AppDbContext _context;

        public PetRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pet>> GetAllAsync() =>
            await _context.Pets.Include(p => p.Tutor).ToListAsync();

        public async Task<Pet> GetByIdAsync(decimal id) =>
            await _context.Pets.Include(p => p.Tutor).FirstOrDefaultAsync(p => p.IdPet == id);

        public async Task<IEnumerable<Pet>> GetByEspecieAsync(string especie) =>
            await _context.Pets.Where(p => p.EspeciePet.ToLower() == especie.ToLower()).ToListAsync();

        public async Task<IEnumerable<Pet>> GetByTutorAsync(decimal idTutor) =>
            await _context.Pets.Where(p => p.IdTutor == idTutor).ToListAsync();

        public async Task AddAsync(Pet pet)
        {
            await _context.Pets.AddAsync(pet);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Pet pet)
        {
            _context.Pets.Update(pet);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(decimal id)
        {
            var pet = await GetByIdAsync(id);
            if (pet != null)
            {
                _context.Pets.Remove(pet);
                await _context.SaveChangesAsync();
            }
        }
    }
}