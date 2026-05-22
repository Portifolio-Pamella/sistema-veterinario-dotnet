using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Repositories; // Correção: Importa o namespace do repositório
using SistemaVeterinario.API.Services;     // Importa o namespace onde o INotificacaoService está
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Services
{
    public class NotificacaoService : INotificacaoService
    {
        private readonly INotificacaoRepository _repository;

        public NotificacaoService(INotificacaoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Notificacao>> GetAllAsync()
            => await _repository.GetAllAsync();

        public async Task<Notificacao?> GetByIdAsync(decimal id)
            => await _repository.GetByIdAsync(id);

        public async Task AddAsync(Notificacao notificacao)
            => await _repository.AddAsync(notificacao);

        public async Task UpdateAsync(Notificacao notificacao)
            => await _repository.UpdateAsync(notificacao);

        public async Task DeleteAsync(decimal id)
            => await _repository.DeleteAsync(id);
    }
}