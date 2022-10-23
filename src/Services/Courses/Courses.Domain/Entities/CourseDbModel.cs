using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using CommonRepository.Models;

namespace Courses.Domain.Entities;

public class CourseDbModel : MongoBaseRepositoryEntity
{

    [BsonElement("Name")]
    public string CourseTitle { get; set; }
    public string CourseDescription { get; set; }
    public List<string>? ModulesIdentificators { get; set; }
}
