using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Repositories; // Certifique-se que o namespace aqui bate com o da interface
using SistemaVeterinario.API.Services;     // Onde está o IClinicaService
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Services
{
    public class ClinicaService : IClinicaService
    {
        private readonly IClinicaRepository _repository;

        public ClinicaService(IClinicaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Clinica>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<Clinica?> GetByIdAsync(decimal id) => await _repository.GetByIdAsync(id);

        public async Task AddAsync(Clinica clinica)
        {
            await _repository.AddAsync(clinica);
        }

        public async Task UpdateAsync(Clinica clinica) => await _repository.UpdateAsync(clinica);

        public async Task DeleteAsync(decimal id) => await _repository.DeleteAsync(id);
    }
}