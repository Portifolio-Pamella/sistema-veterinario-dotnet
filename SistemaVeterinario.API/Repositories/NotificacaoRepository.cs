using Microsoft.EntityFrameworkCore;
using SistemaVeterinario.API.Data;
using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Repositories
{
    public class NotificacaoRepository : INotificacaoRepository
    {
        private readonly AppDbContext _context;

        public NotificacaoRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Notificacao>> GetAllAsync()
        {
            return await _context.Notificacoes
                .Include(n => n.Tutor)
                .Include(n => n.Pet)
                .ToListAsync();
        }

        public async Task<Notificacao?> GetByIdAsync(decimal id)
        {
            return await _context.Notificacoes
                .Include(n => n.Tutor)
                .Include(n => n.Pet)
                .FirstOrDefaultAsync(n => n.IdNotificacao == id);
        }

        public async Task AddAsync(Notificacao notificacao)
        {
            await _context.Notificacoes.AddAsync(notificacao);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Notificacao notificacao)
        {
            _context.Notificacoes.Update(notificacao);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(decimal id)
        {
            var notificacao = await _context.Notificacoes.FindAsync(id);
            if (notificacao != null)
            {
                _context.Notificacoes.Remove(notificacao);
                await _context.SaveChangesAsync();
            }
        }
    }
}