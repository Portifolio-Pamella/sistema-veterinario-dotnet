using SistemaVeterinario.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Repositories
{
    public interface IConsultaRepository
    {
        Task<IEnumerable<Consulta>> GetAllAsync();
        Task<Consulta?> GetByIdAsync(decimal id);
        Task AddAsync(Consulta consulta);
        Task UpdateAsync(Consulta consulta);
        Task DeleteAsync(decimal id);
    }
}