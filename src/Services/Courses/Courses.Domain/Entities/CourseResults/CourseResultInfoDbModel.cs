using CommonRepository.Models;

namespace Courses.Domain.Entities.CourseResults;

public class CourseResultInfoDbModel : MongoBaseRepositoryEntity
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public float Score { get; set; }
    public List<ModuleProgress> ModuleProgresses { get; set; }
}
