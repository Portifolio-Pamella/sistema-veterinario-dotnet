using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaVeterinario.API.Models;

namespace SistemaVeterinario.API.Repositories.interfaces
{
    public interface IVeterinarioRepository
    {
        Task<IEnumerable<Veterinario>> GetAllAsync();
        Task<Veterinario> GetByIdAsync(decimal id);
        Task AddAsync(Veterinario veterinario);
        Task UpdateAsync(Veterinario veterinario);
        Task DeleteAsync(decimal id);
    }
}