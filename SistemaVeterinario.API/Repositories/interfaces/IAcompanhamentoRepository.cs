using SistemaVeterinario.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Repositories.Interfaces
{
    public interface IAcompanhamentoRepository
    {
        Task<IEnumerable<Acompanhamento>> GetAllAsync();
        Task<Acompanhamento?> GetByIdAsync(decimal id);
        Task AddAsync(Acompanhamento acompanhamento);
        Task UpdateAsync(Acompanhamento acompanhamento);
        Task DeleteAsync(decimal id);
    }
}