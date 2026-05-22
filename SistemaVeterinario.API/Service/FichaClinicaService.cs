using SistemaVeterinario.API.Models;
using SistemaVeterinario.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVeterinario.API.Services
{
    public class FichaClinicaService : IFichaClinicaService
    {
        private readonly IFichaClinicaRepository _repository;

        public FichaClinicaService(IFichaClinicaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<FichaClinica>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<FichaClinica?> GetByIdAsync(decimal id) => await _repository.GetByIdAsync(id);

        public async Task AddAsync(FichaClinica ficha)
        {
            // Regra: Uma ficha clínica deve ter um pet válido associado
            if (ficha.IdPet <= 0)
                throw new ArgumentException("O ID do Pet é obrigatório para criar uma ficha clínica.");

            await _repository.AddAsync(ficha);
        }

        public async Task UpdateAsync(FichaClinica ficha)
        {
            var existing = await _repository.GetByIdAsync(ficha.IdFichaClinica);
            if (existing == null) throw new KeyNotFoundException("Ficha clínica não encontrada.");

            await _repository.UpdateAsync(ficha);
        }

        public async Task DeleteAsync(decimal id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) throw new KeyNotFoundException("Ficha clínica não encontrada.");

            await _repository.DeleteAsync(id);
        }
    }
}