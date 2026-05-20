using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaVeterinario.API.Models;

namespace SistemaVeterinario.API.Repositories.interfaces
{
    public interface ITutorRepository
    {
        Task<IEnumerable<Tutor>> GetAllAsync();
        Task<Tutor> GetByIdAsync(decimal id);
        Task AddAsync(Tutor tutor);
        Task UpdateAsync(Tutor tutor);
        Task DeleteAsync(decimal id);
    }
}