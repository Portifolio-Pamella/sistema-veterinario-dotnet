using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaVeterinario.API.Models;

namespace SistemaVeterinario.API.Repositories
{
    public interface IPetRepository
    {
        Task<IEnumerable<Pet>> GetAllAsync();
        Task<Pet> GetByIdAsync(decimal id);
        Task<IEnumerable<Pet>> GetByEspecieAsync(string especie);
        Task<IEnumerable<Pet>> GetByTutorAsync(decimal idTutor);
        Task AddAsync(Pet pet);
        Task UpdateAsync(Pet pet);
        Task DeleteAsync(decimal id);
    }
}