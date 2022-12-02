using Courses.Domain.Entities.CourseInfo.Tests;

namespace Courses.Domain.Entities.CourseInfo;

public class Article
{
    public string Title { get; set; }
    public int Order { get; set; }
    public string VideoLink { get; set; }
    public string Text { get; set; }
    public Test? Test { get; set; }
}