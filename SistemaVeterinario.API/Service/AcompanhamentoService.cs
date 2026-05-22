using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Repositories.Interfaces; // Ajuste conforme seu namespace
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Services
{
    public class AcompanhamentoService : IAcompanhamentoService
    {
        private readonly IAcompanhamentoRepository _repository;

        public AcompanhamentoService(IAcompanhamentoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Acompanhamento>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<Acompanhamento?> GetByIdAsync(decimal id) => await _repository.GetByIdAsync(id);

        public async Task AddAsync(Acompanhamento acompanhamento)
        {
            // Regra de negócio simples: data de início não pode ser futura
            if (acompanhamento.DataInicioAcompanhamento > DateTime.Now)
                throw new ArgumentException("A data de início não pode ser no futuro.");

            await _repository.AddAsync(acompanhamento);
        }

        public async Task UpdateAsync(Acompanhamento acompanhamento) => await _repository.UpdateAsync(acompanhamento);

        public async Task DeleteAsync(decimal id) => await _repository.DeleteAsync(id);
    }
}