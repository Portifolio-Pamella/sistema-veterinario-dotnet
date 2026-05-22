using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Repositories;
using SistemaVeterinario.API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Services
{
    public class TutorService : ITutorService
    {
        private readonly ITutorRepository _repository;

        public TutorService(ITutorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Tutor>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Tutor?> GetByIdAsync(decimal id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(Tutor tutor)
        {
            await _repository.AddAsync(tutor);
        }

        public async Task UpdateAsync(Tutor tutor)
        {
            await _repository.UpdateAsync(tutor);
        }

        public async Task DeleteAsync(decimal id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}