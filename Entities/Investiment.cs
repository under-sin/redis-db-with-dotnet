using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;

namespace RedisImMemmoryDB.Entities;

public class Investiment
{
    [BsonId]
    [BsonGuidRepresentation(GuidRepresentation.Standard)] // configurando o guid para o mongodb
    public Guid Id { get; private set; }
    public string Name { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime PurchaseDate { get; set; }
}
