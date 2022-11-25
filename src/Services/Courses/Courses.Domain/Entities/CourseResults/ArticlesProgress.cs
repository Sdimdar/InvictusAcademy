namespace Courses.Domain.Entities.CourseResults;

public class ArticleProgress : ProgressResult
{
    public bool IsOpened { get; set; }
    public List<TestAttempt> TestAttempts { get; set; }
    public bool IsSuccess { get; set; }
}