namespace Courses.Domain.Entities.CourseResults;

public class ModuleProgress : ProgressResult
{
    public bool IsOpened { get; set; }
    public List<ArticleProgress> ArticlesProgresses { get; set; }
    public bool IsSuccess { get; set; }
}