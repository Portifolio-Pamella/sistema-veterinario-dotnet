using SistemaVeterinario.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Repositories
{
    public interface IPetClinicaRepository
    {
        // Retorna todos os vínculos registrados
        Task<IEnumerable<PetClinica>> GetAllAsync();

        // Busca um vínculo específico pelo ID
        Task<PetClinica?> GetByIdAsync(decimal id);

        // Busca todos os vínculos de uma clínica específica (opcional, mas recomendado)
        Task<IEnumerable<PetClinica>> GetByClinicaIdAsync(decimal idClinica);

        // Busca todos os vínculos de um pet específico
        Task<IEnumerable<PetClinica>> GetByPetIdAsync(decimal idPet);

        // Adiciona um novo vínculo
        Task AddAsync(PetClinica petClinica);

        // Atualiza um vínculo existente
        Task UpdateAsync(PetClinica petClinica);

        // Remove um vínculo
        Task DeleteAsync(decimal id);
    }
}