using CommonRepository.Models;

namespace Courses.Domain.Entities.CourseInfo;

public class ModuleInfoDbModel : MongoBaseRepositoryEntity
{
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public List<Article>? Articles { get; set; } = null;
}
