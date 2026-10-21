using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SistemaVeterinario.Domain.NoSqlModels;

public class HistoricoAtendimentoDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public decimal IdPet { get; set; }
    public string DescricaoAtendimento { get; set; } = string.Empty;
    public DateTime DataAtendimento { get; set; } = DateTime.UtcNow;
}