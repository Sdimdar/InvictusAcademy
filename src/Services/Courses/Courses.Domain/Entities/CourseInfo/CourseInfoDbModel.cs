using CommonRepository.Models;

namespace Courses.Domain.Entities.CourseInfo;

public class CourseInfoDbModel : MongoBaseRepositoryEntity
{
    public CourseInfo CourseInfo { get; set; }
}
