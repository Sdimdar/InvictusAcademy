using Ardalis.Result;
using MediatR;
using ServicesContracts.Courses.Responses;

namespace ServicesContracts.Courses.Requests.Querries;

public class GetCoursesQuerry : IRequest<Result<CoursesVm>>
{
    public string Email { get; set; }
    public CourseTypes Type { get; set; }
}
