using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaVeterinario.API.Models;

namespace SistemaVeterinario.API.Repositories.interfaces
{
    public interface IConsultaRepository
    {
        Task<IEnumerable<Consulta>> GetAllAsync();
        Task<Consulta> GetByIdAsync(decimal id);
        Task AddAsync(Consulta consulta);
        Task UpdateAsync(Consulta consulta);
        Task DeleteAsync(decimal id);
    }
}