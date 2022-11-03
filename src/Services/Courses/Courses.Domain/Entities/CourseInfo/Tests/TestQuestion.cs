namespace Courses.Domain.Entities.CourseInfo.Tests;

public class TestQuestion
{
    public string Question { get; set; }
    public QuestionType QuestionType { get; set; }
    public List<Answer> Answers { get; set; }
}