using Courses.Domain.Entities.CourseInfo.Tests;

namespace ServicesContracts.Courses.Responses;

public class PurchasedTestVm
{
    public int TestId { get; set; }
    public string TestQuestion { get; set; }
    public QuestionType QuestionType { get; set; }
    public List<PurchasedTestAnswerVm> TestAnswers { get; set; }
}
