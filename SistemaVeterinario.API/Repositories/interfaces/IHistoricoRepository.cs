using SistemaVeterinario.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Repositories.Interfaces
{
    public interface IHistoricoRepository
    {
        Task<IEnumerable<Historico>> GetAllAsync();
        Task<Historico?> GetByIdAsync(decimal id);
        Task AddAsync(Historico historico);
        Task UpdateAsync(Historico historico);
        Task DeleteAsync(decimal id);
    }
}