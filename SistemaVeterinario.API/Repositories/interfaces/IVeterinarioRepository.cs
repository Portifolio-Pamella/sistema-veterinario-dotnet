using SistemaVeterinario.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Repositories.Interfaces
{
    public interface IVeterinarioRepository
    {
        Task<IEnumerable<Veterinario>> GetAllAsync();
        Task<Veterinario?> GetByIdAsync(decimal id);
        Task AddAsync(Veterinario veterinario);
        Task UpdateAsync(Veterinario veterinario);
        Task DeleteAsync(decimal id);
    }
}