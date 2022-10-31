using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CommonRepository.Models;

public class MongoBaseRepositoryEntity
{

    [BsonId]
    [BsonRepresentation(BsonType.Int32)]
    public int Id { get; set; }
}
