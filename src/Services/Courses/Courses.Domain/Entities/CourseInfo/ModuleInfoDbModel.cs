using CommonRepository.Models;

namespace Courses.Domain.Entities.CourseInfo;

public class ModuleInfoDbModel : MongoBaseRepositoryEntity
{
    public ModuleInfo ModuleInfo { get; set; }
}
