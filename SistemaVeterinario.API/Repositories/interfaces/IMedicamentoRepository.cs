using SistemaVeterinario.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Repositories.Interfaces
{
    public interface IMedicamentoRepository
    {
        Task<IEnumerable<Medicamento>> GetAllAsync();
        Task<Medicamento?> GetByIdAsync(decimal id);
        Task AddAsync(Medicamento medicamento);
        Task UpdateAsync(Medicamento medicamento);
        Task DeleteAsync(decimal id);
    }
}