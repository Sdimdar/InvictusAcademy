using Ardalis.Result;
using MediatR;
using ServicesContracts.Courses.Responses;

namespace ServicesContracts.Courses.Requests.Tests.Commands;

public class CheckTestAnswersCommand : IRequest<Result<TestResultVm>>
{
    public int UserId { get; set; }
    public int CourseId { get; set; }
    public int ModuleId { get; set; }
    public int ArticleOrder { get; set; }
    public List<TestAnswer> Answers { get; set; }

    public class TestAnswer
    {
        public int QuestionId { get; set; }
        public List<int> ChoosedAnswers { get; set; }
    }
}
