using CommonRepository.Models;

namespace Courses.Domain.Entities.CourseInfo;

public class CourseInfoDbModel : MongoBaseRepositoryEntity
{
    public string ModulesString { get; set; }
}
