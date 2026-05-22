using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Services
{
    public class PetClinicaService : IPetClinicaService
    {
        private readonly IPetClinicaRepository _repository;

        public PetClinicaService(IPetClinicaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PetClinica>> GetAllAsync()
            => await _repository.GetAllAsync();

        public async Task<PetClinica?> GetByIdAsync(decimal id)
            => await _repository.GetByIdAsync(id);

        public async Task<IEnumerable<PetClinica>> GetByClinicaIdAsync(decimal idClinica)
            => await _repository.GetByClinicaIdAsync(idClinica);

        public async Task<IEnumerable<PetClinica>> GetByPetIdAsync(decimal idPet)
            => await _repository.GetByPetIdAsync(idPet);

        public async Task AddAsync(PetClinica petClinica)
            => await _repository.AddAsync(petClinica);

        public async Task UpdateAsync(PetClinica petClinica)
            => await _repository.UpdateAsync(petClinica);

        public async Task DeleteAsync(decimal id)
            => await _repository.DeleteAsync(id);
    }
}