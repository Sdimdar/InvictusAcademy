using CommonRepository.Models;

namespace Courses.Domain.Entities.CourseResults;

public class CourseResultInfoDbModel : MongoBaseRepositoryEntity
{
    public List<ModuleProgress> ModuleProgresses { get; set; }
}
