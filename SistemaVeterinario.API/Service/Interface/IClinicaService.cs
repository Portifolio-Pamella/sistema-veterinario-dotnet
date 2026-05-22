using SistemaVeterinario.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Services
{
    public interface IClinicaService
    {
        Task<IEnumerable<Clinica>> GetAllAsync();
        Task<Clinica?> GetByIdAsync(decimal id);
        Task AddAsync(Clinica clinica);
        Task UpdateAsync(Clinica clinica);
        Task DeleteAsync(decimal id);
    }
}