using SistemaVeterinario.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Services
{
    public interface IPetService
    {
        Task<IEnumerable<Pet>> GetAllAsync();
        Task<Pet?> GetByIdAsync(decimal id);
        Task AddAsync(Pet pet);
        Task UpdateAsync(Pet pet);
        Task DeleteAsync(decimal id);
    }
}