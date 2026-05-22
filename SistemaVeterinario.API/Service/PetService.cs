using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Repositories; 
using SistemaVeterinario.API.Repositories.Interfaces;
using SistemaVeterinario.API.Services;     
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _repository;

        public PetService(IPetRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Pet>> GetAllAsync()
            => await _repository.GetAllAsync();

        public async Task<Pet?> GetByIdAsync(decimal id)
            => await _repository.GetByIdAsync(id);

        public async Task AddAsync(Pet pet)
            => await _repository.AddAsync(pet);

        public async Task UpdateAsync(Pet pet)
            => await _repository.UpdateAsync(pet);

        public async Task DeleteAsync(decimal id)
            => await _repository.DeleteAsync(id);
    }
}