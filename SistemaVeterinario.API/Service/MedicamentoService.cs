using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Services
{
    public class MedicamentoService : IMedicamentoService
    {
        private readonly IMedicamentoRepository _repository;

        public MedicamentoService(IMedicamentoRepository repository) => _repository = repository;

        public async Task<IEnumerable<Medicamento>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<Medicamento?> GetByIdAsync(decimal id) => await _repository.GetByIdAsync(id);

        public async Task AddAsync(Medicamento medicamento)
        {
            if (medicamento.DataInicioMedicamento > (medicamento.DataFimMedicamento ?? DateTime.MaxValue))
                throw new ArgumentException("A data de início não pode ser posterior à data de fim.");

            await _repository.AddAsync(medicamento);
        }

        public async Task UpdateAsync(Medicamento medicamento) => await _repository.UpdateAsync(medicamento);

        public async Task DeleteAsync(decimal id) => await _repository.DeleteAsync(id);
    }
}