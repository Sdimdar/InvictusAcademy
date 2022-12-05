using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CommonRepository.Models;

public class MongoBaseRepositoryEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.Int32)]
    public int Id { get; set; }
}
