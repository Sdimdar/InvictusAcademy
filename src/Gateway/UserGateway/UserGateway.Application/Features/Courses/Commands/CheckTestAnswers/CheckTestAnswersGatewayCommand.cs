using static ServicesContracts.Courses.Requests.Tests.Commands.CheckTestAnswersCommand;

namespace UserGateway.Application.Features.Courses.Commands.CheckTestAnswers;

public class CheckTestAnswersGatewayCommand
{
    public int CourseId { get; set; }
    public int ModuleId { get; set; }
    public int ArticleOrder { get; set; }
    public List<TestAnswer> Answers { get; set; }
}
