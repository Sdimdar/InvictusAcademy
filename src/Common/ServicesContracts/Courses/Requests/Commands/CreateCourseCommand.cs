using Ardalis.Result;
using MediatR;

namespace ServicesContracts.Courses.Requests.Commands;

public class CreateCourseCommand : IRequest<Result<string>>
{
    public string CourseTitle { get; set; }
    public string CourseDescription { get; set; }
    public List<string>? ModulesIdentificators { get; set; }
}
