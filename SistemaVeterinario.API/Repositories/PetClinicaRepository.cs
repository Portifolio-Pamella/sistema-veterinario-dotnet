using Microsoft.EntityFrameworkCore;
using SistemaVeterinario.API.Data;
using SistemaVeterinario.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Repositories
{
    public class PetClinicaRepository : IPetClinicaRepository
    {
        private readonly AppDbContext _context;

        public PetClinicaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PetClinica>> GetAllAsync()
        {
            return await _context.PetClinicas.ToListAsync();
        }

        public async Task<PetClinica?> GetByIdAsync(decimal id)
        {
            return await _context.PetClinicas.FindAsync(id);
        }

        public async Task<IEnumerable<PetClinica>> GetByClinicaIdAsync(decimal idClinica)
        {
            return await _context.PetClinicas
                .Where(pc => pc.IdClinica == idClinica)
                .ToListAsync();
        }

        public async Task<IEnumerable<PetClinica>> GetByPetIdAsync(decimal idPet)
        {
            return await _context.PetClinicas
                .Where(pc => pc.IdPet == idPet)
                .ToListAsync();
        }

        public async Task AddAsync(PetClinica petClinica)
        {
            await _context.PetClinicas.AddAsync(petClinica);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PetClinica petClinica)
        {
            _context.PetClinicas.Update(petClinica);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(decimal id)
        {
            var petClinica = await GetByIdAsync(id);
            if (petClinica != null)
            {
                _context.PetClinicas.Remove(petClinica);
                await _context.SaveChangesAsync();
            }
        }
    }
}