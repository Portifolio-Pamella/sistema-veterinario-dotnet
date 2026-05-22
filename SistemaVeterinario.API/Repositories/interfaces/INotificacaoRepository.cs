using SistemaVeterinario.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Repositories
{
    public interface INotificacaoRepository
    {
        Task<IEnumerable<Notificacao>> GetAllAsync();
        Task<Notificacao?> GetByIdAsync(decimal id);
        Task AddAsync(Notificacao notificacao);
        Task UpdateAsync(Notificacao notificacao);
        Task DeleteAsync(decimal id);
    }
}