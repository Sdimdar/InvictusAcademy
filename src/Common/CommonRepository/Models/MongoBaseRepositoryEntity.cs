using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CommonRepository.Models;

public class MongoBaseRepositoryEntity
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
}
