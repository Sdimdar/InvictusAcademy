namespace Courses.Domain.Entities.CourseInfo.Tests;

public class TestQuestion
{
    public int Id { get; set; }
    public string Question { get; set; }
    public QuestionType QuestionType { get; set; }
    public List<Answer> Answers { get; set; }
}