namespace Courses.Domain.Entities.CourseResults;

public class ArticlesProgress : ProgressResult
{
    public bool IsOpened { get; set; }
    public List<TestAttempt> TestAttempts { get; set; }
    public bool IsSuccess { get; set; }
}