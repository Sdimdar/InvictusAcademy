using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace CommonRepository.Models;

public class MongoBaseRepositoryEntity
{
    [BsonElement("_id")]
    [BsonId]
    [BsonRepresentation(BsonType.Int32)]
    [JsonPropertyName("_id")]
    public int Id { get; set; }
}
