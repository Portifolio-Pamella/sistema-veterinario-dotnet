using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Repositories; // Namespace da interface
using SistemaVeterinario.API.Services;     // Namespace do IConsultaService
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Services
{
    public class ConsultaService : IConsultaService
    {
        private readonly IConsultaRepository _repository;

        public ConsultaService(IConsultaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Consulta>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<Consulta?> GetByIdAsync(decimal id) => await _repository.GetByIdAsync(id);

        public async Task AddAsync(Consulta consulta)
        {
            if (consulta.DataConsulta < DateTime.Now)
                throw new ArgumentException("A data da consulta não pode ser no passado.");

            await _repository.AddAsync(consulta);
        }

        public async Task UpdateAsync(Consulta consulta)
        {
            var existing = await _repository.GetByIdAsync(consulta.IdConsulta);
            if (existing == null) throw new KeyNotFoundException("Consulta não encontrada.");

            await _repository.UpdateAsync(consulta);
        }

        public async Task DeleteAsync(decimal id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) throw new KeyNotFoundException("Consulta não encontrada.");

            await _repository.DeleteAsync(id);
        }
    }
}