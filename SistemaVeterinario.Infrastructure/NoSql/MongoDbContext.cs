using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SistemaVeterinario.Infrastructure.NoSql;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoDBSettings> settings)
    {
        var mongoSettings = MongoClientSettings.FromConnectionString(settings.Value.ConnectionString);
        mongoSettings.ServerApi = new ServerApi(ServerApiVersion.V1);

        var client = new MongoClient(mongoSettings);

        try
        {
            client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
            Console.WriteLine("Conexão com o MongoDB Atlas realizada com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao conectar no MongoDB Atlas: {ex.Message}");
        }

        _database = client.GetDatabase(settings.Value.DatabaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }
}