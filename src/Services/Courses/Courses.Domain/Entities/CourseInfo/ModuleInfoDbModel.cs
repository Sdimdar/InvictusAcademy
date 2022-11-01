using CommonRepository.Models;

namespace Courses.Domain.Entities.CourseInfo;

public class ModuleInfoDbModel : MongoBaseRepositoryEntity
{
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public List<Articles> Articles { get; set; }
}
