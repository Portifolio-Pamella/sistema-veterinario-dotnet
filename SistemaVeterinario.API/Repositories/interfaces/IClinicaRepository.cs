using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaVeterinario.API.Models;

namespace SistemaVeterinario.API.Repositories.interfaces
{
    public interface IClinicaRepository
    {
        Task<IEnumerable<Clinica>> GetAllAsync();
        Task<Clinica> GetByIdAsync(decimal id);
        Task AddAsync(Clinica clinica);
        Task UpdateAsync(Clinica clinica);
        Task DeleteAsync(decimal id);
    }
}