using SistemaVeterinario.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Services
{
    public interface IFichaClinicaService
    {
        Task<IEnumerable<FichaClinica>> GetAllAsync();
        Task<FichaClinica?> GetByIdAsync(decimal id);
        Task AddAsync(FichaClinica ficha);
        Task UpdateAsync(FichaClinica ficha);
        Task DeleteAsync(decimal id);
    }
}