using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Services
{
    public class HistoricoService : IHistoricoService
    {
        private readonly IHistoricoRepository _repository;

        public HistoricoService(IHistoricoRepository repository) => _repository = repository;

        public async Task<IEnumerable<Historico>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<Historico?> GetByIdAsync(decimal id) => await _repository.GetByIdAsync(id);

        public async Task AddAsync(Historico historico) => await _repository.AddAsync(historico);

        public async Task UpdateAsync(Historico historico) => await _repository.UpdateAsync(historico);

        public async Task DeleteAsync(decimal id) => await _repository.DeleteAsync(id);
    }
}