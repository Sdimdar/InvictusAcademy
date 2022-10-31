using CommonRepository.Models;

namespace Courses.Domain.Entities.CourseResults;

public class CourseResultInfoDbModel : MongoBaseRepositoryEntity
{
    public CourseResultInfo CourseResultInfo { get; set; }
}
