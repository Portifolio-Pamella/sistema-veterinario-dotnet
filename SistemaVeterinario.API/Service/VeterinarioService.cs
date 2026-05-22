using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Repositories; // Corrigido: sem o ".Interfaces"
using SistemaVeterinario.API.Repositories.Interfaces;
using SistemaVeterinario.API.Services;     // Corrigido: plural "Services"
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Services
{
    public class VeterinarioService : IVeterinarioService
    {
        private readonly IVeterinarioRepository _repository;

        public VeterinarioService(IVeterinarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Veterinario>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<Veterinario?> GetByIdAsync(decimal id) => await _repository.GetByIdAsync(id);

        public async Task AddAsync(Veterinario veterinario) => await _repository.AddAsync(veterinario);

        public async Task UpdateAsync(Veterinario veterinario) => await _repository.UpdateAsync(veterinario);

        public async Task DeleteAsync(decimal id) => await _repository.DeleteAsync(id);
    }
}