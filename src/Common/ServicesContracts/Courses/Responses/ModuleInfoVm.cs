using Courses.Domain.Entities.CourseInfo;

namespace ServicesContracts.Courses.Responses;

public class ModuleInfoVm
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public List<Article>? Articles { get; set; } = null;
}