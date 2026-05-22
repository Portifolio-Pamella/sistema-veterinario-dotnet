using SistemaVeterinario.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Services
{
    public interface IPetClinicaService
    {
        Task<IEnumerable<PetClinica>> GetAllAsync();
        Task<PetClinica?> GetByIdAsync(decimal id);
        Task<IEnumerable<PetClinica>> GetByClinicaIdAsync(decimal idClinica);
        Task<IEnumerable<PetClinica>> GetByPetIdAsync(decimal idPet);
        Task AddAsync(PetClinica petClinica);
        Task UpdateAsync(PetClinica petClinica);
        Task DeleteAsync(decimal id);
    }
}