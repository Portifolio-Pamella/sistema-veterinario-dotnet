using SistemaVeterinario.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Repositories // Este namespace deve coincidir exatamente
{
    public interface IClinicaRepository
    {
        Task<IEnumerable<Clinica>> GetAllAsync();
        Task<Clinica?> GetByIdAsync(decimal id);
        Task AddAsync(Clinica clinica);
        Task UpdateAsync(Clinica clinica);
        Task DeleteAsync(decimal id);
    }
}