using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SistemaVeterinario.Domain.NoSqlModels;
using SistemaVeterinario.Infrastructure.NoSql;

namespace SistemaVeterinario.Application.Service;

public class HistoricoAtendimentoService
{
    private readonly IMongoCollection<HistoricoAtendimentoDocument> _collection;

    public HistoricoAtendimentoService(MongoDbContext context, IOptions<MongoDBSettings> settings)
    {
        // Pega o nome da collection configurada no appsettings ("LogsAtendimentos")
        var collectionName = settings.Value.CollectionName ?? "LogsAtendimentos";
        _collection = context.GetCollection<HistoricoAtendimentoDocument>(collectionName);
    }

    public async Task<List<HistoricoAtendimentoDocument>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<List<HistoricoAtendimentoDocument>> GetByPetIdAsync(decimal idPet)
    {
        return await _collection.Find(h => h.IdPet == idPet).ToListAsync();
    }

    public async Task CreateAsync(HistoricoAtendimentoDocument historico)
    {
        await _collection.InsertOneAsync(historico);
    }
}